﻿using System;

namespace Generators
{
    public class IntGenerator : Generator
    {
        public IntGenerator(Random random) : base(random)
        {
        }

        public override object GenerateValue()
        {
            return Random.Next();
        }
    }
}