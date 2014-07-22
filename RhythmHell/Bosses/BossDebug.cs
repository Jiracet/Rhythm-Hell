using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RhythmHell
{
    class BossDebug : BossBase
    {
        public BossDebug(Vector2 centerLocation, IBossBehavior bossBehavior, BulletPool bulletPool, GameTime gameTime) : 
            base(centerLocation, bossBehavior, bulletPool, gameTime)
        { 
            
        }

        public override void Draw()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Purple);
            GL.Vertex3(CenterLocation.X - 30, CenterLocation.Y + 15, 0);
            GL.Vertex3(CenterLocation.X - 30, CenterLocation.Y - 15, 0);
            GL.Vertex3(CenterLocation.X + 30, CenterLocation.Y - 15, 0);
            GL.Vertex3(CenterLocation.X + 30, CenterLocation.Y + 15, 0);
            GL.End();
        }
    }
}
