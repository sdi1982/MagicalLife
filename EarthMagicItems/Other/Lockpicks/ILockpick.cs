﻿// <copyright file="ILockpick.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace EarthMagicItems.Other.Lockpicks
{
    using EarthWithMagicAPI.API.Interfaces.Items;

    /// <summary>
    /// Used by all lock picks. Stores some logic.
    /// </summary>
    public abstract class ILockpick : IItem
    {
        public ILockpick(string name, double weight, string imagePath, string documentationPath, int lockpickBonus)
            : base(name, weight, imagePath, documentationPath)
        {
            this.EquipImpact.Lockpick = lockpickBonus;
        }
    }
}