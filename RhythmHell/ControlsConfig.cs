using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using OpenTK.Input;

namespace RhythmHell
{
    class ControlsConfig
    {
        //Fields
        FileInfo file;
        string fileName;  //backing store

        //Constructors
        public ControlsConfig(string fileName)
        {
            this.FileName = fileName;
            this.file = new FileInfo(System.Environment.CurrentDirectory + "\\" + fileName + ".txt");
            ImportControls();   //imports controls upon initialization
        }

        //Properties
        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                file = new FileInfo(System.Environment.CurrentDirectory + "\\" + value + ".txt");
            }
        }

        //key map properties
        public Key Up { get; private set; }
        public Key Down { get; private set; }
        public Key Left { get; private set; }
        public Key Right { get; private set; }
        public Key Shoot { get; private set; }
        public Key Rewind { get; private set; }
        public Key Dilate { get; private set; }

        //Public Methods

        //Post: imports the controls from a text file
        public void ImportControls()
        {
            if (!file.Exists)
            {
                DefaultControls();  //create default controls text file
                ExportControls();
            }
            else
            {

                StreamReader streamReader = file.OpenText();
                try
                {
                    Up = (Key)Enum.Parse(typeof(Key), streamReader.ReadLine());
                    Down = (Key)Enum.Parse(typeof(Key), streamReader.ReadLine());
                    Left = (Key)Enum.Parse(typeof(Key), streamReader.ReadLine());
                    Right = (Key)Enum.Parse(typeof(Key), streamReader.ReadLine());
                    Shoot = (Key)Enum.Parse(typeof(Key), streamReader.ReadLine());
                    Rewind = (Key)Enum.Parse(typeof(Key), streamReader.ReadLine());
                    Dilate = (Key)Enum.Parse(typeof(Key), streamReader.ReadLine());
                }
                catch
                {
                    MessageBox.Show("ERROR: Controls data is corrupted.");
                    DefaultControls();
                    ExportControls();
                }
                finally
                {
                    streamReader.Close();
                }
            }
        }

        //exports the controls to a new text file
        public void ExportControls()
        {
            StreamWriter streamWriter = file.CreateText();

            try
            {
                streamWriter.WriteLine(Convert.ToString(Up));
                streamWriter.WriteLine(Convert.ToString(Down));
                streamWriter.WriteLine(Convert.ToString(Left));
                streamWriter.WriteLine(Convert.ToString(Right));
                streamWriter.WriteLine(Convert.ToString(Shoot));
                streamWriter.WriteLine(Convert.ToString(Rewind));
                streamWriter.WriteLine(Convert.ToString(Dilate));
            }
            catch
            {
                MessageBox.Show("ERROR: Failed to export controls to text file.");
            }
            finally
            {
                streamWriter.Close();
            }
        }

        public void ModifyControls(Key up, Key down, Key left, Key right, Key shoot, Key rewind, Key dilate)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
            Shoot = shoot;
            Rewind = rewind;
            Dilate = dilate;
            ExportControls();   //exports modified controls
        }

        //Private Methods

        //Post: defaults the controls
        private void DefaultControls()
        {
            Up = Key.Up;
            Down = Key.Down;
            Left = Key.Left;
            Right = Key.Right;
            Shoot = Key.Z;
            Rewind = Key.X;
            Dilate = Key.C;
        }
    }
}
