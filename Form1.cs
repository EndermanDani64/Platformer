using Microsoft.VisualBasic.Devices;
using Platformer.Game;
using Platformer.Logic;
using System.Diagnostics;
using System.Windows.Input;

namespace Platformer
{
    public partial class Form1 : Form
    {
        private Player _player;

        public bool debugLogging = false;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;

            CreateObjects();
            SetInstanceRefs();

            Time.timerRef = mainTimer;
            Time.StartGameTime();
            Time.Update += Update;
            Start();

            debugCheckBox.CheckedChanged += (s, e) => { debugLogging = debugCheckBox.Checked; };
        }

        /// <summary>
        /// Creates all the necesarry classes.
        /// </summary>
        private void CreateObjects()
        {
            _player = new();
        }

        /// <summary>
        /// Sets the references to this object for the other objects that have been created in the constructor.
        /// </summary>
        private void SetInstanceRefs()
        {
            _player.formInstanceRef = this;
            _player.playerObj = playerObj;
            _player.Initial();
        }

        public void CreateObject(ref GameObject mainObj, PictureBox picObj)
        {
            World.gameObjectsPicBoxes.Add(picObj);
            this.Controls.Add(picObj);
            mainObj.formInstanceRef = this;
        }

        // ##################################################################################### //

        GameObject ground;
        TriggerObject testywest;

        /// <summary>
        /// Main method to call stuff.
        /// </summary>
        private void Start() // ZZZ...
        {
            ground = new((this.Size.Width / 2, this.Size.Height - 75), (900, 75), true);
            /*ground.formInstanceRef = this;
            ground.Initial();*/
            CreateObject(ref ground, ground.gameObject);

            testywest = new((this.Size.Width / 2, this.Size.Height - 250), (100, 50), false, _player);
            /*testywest.formInstanceRef = this;
            testywest.Initial();*/
            CreateObject(ref testywest, testywest.gameObject);
        }

        private void Update()
        {
            //Time.Update += () => { testywest.MoveTo(new Point(100, 100), 1048); };
        }

        // event managed methods

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) 
            { 
                _player.targetDir.Item2 = 0;
                Debug.WriteLine($"keyup, Up targetDir = {_player.targetDir}");
            }
            else if (e.KeyCode == Keys.S)
            {
                _player.targetDir.Item2 = 0;
                Debug.WriteLine($"keyDOWN, Down targetDir = {_player.targetDir}");
            }

            if (e.KeyCode == Keys.A) 
            { 
                _player.targetDir.Item1 = 0;
                Debug.WriteLine($"keyup, Left targetDir = {_player.targetDir}");
            }
            else if (e.KeyCode == Keys.D) 
            { 
                _player.targetDir.Item1 = 0;
                Debug.WriteLine($"keyup, Right targetDir = {_player.targetDir}");
            }
        }

        /*private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                _player.targetDir.Item2 = -1;
                Debug.WriteLine($"keyDOWN, Up targetDir = {_player.targetDir}");
            }
            else if (e.KeyCode == Keys.Down)
            {
                _player.targetDir.Item2 = 1;
                Debug.WriteLine($"keyDOWN, Down targetDir = {_player.targetDir}");
            }

            if (e.KeyCode == Keys.Left) 
            { 
                _player.targetDir.Item1 = -1;
                Debug.WriteLine($"keyDOWN, Left targetDir = {_player.targetDir}");
            }   
            else if (e.KeyCode == Keys.Right) 
            { 
                _player.targetDir.Item1 = 1;
                Debug.WriteLine($"keyDOWN, Right targetDir = {_player.targetDir}");
            }
        }*/

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.W)
            {
                _player.targetDir.Item2 = -1;
                Debug.WriteLine($"keyDOWN, Up targetDir = {_player.targetDir}");
            }

            if (keyData == Keys.S)
            {
                _player.targetDir.Item2 = 1;
                Debug.WriteLine($"keyDOWN, Down targetDir = {_player.targetDir}");
            }

            if (keyData == Keys.A)
            {
                _player.targetDir.Item1 = -1;
                Debug.WriteLine($"keyDOWN, Left targetDir = {_player.targetDir}");
            }
            if (keyData == Keys.D)
            {
                _player.targetDir.Item1 = 1;
                Debug.WriteLine($"keyDOWN, Right targetDir = {_player.targetDir}");
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}