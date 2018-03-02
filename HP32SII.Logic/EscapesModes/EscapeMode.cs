using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP32SII.Logic
{
    public abstract class EscapeMode
    {
        public static string TopStatus { get; protected set; } = "";

        public abstract EscapeMode HandleLeftArrow();
        public abstract EscapeMode HandleRightArrow();
    }
}
