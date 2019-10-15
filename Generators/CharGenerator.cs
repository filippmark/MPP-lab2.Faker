﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Generators
{
    class CharGenerator : Generator
    {
        public CharGenerator(Random randomizer) : base(randomizer)
        {
            GeneratedType = typeof(char);
        }

        public override object GenerateValue()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int index = Random.Next(chars.Length);
            return chars[index];
        }
    }
}