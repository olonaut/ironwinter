using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWCORE_WIN
{
    class Map
    {
        /*
         For the purposes of creating a running prototype,
         this class will only contain code to create a simple room
         and maybe a one or two enemies.
         This Prototype is very limited, and is only supposed to test
         the ground-mechanics of the final product, so they feel right.
         The code will be updated to involve more features later. 
         */

        public Room demoRoom;
        //private Room[] rooms;
        static private Vector2 DEMOROOMSIZE = new Vector2(400,400); 


        public Map()
        {
            demoRoom = new Room(new Vector2(50,50),DEMOROOMSIZE,1); /* Testmap */
        }
    }
}
