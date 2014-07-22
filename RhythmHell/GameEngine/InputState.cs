using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace RhythmHell
{
    class InputState
    {
        //Fields
        List<Key> keysPressed;
        ControlsConfig controls;

        //Constructors
        public InputState(List<Key> keysPressed, ControlsConfig controls)
        {
            KeysPressed = keysPressed;
            Controls = controls;
            IsChanged = true;
        }

        //Properties
        public bool IsChanged { get; set; }     //must reset this value to false to track changes

        public List<Key> KeysPressed 
        {
            get { return keysPressed; }
            set { keysPressed = value; }
        }

        public ControlsConfig Controls 
        {
            get { return controls; }
            set { controls = value; }
        }

        //Public Methods
        public void AddKey(Key key)
        {
            if (!KeysPressed.Contains(key))
            {
                KeysPressed.Add(key);

                IsChanged = true;
            }
        }

        public void RemoveKey(Key key)
        {
            if (KeysPressed.Contains(key))
            {
                KeysPressed.Remove(key);

                IsChanged = true;
            }
        }
    }
}
