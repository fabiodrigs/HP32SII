using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP32SII.Logic
{
    public class NoEscapeMode : EscapeMode
    {
        public NoEscapeMode()
        {
            TopStatus = "";
        }

        public override EscapeMode HandleLeftArrow()
        {
            return new LeftEscapeMode();
        }

        public override EscapeMode HandleRightArrow()
        {
            return new RightEscapeMode();
        }
    }
}
