using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RhythmHell
{
    class BulletPool
    {
        List<BulletBase> bullets = new List<BulletBase>();
        GameTime gameTime;

        //Constructor
        public BulletPool(int poolSize, GameTime gameTime)
        {
            PoolSize = poolSize;
            IsReverse = false;
            this.gameTime = gameTime;

            //allocates memory with reusable bullets
            for (int i = 0; i < PoolSize; i++)
            {
                bullets.Add(new BulletBase(Vector2.Zero, 1, new StandardBehavior(Vector2.Zero, Vector2.Zero, 0, 0), 0));
            }
        }

        //Properties
        public int PoolSize { get; set; }
        public bool IsReverse { get; set; }
        public List<BulletBase> Bullets
        {
            get { return bullets; }
            set { bullets = value; }
        }

        //Public Methods
        public void Create(Vector2 centerLocation, short hitBoxRadius, IBulletBehavior bulletBehavior, int entryFrame)
        {
            for (int i = 0; i < PoolSize; i++)
            {
                if (!bullets[i].InUse)
                {
                    //initializes bullet data
                    bullets[i].CenterLocation = centerLocation;
                    bullets[i].HitBoxRadius = hitBoxRadius;
                    bullets[i].SetBulletBehavior(bulletBehavior);
                    bullets[i].EntryFrame = entryFrame;
                    bullets[i].InUse = true;
                    return;
                }
            }
        }

        public void Update()
        {
            for (int i = 0; i < PoolSize; i++)
            {
                //updates all bullets in use
                if (bullets[i].InUse)
                {
                    if (IsReverse)
                    {
                        if (bullets[i].EntryFrame + 1 >= gameTime.Frame)
                            bullets[i].InUse = false;

                        bullets[i].TimeManipulationFactor *= -1;
                    }

                    //updates every bullet
                    bullets[i].Update();

                    bullets[i].TimeManipulationFactor = 1;  //resets time manipulation factor
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < PoolSize; i++)
            {
                //updates all bullets in use
                if (bullets[i].InUse)
                {
                    bullets[i].Draw();
                }
            }
        }

        //Private Methods

    }
}
