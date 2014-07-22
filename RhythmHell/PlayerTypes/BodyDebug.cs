using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace RhythmHell
{
    class BodyDebug : BodyBase
    {

        //constructors
        public BodyDebug(Vector2 centerLocation, float speed, Int16 hitBoxRadius, Rectangle boundaries)
            : base(centerLocation, speed, hitBoxRadius, boundaries)
        { 

        }

        //Methods
        public override void Draw()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Green);
            GL.Vertex3(CenterLocation.X - 10, CenterLocation.Y + 5, 0);
            GL.Vertex3(CenterLocation.X - 10, CenterLocation.Y - 5, 0);
            GL.Vertex3(CenterLocation.X + 10, CenterLocation.Y - 5, 0);
            GL.Vertex3(CenterLocation.X + 10, CenterLocation.Y + 5, 0);
            GL.End();

        }

    }
}
