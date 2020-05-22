using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annogramm.DataStructs
{
    class CharBoolPair
    {
        public char Char { get; private set; }

        public bool Using { get; set; }

        public CharBoolPair(char ch)
        {
            Char = ch;
            Using = false;
        }
    }
}
