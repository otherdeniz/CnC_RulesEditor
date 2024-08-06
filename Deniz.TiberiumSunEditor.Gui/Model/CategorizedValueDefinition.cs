﻿using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class CategorizedValueDefinition
    {
        public CategorizedValueDefinition(UnitValueDefinition unitValueDefinition, string category)
        {
            UnitValueDefinition = unitValueDefinition;
            Category = category;
        }

        public UnitValueDefinition UnitValueDefinition { get; }

        public string Category { get; }

    }
}