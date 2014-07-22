using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Media;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.IO;
using OpenTK.Audio.OpenAL;

namespace RhythmHell
{
    class GamePrototype : GameWindow
    {
        //Fields
        Game game = new Game();
        MainMenu menu = new MainMenu();

        StateManager stateManager = new StateManager();
        InputState inputState;

        //Constructors
        public GamePrototype()
            : base(500, 600, GraphicsMode.Default, "Rhythm Hell")
        { 
        
        }

        //Properties

        //Interfaces

        //Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);

            inputState = new InputState(new List<Key>(), new ControlsConfig("CONTROLS"));
            stateManager.InputState = inputState;   //syncs input with state manager

            //initializes with menu screen
            stateManager.AddState(menu);
        }

        #region Input

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            inputState.AddKey(e.Key);

            //updates current game state input
            if (stateManager != null)
                stateManager.Top.ProcessInput(inputState);
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            base.OnKeyUp(e);
            inputState.RemoveKey(e.Key);

            //updates current game state input
            if (stateManager != null)
                stateManager.Top.ProcessInput(inputState);
        }

        #endregion

        //resize method
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 2, Width / (float)Height, 1f, 1024.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection); 
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (stateManager.States.Count == 0)
                Exit();

            //progresses game one frame
            stateManager.Update();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //draws reference axes for debugging
            //GL.Begin(PrimitiveType.Lines);
            //GL.LineWidth(2);
            //GL.Color3(Color.Red);
            //GL.Vertex3(250, 0, 0);
            //GL.Vertex3(-250, 0, 0);
            //GL.End();

            //GL.Begin(PrimitiveType.Lines);
            //GL.LineWidth(2);
            //GL.Color3(Color.Blue);
            //GL.Vertex3(0, 300, 0);
            //GL.Vertex3(0, -300, 0);
            //GL.End();

            //GL.Begin(PrimitiveType.Lines);
            //GL.LineWidth(2);
            //GL.Color3(Color.Green);
            //GL.Vertex3(0, 0, 100);
            //GL.Vertex3(0, 0, -100);
            //GL.End();

            //renders the game
            stateManager.Draw();

            SwapBuffers();  //swaps buffers
        }

        //Public Methods

    }
}

