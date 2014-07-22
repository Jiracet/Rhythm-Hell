using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace RhythmHell
{
    class StateManager
    {
        //Fields
        List<GameState> states;

        public StateManager()
        {
            States = new List<GameState>();
        }

        public StateManager(List<GameState> states)
        {
            foreach (var state in states)
                state.StateManager = this;
            States = states;
        }

        //Properties
        public InputState InputState { get; set; }  //reference to inputstate for this statemanager
        public List<GameState> States 
        {
            get { return states; }
            set { states = value; }
        }

        //returns the state on top
        public GameState Top
        {
            get
            {
                if (States != null && States.Count > 0)
                    return States.ElementAt(States.Count - 1);
                else
                    return null;
            }
            private set { }
        }

        public void Update()
        {
            for (int i = states.Count - 1; i >= 0; i--)
            {
                states[i].Update();
            }
        }

        public void Draw()
        {
            for (int i = states.Count - 1; i >= 0; i--)
            {
                states[i].Draw();
            }
        }

        public void AddState(GameState state)
        {
            state.StateManager = this;  //shows that this state belongs to this statemanager
            state.ProcessInput(InputState); //initializes input
            States.Add(state);
        }

        public void RemoveState(GameState state)
        {
            if (States.Contains(state))
                States.Remove(state);
        }

    }
}
