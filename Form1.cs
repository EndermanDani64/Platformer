using Microsoft.VisualBasic.Devices;
using Platformer.Game;
using Platformer.Logic;
using System.Windows.Input;

namespace Platformer
{
    public partial class Form1 : Form
    {
        private Player _player;

        public int gravity;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;

            CreateObjects();
            SetInstanceRefs();

            Time.timerRef = mainTimer;
            Time.StartGameTime();
            Start();
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

        /// <summary>
        /// Main method to call stuff.
        /// </summary>
        private void Start()
        {
            GameObject ground = new((this.Size.Width / 2, this.Size.Height - 75), (900, 75));
            ground.formInstanceRef = this;
            ground.Initial();
            CreateObject(ground.gameObject);

            GameObject testywest = new((this.Size.Width / 2, this.Size.Height - 250), (50, 50));
            testywest.formInstanceRef = this;
            testywest.Initial();
            CreateObject(testywest.gameObject);
        }

        // event managed methods

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