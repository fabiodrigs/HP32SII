﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HP32SII.Logic.Test")]

namespace HP32SII.Logic
{
    internal sealed class StorageUnit : IDatabase
    {
        private Dictionary<string, double> memory = new Dictionary<string, double>
        {
            { "A", 0.0 }, { "B", 0.0 }, { "C", 0.0 }, { "D", 0.0 },
            { "E", 0.0 }, { "F", 0.0 }, { "G", 0.0 }, { "H", 0.0 },
            { "I", 0.0 }, { "J", 0.0 }, { "K", 0.0 }, { "L", 0.0 },
            { "M", 0.0 }, { "N", 0.0 }, { "O", 0.0 }, { "P", 0.0 },
            { "Q", 0.0 }, { "R", 0.0 }, { "S", 0.0 }, { "T", 0.0 },
            { "U", 0.0 }, { "V", 0.0 }, { "W", 0.0 }, { "X", 0.0 },
            { "Y", 0.0 }, { "Z", 0.0 }, { "i", 0.0 },
        };

        public void Store(string key, double value)
        {
            memory[key] = value;
        }

        public double Recall(string key)
        {
            return memory[key];
        }

        public void ClearAll()
        {
            memory = memory.ToDictionary(p => p.Key, p => 0.0);
        }
    }
}
