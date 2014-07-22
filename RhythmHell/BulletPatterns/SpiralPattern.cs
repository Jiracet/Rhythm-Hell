using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;

namespace RhythmHell
{
    class SpiralPattern : IBulletPattern
    {
        //fields
        Vector2 origin;
        float angle, startAngle, degreesPerBullet, bulletSpeed;
        int framesPerBullet, frameOffset, numBullets, patternEntryFrame, counter;
        GameTime gameTime;

        //constructors
        public SpiralPattern(Vector2 origin, int framesPerBullet, float degreesPerBullet, float bulletSpeed, int numBullets,
            float startAngle, GameTime gameTime)
        {
            this.origin = origin;
            this.framesPerBullet = framesPerBullet;
            this.degreesPerBullet = degreesPerBullet;
            this.bulletSpeed = bulletSpeed;
            this.angle = startAngle;
            this.startAngle = startAngle;
            this.numBullets = numBullets;
            this.gameTime = gameTime;

            patternEntryFrame = gameTime.Frame;
            IsReverse = false;
        }

        //properties
        public bool IsReverse { get; set; } //determines if the pattern is in reverse or not
        public BulletPool BulletPool { get; set; }

        //methods
        public void Update()
        {
            if (!IsReverse)
            {
                if (frameOffset % framesPerBullet == 0)
                {
                    if (counter < numBullets)
                    {
                        //increments angle
                        angle = startAngle + (frameOffset / framesPerBullet) * degreesPerBullet;

                        counter++;  //increments counter when bullet is created
                        BulletPool.Create(origin, 3, new StandardBehavior(MathHelper.AngleToDirection(angle), Vector2.Zero, bulletSpeed, 0), gameTime.Frame);
                    }
                }

                frameOffset++;
            }
            else
            {
                if (frameOffset % framesPerBullet == 0)
                    counter--;

                frameOffset--;
            }
        }
    }
}
