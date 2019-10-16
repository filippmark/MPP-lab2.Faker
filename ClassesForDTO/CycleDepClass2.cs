using System;
using System.Collections.Generic;
using System.Text;

namespace ClassesForDTO
{
    public class CycleDepClass2
    {
        public int X { get; set; }
        public string Str { get; set; }

        public CycleDepClass1 Cl { get; set; }
    }
}
