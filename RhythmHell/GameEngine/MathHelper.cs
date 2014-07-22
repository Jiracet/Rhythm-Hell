using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RhythmHell
{
    static class MathHelper
    {
        //methods

        //Convert Operation Methods
        //Post: returns direction vector using angle from up vector
        public static Vector2 AngleToDirection(float angle)
        {
            return new Vector2((float)Math.Sin(ToRadians((double)angle)),
                (float)Math.Cos(ToRadians((double)angle)));
        }

        public static float ToRadians(float degrees)
        {
            return (float)Math.PI * degrees / 180;
        }

        public static double ToRadians(double degrees)
        {
            return Math.PI * degrees / 180;
        }

        public static float ToDegrees(float radians)
        {
            return 180 * radians / (float)Math.PI;
        }

        public static double ToDegrees(double radians)
        {
            return 180 * radians / Math.PI;
        }
    }
}
