using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace RhythmHell
{
    class MainMenu : MenuBase
    {
        //Fields

        //Constructors
        public MainMenu()
            : base("Main Menu")
        {
            MenuEntry playMenuEntry = new MenuEntry("Play", new Vector2(-100, 200));
            MenuEntry optionMenuEntry = new MenuEntry("Options", new Vector2(-100, 140));
            MenuEntry leaderBoardsMenuEntry = new MenuEntry("Leaderboards", new Vector2(-100, 80));
            MenuEntry exitMenuEntry = new MenuEntry("Exit", new Vector2(-100, 20));

            //hook up events
            playMenuEntry.Selected += PlayMenuEntrySelected;
            optionMenuEntry.Selected += OptionMenuEntrySelected;
            leaderBoardsMenuEntry.Selected += LeaderBoardsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            //add menu entries to list
            menuEntries.Add(playMenuEntry);
            menuEntries.Add(optionMenuEntry);
            menuEntries.Add(leaderBoardsMenuEntry);
            menuEntries.Add(exitMenuEntry);
        }

        //Private Methods
        void PlayMenuEntrySelected(object sender, EventArgs e)
        {
            StateManager.AddState(new Game());
            StateManager.RemoveState(this);
        }

        void OptionMenuEntrySelected(object sender, EventArgs e)
        { 
            
        }

        void LeaderBoardsMenuEntrySelected(object sender, EventArgs e)
        { 
        
        }
    }
}
