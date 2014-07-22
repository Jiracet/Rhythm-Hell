using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace RhythmHell
{
    class BulletBase
    {
        //fields
        protected Image image;
        protected Vector2 centerLocation;
        protected Int16 hitBoxRadius;

        //constructors
        public BulletBase(Vector2 centerLocation, Int16 hitBoxRadius, IBulletBehavior bulletBehavior, int entryFrame)
        {
            CenterLocation = centerLocation;
            HitBoxRadius = hitBoxRadius;
            this.bulletBehavior = bulletBehavior;
            EntryFrame = entryFrame;
            TimeManipulationFactor = 1; //default flow of time is 1

            InUse = false;  //initially set to not in use
        }

        //interfaces
        protected IBulletBehavior bulletBehavior;

        //properties
        public bool InUse { get; set; }
        public int EntryFrame { get; set; }
        public float TimeManipulationFactor { get; set; }

        public Vector2 CenterLocation
        {
            get { return centerLocation; }
            set
            {
                centerLocation = value;
            }
        }

        public Int16 HitBoxRadius
        {
            get { return hitBoxRadius; }
            set
            {
                if (value >= 0 && value <= 100)
                    hitBoxRadius = value;
            }
        }

        //methods
        //updates the bullet according to bullet behavior
        public void Update()
        {
            CenterLocation = bulletBehavior.Update(CenterLocation, TimeManipulationFactor);
        }

        //sets the bullet behavior of the bullet
        public void SetBulletBehavior(IBulletBehavior bulletBehavior)
        {
            this.bulletBehavior = bulletBehavior;
        }

        //Post: draws bullet on screen
        public void Draw()
        {
            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(Color.Red);
            GL.Vertex2(CenterLocation.X + HitBoxRadius, CenterLocation.Y);
            GL.Vertex2(CenterLocation.X, CenterLocation.Y - HitBoxRadius);
            GL.Vertex2(CenterLocation.X - HitBoxRadius, CenterLocation.Y);
            GL.Vertex2(CenterLocation.X, CenterLocation.Y + HitBoxRadius);
            GL.End();
        }

    }
}
