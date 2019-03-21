﻿using MagicalLifeAPI.Filing.Logging;
using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using ProtoBuf;

namespace MagicalLifeAPI.Networking.Serialization
{
    /// <summary>
    /// Used to serialize and deserialize using https://github.com/mgravell/protobuf-net.
    /// </summary>
    public static class ProtoUtil
    {
        public static RuntimeTypeModel TypeModel { get; set; }

        /// <summary>
        /// Value: The ID of a base message.
        /// Key: The type of the base message that the ID is connected with.
        /// </summary>
        public static Dictionary<NetMessageID, Type> IDToMessage { get; private set; } = new Dictionary<NetMessageID, Type>();

        /// <summary>
        /// Serializes the object to string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Serialize<T>(T data)
        {
            try
            {
                using (MemoryStream outputStream = new MemoryStream())
                {
                    TypeModel.SerializeWithLengthPrefix(outputStream, data, typeof(T), ProtoBuf.PrefixStyle.Base128, 0);
                    return outputStream.GetBuffer();
                }
            }
            catch (Exception e)
            {
                MasterLog.DebugWriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Deserializes the message from bytes.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static BaseMessage Deserialize(byte[] data)
        {
            try
            {
                using (MemoryStream ms = new System.IO.MemoryStream(data))
                {
                    BaseMessage Base = (BaseMessage)TypeModel.DeserializeWithLengthPrefix(ms, null, typeof(BaseMessage), ProtoBuf.PrefixStyle.Base128, 0);
                    return Base;
                }
            }
            catch (IndexOutOfRangeException e)
            {
                MasterLog.DebugWriteLine(e, "Unknown message type!");
                return null;
            }
        }

        public static T Deserialize<T>(string data)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(data)))
                {
                    return (T)TypeModel.Deserialize(ms, null, typeof(T));
                }
            }
            catch (Exception e)
            {
                MasterLog.DebugWriteLine(e.Message);
                return default;
            }
        }

        /// <summary>
        /// Registers a type as serializable.
        /// </summary>
        /// <param name="serializableClassType"></param>
        internal static void RegisterSerializableClass(Type serializableClassType)
        {
            TypeModel.Add(serializableClassType, true);
        }

        /// <summary>
        /// Registers a type as the subclass of another type.
        /// </summary>
        /// <param name="subclass"></param>
        /// <param name="baseClass"></param>
        internal static void RegisterSubclass(Type subclass, Type baseClass)
        {
            MetaType meta = TypeModel.Add(baseClass, true);
            SubType[] subtypes = meta.GetSubtypes();
            meta.AddSubType(subtypes.Length + 1, subclass);
        }

        /// <summary>
        /// Registers all serializable types found within an assembly.
        /// While this is slower than <see cref="RegisterSerializableClass(Type)"/> and <see cref="RegisterSubclass(Type, Type)"/>, this is more convenient.
        /// </summary>
        /// <param name="assembly"></param>
        internal static void RegisterAssembly(Assembly assembly)
        {
            Type[] allTypes = assembly.GetExportedTypes();

            foreach (Type item in allTypes)
            {
                Attribute attribute = item.GetCustomAttribute(typeof(ProtoContractAttribute));

                if (attribute != null)
                {
                    RegisterSerializableClass(item);
                    
                    if (item.BaseType != null)
                    {
                        Attribute baseAttribute = item.BaseType.GetCustomAttribute(typeof(ProtoContractAttribute));

                        if (baseAttribute != null)
                        {
                            RegisterSubclass(item, item.BaseType);
                        }
                    }
                }
            }
        }
    }
}