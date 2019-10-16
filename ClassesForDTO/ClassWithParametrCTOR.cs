using System;
using System.Collections.Generic;
using System.Text;

namespace ClassesForDTO
{
    public class ClassWithParametrCTOR
    {
        public char ch { get; }
        public DateTime dateTime;
        public string str { get; }

        public ClassWithParametrCTOR(char ch, string str)
        {
            this.ch = ch;
            this.str = str;
        }
    }
}
