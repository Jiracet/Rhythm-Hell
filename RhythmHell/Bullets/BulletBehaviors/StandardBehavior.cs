using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RhythmHell
{
    class StandardBehavior : IBulletBehavior
    {
        //fields
        protected Vector2 vel;
        protected Vector2 accel;
        protected float velMag, accelMag;

        //constructors
        public StandardBehavior(Vector2 vel, Vector2 accel, float velMag, float accelMag)
        {
            this.Vel = vel;
            this.Accel = accel;
            this.VelMag = velMag;
            this.AccelMag = accelMag;
        }

        //properties
        public Vector2 Vel
        {
            get { return vel; }
            set { vel = value.Normalized(); }
        }

        public Vector2 Accel
        {
            get { return accel; }
            set { accel = value.Normalized(); }
        }

        public float VelMag
        {
            get { return velMag; }
            set
            {
                if (value >= 0 && value <= 100)
                    velMag = value;
            }
        }

        public float AccelMag
        {
            get { return accelMag; }
            set
            {
                if (value >= 0 && value <= 100)
                    accelMag = value;
            }
        }

        //methods
        public Vector2 Update(Vector2 CenterLocation, float timeManipulationFactor)
        {
            //standard movement
            //Vel = Vector2.Add(Vel, Vector2.Multiply(Accel, AccelMag));
            return Vector2.Add(CenterLocation, Vector2.Multiply(Vel, VelMag) * timeManipulationFactor);
        }
    }
}
