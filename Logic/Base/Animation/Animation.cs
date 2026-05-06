using Platformer.Game;
using Platformer.Logic.Fundamentals;

namespace Platformer.Logic.Base.Animation
{
    internal class Animation
    {
        private string _spriteFolderPath = "";
        public string? SpriteFolderPath
        {
            get { return _spriteFolderPath; }
            set
            {
                if (value == null) _spriteFolderPath = "";
                else
                {
                    _spriteFolderPath = value.ToString();
                }
            }
        }

        private List<string> _spriteImgPaths = new();

        public PictureBox? picObject;
        public GameObject? gameObject;

        private bool _isPlaying = false;
        private bool _looped;
        private int _animationInterval = -1;
        private System.Windows.Forms.Timer _animTimer;

        public Animation(GameObject? gameObject, string spriteFolderPath, int animationInterval)
        {
            if (animationInterval <= 0 || Path.Exists(_spriteFolderPath)) return;

            this.gameObject = gameObject;
            this._animationInterval = animationInterval;

            SpriteFolderPath = spriteFolderPath;
            _spriteImgPaths = Directory.GetFiles(spriteFolderPath).ToList();
        }
        
        /// <summary>
        /// Plays the animation. You can set looping with the parameter "looping".
        /// </summary>
        /// <param name="looped">You can set looping with this.</param>
        public void Play(bool looped)
        {
            if (_isPlaying) return;

            _isPlaying = true;
            _looped = looped;
            _spriteImageIndexCounter = 0;

            _animTimer = new(); 

            _animTimer.Interval = _animationInterval;
            _animTimer.Start();
            _animTimer.Tick += AnimTick;

            Animator.ActiveAnimations.Add(this);
        }

        private int _spriteImageIndexCounter = 0;
        private void AnimTick(object? sender, EventArgs e)
        {
            SetImage(_spriteImgPaths[_spriteImageIndexCounter]);

            if (_spriteImageIndexCounter == _spriteImgPaths.Count - 1)
            {
                if (!_looped)
                {
                    Stop();
                    return;
                }
                _spriteImageIndexCounter = 0;
            }
            else
            {
                _spriteImageIndexCounter++;
            }
        }

        /// <summary>
        /// Stops the currently playing animation.
        /// </summary>
        public void Stop()
        {
            if (!_isPlaying) return;

            _animTimer.Stop();
            _animTimer.Tick -= AnimTick;
            _isPlaying = false;

            Animator.ActiveAnimations.Remove(this);
        }

        public bool IsPlaying() { return _isPlaying; }

        // submethods

        private void SetImage(string path)
        {
            gameObject.baseObject.ImageLocation = path;
        }
    }
}
