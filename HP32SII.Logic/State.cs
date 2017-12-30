using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP32SII.Logic
{
    public enum State
    {
        Off,
        Default,
        Left,
        Right,
        Store,
        Recall,
        WaitForDefault,
    }
}
