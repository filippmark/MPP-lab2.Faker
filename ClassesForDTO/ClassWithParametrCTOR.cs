using System;
using System.Collections.Generic;
using System.Text;

namespace ClassesForDTO
{
    class ClassWithParametrCTOR
    {
        internal long longg;
        public DateTime DateTime;
        internal string str;

        public ClassWithParametrCTOR(long longg, string str)
        {
            this.longg = longg;
            this.str = str;
        }
    }
}
