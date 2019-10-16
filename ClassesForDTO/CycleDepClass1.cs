using System;
using System.Collections.Generic;

namespace ClassesForDTO
{
    public enum Days
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
    public class CycleDepClass1
    {
        public List<string> list { get; set; }
        public int Item { get; set; }
        public int Item2 { get; set; }

        public CycleDepClass2 Cl { get; set; }

        public Struct1 S1 { get; set; }

        public DateTime DT1 { get; set; }

        public Days Ds { get; set; }
    }
}
