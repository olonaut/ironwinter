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
        Vector2 pos;
        double facing; /* where the player is facing, where as 0 = straight up, 25 = right, 50 = straight down, 75 = left */
        public Texture2D texture;
        private Vector2 origin;

        public Player(Vector2 _pos)
        {
            pos = _pos;
        }

        public Vector2 getOrigin()
        {
            return origin;
        }

        public void setOrigin(Vector2 _origin)
        {
            origin = _origin;
        }

        public Vector2 getPos()
        {
            return pos;
        }
        public void setPos(Vector2 _pos)
        {
            pos = _pos;
        }


    }
}
