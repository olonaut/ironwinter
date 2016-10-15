using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWCORE_WIN
{
    class Line
    {
        Vector2 a;
        Vector2 b;

        public bool IsIntersecting(Line a, Line b)
        {
            float denominator = ((a.b.X - a.a.X) * (b.b.Y - b.a.Y)) - ((a.b.Y - a.a.Y) * (b.b.X - b.a.X));
            float numerator1 = ((a.a.Y - b.a.Y) * (b.b.X - b.a.X)) - ((a.a.X - b.a.X) * (b.b.Y - b.a.Y));
            float numerator2 = ((a.a.Y - b.a.Y) * (a.b.X - a.a.X)) - ((a.a.X - b.a.X) * (a.b.Y - a.a.Y));

            // Detect coincident lines (has a problem)
            if (denominator == 0) return numerator1 == 0 && numerator2 == 0;

            float r = numerator1 / denominator;
            float s = numerator2 / denominator;

            return (r >= 0 && r <= 1) && (s >= 0 && s <= 1);
        }

        public static void Draw(SpriteBatch sb, Texture2D _t, Vector2 start, Vector2 end, Color col)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle = (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(_t,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                col, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }
    }
}
