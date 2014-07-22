using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace RhythmHell
{
    abstract class BodyBase
    {
        //fields
        protected Image image;

        protected Vector2 centerLocation;
        protected float speed;
        protected Int16 hitBoxRadius;
        protected Rectangle boundaries;

        //constructors
        public BodyBase(Vector2 centerLocation, float speed, Int16 hitBoxRadius, Rectangle boundaries)
        {
            this.Boundaries = boundaries;
            this.CenterLocation = centerLocation;
            this.Speed = speed;
            this.HitBoxRadius = hitBoxRadius;
            PlayerPositionHistory = new Dictionary<int, Vector2>();
        }

        //properties
        public Dictionary<int, Vector2> PlayerPositionHistory { get; set; }
        public Vector2 CenterLocation
        {
            get { return centerLocation; }
            set
            {
                if (boundaries.Contains(new Point((int)value.X, (int)value.Y)))
                    centerLocation = value;
            }
        }

        public float Speed
        {
            get { return speed; }
            set
            {
                if (value >= 0 && value <= 100)
                    speed = value;
            }
        }

        //may change to strategy pattern
        public Int16 HitBoxRadius
        {
            get { return hitBoxRadius; }
            set
            {
                if (value >= 0 && value <= 100)
                    hitBoxRadius = value;
            }
        }

        public Rectangle Boundaries
        {
            get { return boundaries; }
            set
            {
                if (value != null)
                    boundaries = value;
            }
        }
        
        //Methods
        public virtual void Move(InputState inputState)
        {
            List<Key> keysPressed = inputState.KeysPressed;
            ControlsConfig controls = inputState.Controls;

            //checks for the most recent movement key in an axis and updates player position
            if (keysPressed.IndexOf(controls.Up) > keysPressed.IndexOf(controls.Down))
            {
                CenterLocation = Vector2.Add(CenterLocation, new Vector2(0, Speed));
            }
            else if (keysPressed.IndexOf(controls.Down) > keysPressed.IndexOf(controls.Up))
            {
                CenterLocation = Vector2.Add(CenterLocation, new Vector2(0, -Speed));
            }

            if (keysPressed.IndexOf(controls.Left) > keysPressed.IndexOf(controls.Right))
            {
                CenterLocation = Vector2.Add(CenterLocation, new Vector2(-Speed, 0));
            }
            else if (keysPressed.IndexOf(controls.Right) > keysPressed.IndexOf(controls.Left))
            {
                CenterLocation = Vector2.Add(CenterLocation, new Vector2(Speed, 0));
            }
        }

        abstract public void Draw();

        private void ImportImage(int animationCell)
        {
            image = Image.FromFile(System.Environment.CurrentDirectory);
        }
    }
}
