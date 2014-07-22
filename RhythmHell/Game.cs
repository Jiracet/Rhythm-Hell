using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace RhythmHell
{
    class Game : GameState
    {
        #region Fields

        //fields
        //Engine fields
        Dictionary<int, List<Key>> keyHistory;    //records history of keypresses
        BulletPool bulletPool;
        
        //Time elements

        //Player fields
        BodyBase player1;
        bool isAlive;
        bool rewinding;
        BossBase boss1;

        #endregion

        #region Constructors

        //constructors
        public Game()
        {
            Initialize();
        }

        #endregion

        #region Properties

        public bool LevelRewinding { get; set; }
        public GameTime GameTime { get; set; }

        #endregion

        #region Public Methods

        List<IBulletPattern> patterns = new List<IBulletPattern>();
        IBulletPattern testPattern;
        IBulletPattern testPattern2;
        IBulletPattern testPattern3;

        public override void Load()
        { 
        
        }

        public override void Initialize()
        {
            //engine
            camera = new Camera(new Vector3(0, 0, 300), -Vector3.UnitZ, Vector3.UnitY);
            GameTime = new GameTime();
            bulletPool = new BulletPool(100, GameTime);

            //player
            player1 = new BodyDebug(new Vector2(0, -200), 4, 1, new Rectangle(-250, -300, 500, 600));
            isAlive = true;

            //boss
            boss1 = new BossDebug(new Vector2(0, 200), new BossSimpleBehavior(), bulletPool, GameTime);
            boss1.BulletPool = bulletPool;

            //bullets
            testPattern = new SpiralPattern(new Vector2(0, 100), 1, -1, 1, 18000, 180, GameTime);
            testPattern2 = new SpiralPattern(new Vector2(100, 100), 3, 1, 1, 1000, 180, GameTime);
            testPattern3 = new SpiralPattern(new Vector2(-100, 100), 3, 23, 1, 1000, 180, GameTime);

            testPattern2.BulletPool = bulletPool;
            testPattern3.BulletPool = bulletPool;

            //patterns.Add(testPattern);
            patterns.Add(testPattern2);
            patterns.Add(testPattern3);

            //initializes key history
            keyHistory = new Dictionary<int, List<Key>>();
            rewinding = false;
        }

        //updates the entire game one frame
        public override void Update()
        {
            if (isAlive)
            {
                //tracks if player is rewinding
                rewinding = (inputState.KeysPressed.Contains(inputState.Controls.Rewind));

                //case while not rewinding
                if (!rewinding)
                {
                    RecordPlayerData();

                    player1.Move(inputState);
                    boss1.Update();

                    if (inputState.KeysPressed.Contains(inputState.Controls.Dilate))
                    {
                        foreach (var bullet in bulletPool.Bullets)
                        {
                            bullet.TimeManipulationFactor *= 0.3f;
                        }
                    }

                    foreach (var pattern in patterns)
                    {
                        //pattern is reverse when level is rewinding
                        pattern.IsReverse = LevelRewinding;
                        pattern.Update();
                    }

                    bulletPool.IsReverse = LevelRewinding;
                    bulletPool.Update();

                    CheckForPlayerCollisions();

                    GameTime.Frame++;
                }
                else if (GameTime.Frame > 0)  //case when rewinding
                {
                    foreach (var pattern in patterns)
                    {
                        //pattern is reverse when level is not rewinding
                        pattern.IsReverse = !LevelRewinding;
                        pattern.Update();
                    }

                    if (keyHistory.Last().Value.Contains(inputState.Controls.Dilate))
                    {
                        foreach (var bullet in bulletPool.Bullets)
                        {
                            bullet.TimeManipulationFactor *= 0.3f;
                        }
                    }

                    bulletPool.IsReverse = !LevelRewinding;
                    bulletPool.Update();

                    //sets player position to position during specified frame
                    player1.CenterLocation = player1.PlayerPositionHistory[GameTime.Frame - 1];
                    player1.PlayerPositionHistory.Remove(GameTime.Frame - 1);

                    if (GameTime.Frame <= keyHistory.Last().Key + 1) //when current time is before the last record of input
                        keyHistory.Remove(keyHistory.Last().Key);   //removes most recent entry

                    GameTime.Frame--;
                }
                else
                {
                    //time freezes when rewinding at frame 0
                }


            }
        }

        //renders the entire game one frame
        public override void Draw()
        {
            Matrix4 modelview = camera.View;
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            player1.Draw();
            boss1.Draw();
            bulletPool.Draw();
        }

        public override void Free()
        { 
        
        }

        public override void Unload()
        { 
        
        }

        #endregion

        #region Private Methods

        private void RecordPlayerData()
        {
            //records player position every frame
            player1.PlayerPositionHistory.Add(GameTime.Frame, player1.CenterLocation);

            //when input changes, record state in key history with corresponding frame
            if (inputState.IsChanged)
            {
                if (!keyHistory.ContainsKey(GameTime.Frame))
                    keyHistory.Add(GameTime.Frame, StaticFactory.Clone(inputState.KeysPressed));
                inputState.IsChanged = false;   //reset isChanged property
            }
        }

        //checks for player collision with bullet
        private void CheckForPlayerCollisions()
        {
            foreach (var bullet in bulletPool.Bullets)
            {
                if (bullet.InUse)
                {
                    if (player1.HitBoxRadius + bullet.HitBoxRadius > Vector2.Subtract(player1.CenterLocation, bullet.CenterLocation).LengthFast)
                    {
                        MessageBox.Show("You lost the game. Better prompt coming later");
                        isAlive = false;
                    }
                }
            }
        }

        #endregion
    }
}
