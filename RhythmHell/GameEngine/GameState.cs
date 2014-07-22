using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace RhythmHell
{
    abstract class GameState
    {
        //Fields
        protected InputState inputState;
        protected Camera camera;

        /// <summary>
        /// Returns the statemanager the current game state belongs to.
        /// </summary>
        public StateManager StateManager { get; set; }

        //public methods to implement
        public virtual void Load() { }
        public virtual void Initialize() { }

        public virtual void Update() { }
        public virtual void Draw() { }

        public virtual void ProcessInput(InputState inputState) 
        {
            this.inputState = inputState;
        }

        public virtual void Free() { }
        public virtual void Unload() { }

        //removes state
        public void ExitState()
        {
            StateManager.RemoveState(this);
        }
    }
}
