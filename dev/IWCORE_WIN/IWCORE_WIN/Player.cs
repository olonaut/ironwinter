using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWCORE_WIN
{
    class Player
    {
        public Vector2 pos;
        public float facing; /* where the player is facing in rad */
        public Texture2D texture; /* texture, to be loaded in core.cs */
        public Vector2 origin; /* Honestly, how does this work? */

        public static double SHOOTINGSPEED; /* minimal timespan between two shots in milliseconds */

        public Player(Vector2 _pos)
        {
            pos = _pos;
            facing = 0;
            SHOOTINGSPEED = 200;
        }
        


    }
}
