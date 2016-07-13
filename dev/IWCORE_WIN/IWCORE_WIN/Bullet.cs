using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWCORE_WIN
{
    class Bullet
    {
        public Vector2 pos; /* Bullet Position */
        public double heading; /* Bullet direction in radiant */
        public bool active = false;

        public Bullet(Vector2 _pos, double _heading)
        {
            pos = _pos;
            heading = _heading;
            active = true;
        }

    }
}
