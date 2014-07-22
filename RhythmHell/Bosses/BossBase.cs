using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.Drawing;

namespace RhythmHell
{
    abstract class BossBase
    {
        //fields
        protected Image image;

        protected Vector2 centerLocation;

        //constructors
        public BossBase(Vector2 centerLocation, IBossBehavior bossBehavior, BulletPool bulletPool, GameTime gameTime)
        {
            this.centerLocation = centerLocation;
            this.bossBehavior = bossBehavior;
            BulletPool = bulletPool;
            GameTime = gameTime;
        }

        //interfaces
        protected IBossBehavior bossBehavior;

        //properties
        public GameTime GameTime { get; set; }
        public BulletPool BulletPool { get; set; }
        public Dictionary<int, Vector2> BossPositionHistory { get; set; }
        public Vector2 CenterLocation
        {
            get { return centerLocation; }
            set { centerLocation = value; }
        }

        public void SetBossBehavior(IBossBehavior bossBehavior)
        {
            this.bossBehavior = bossBehavior;
        }

        //public methods
        public virtual void Update()
        {
            CenterLocation = bossBehavior.Update(CenterLocation);
        }

        abstract public void Draw();
    }
}
