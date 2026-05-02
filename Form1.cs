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

        public void CreateObject(PictureBox obj)
        {
            this.Controls.Add(obj);
        }

        // ##################################################################################### //

        GameObject ground;
        GameObject testywest;

        /// <summary>
        /// Main method to call stuff.
        /// </summary>
        private void Start()
        {
            ground = new((this.Size.Width / 2, this.Size.Height - 75), (900, 75));
            ground.formInstanceRef = this;
            ground.Initial();
            CreateObject(ground.gameObject);

            testywest = new((this.Size.Width / 2, this.Size.Height - 250), (50, 50));
            testywest.formInstanceRef = this;
            testywest.Initial();
            CreateObject(testywest.gameObject);
        }

        private void Update()
        {
            Time.Update += () => { testywest.MoveTo(new Point(100, 100), 8); };
        }

        // event managed methods


        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                e.IsInputKey = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) _player.targetDir.Item2 = 0;

            if (e.KeyCode == Keys.Left) _player.targetDir.Item1 = 0;
            else if (e.KeyCode == Keys.Right) _player.targetDir.Item1 = 0;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) _player.targetDir.Item2 = -1;

            if (e.KeyCode == Keys.Left) _player.targetDir.Item1 = -1;
            else if (e.KeyCode == Keys.Right) _player.targetDir.Item1 = 1;
        }
    }
}