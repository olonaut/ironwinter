using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWCORE_WIN
{
    class Foe
    {
        Vector2 pos;
        double facing; /* For further reference, see Player.cs */

        public Foe(Vector2 _pos)
        {
            pos = _pos;
        }
    }
}
