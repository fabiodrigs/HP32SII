using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP32SII.Logic
{
    public class LeftEscapeMode : EscapeMode
    {
        public LeftEscapeMode()
        {
            TopStatus = "  <=";
        }

        public override EscapeMode HandleLeftArrow()
        {
            return new NoEscapeMode();
        }

        public override EscapeMode HandleRightArrow()
        {
            return new RightEscapeMode();
        }
    }
}
