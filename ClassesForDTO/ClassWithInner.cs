using System;
using System.Collections.Generic;
using System.Text;

namespace ClassesForDTO
{
    public class ClassWithInner
    {
        public string str;

        public Dictionary<string, object> dict;

        public ClassWithNoInner NoInner { get; set; }
    }
}
