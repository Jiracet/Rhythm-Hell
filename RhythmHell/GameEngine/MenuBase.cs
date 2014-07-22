using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RhythmHell
{
    class MenuBase : GameState
    {
        //Fields
        protected List<MenuEntry> menuEntries = new List<MenuEntry>();
        protected int selectedEntry = 0, inputBuffer;
        protected string menuTitle;
        protected bool wrapSelect = false;

        //Constructor
        public MenuBase(string title)
        {
            menuTitle = title;
            Initialize();
        }

        protected virtual void OnCancel(object sender, EventArgs e)
        {
            ExitState();
        }

        //Public Methods
        public override void Load()
        { 
        
        }

        public override void Initialize()
        {
            camera = new Camera(new Vector3(0, 0, 300), -Vector3.UnitZ, Vector3.UnitY);
            inputBuffer = 30;
        }

        public override void Update()
        {
            if (inputBuffer <= 0)  //when input buffer is 0, ready for next input
            {
                HandlePlayerInput();
            }
            else
                inputBuffer--;
        }

        public override void Draw()
        {
            Matrix4 modelview = camera.View;
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            for (int i = 0; i < menuEntries.Count; i++)
            {
                menuEntries[i].Draw(i == selectedEntry);
            }
        }

        public override void ProcessInput(InputState inputState)
        {
            base.ProcessInput(inputState);
            HandlePlayerInput();
            inputBuffer = 30;
        }

        public override void Free()
        { 
        
        }

        public override void Unload()
        { 
        
        }

        protected void HandlePlayerInput()
        {
            //placed in process input so option switch only happens when a key action has occurred
            if (inputState.KeysPressed.IndexOf(inputState.Controls.Up) < inputState.KeysPressed.IndexOf(inputState.Controls.Down))
            {
                if (selectedEntry < menuEntries.Count - 1)
                    selectedEntry++;
                else if (wrapSelect && selectedEntry >= menuEntries.Count - 1)
                    selectedEntry = 0;
            }
            else if (inputState.KeysPressed.IndexOf(inputState.Controls.Down) < inputState.KeysPressed.IndexOf(inputState.Controls.Up))
            {
                if (selectedEntry > 0)
                    selectedEntry--;
                else if (wrapSelect && selectedEntry <= 0)
                    selectedEntry = menuEntries.Count - 1;
            }

            //fires menu entry selection event
            if (inputState.KeysPressed.Contains(inputState.Controls.Shoot))
            {
                OnSelectEntry(selectedEntry);   
            }
        }

        /// <summary>
        /// Handles when entry is selected.
        /// </summary>
        /// <param name="entryNumber"></param>
        protected virtual void OnSelectEntry(int entryNumber)
        {
            menuEntries[entryNumber].OnSelectEntry(new EventArgs());
        }

    }
}
