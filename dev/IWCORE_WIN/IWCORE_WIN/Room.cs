using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWCORE_WIN
{
    class Room
    {
        public Vector2 pos;
        public Vector2 size;
        public int foeAmmount;
        public Foe[] foes;

        public Room(Vector2 _pos, Vector2 _size, int _foeAmmount)
        {
            pos = _pos;
            size = _size;
            foeAmmount = _foeAmmount;
        }
    }
}
