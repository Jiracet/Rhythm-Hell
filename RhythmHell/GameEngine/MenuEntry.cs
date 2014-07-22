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
    public delegate void SelectedEventHandler(object sender, EventArgs e);

    class MenuEntry
    {
        //Constructors
        public MenuEntry(string text, Vector2 position)
        {
            Text = text;
            Position = position;
        }

        //Properties
        public string Text { get; set; }
        public Vector2 Position { get; set; }

        //Events
        public event SelectedEventHandler Selected; //invokes when menu entry is selected

        protected internal virtual void OnSelectEntry(EventArgs e)
        {
            if (Selected != null)
                Selected(this, e);
        }

        public void Update(bool isSelected)
        { 
            
        }

        public virtual void Draw(bool isSelected)
        {
            GL.Begin(PrimitiveType.Quads);

            if (!isSelected)
                GL.Color3(Color.AliceBlue);
            else
                GL.Color3(Color.Blue);

            GL.Vertex3(Position.X, Position.Y, 0);
            GL.Vertex3(Position.X + 200, Position.Y, 0);
            GL.Vertex3(Position.X + 200, Position.Y - 50, 0);
            GL.Vertex3(Position.X, Position.Y - 50, 0);
            GL.End();
        }

    }
}
