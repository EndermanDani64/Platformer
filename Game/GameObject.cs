using Platformer.Logic;
using Platformer.Logice;
using System.Diagnostics;

namespace Platformer.Game
{
    internal class GameObject
    {
        private Point _location;
        public Point Location
        {
            get { return _location; }
            set
            {
                _location = value;
                gameObject.Location = value;
            }
        }

        private Size _size;
        public Size Size
        {
            get { return _size; }
            set
            {
                _size = value;
                gameObject.Size = value;
            }
        }

        private bool _colision = true;
        public bool Colision
        {
            get { return _colision; }
            set
            {
                _colision = value;
                World.gameObjectsPicBoxes.Remove(gameObject);
            }
        }

        float _ellapsed = 0;
        float _duration = -1;

        public GameObject((int, int) location, (int, int) size, bool collision)
        {
            gameObject = new PictureBox();

            Colision = collision;

            Location = new(location.Item1, location.Item2);
            Size = new(size.Item1, size.Item2);

            gameObject.BackColor = Color.Blue;
        }

        private Point startLoc;
        /// <summary>
        /// Usable with the event Update.
        /// </summary>
        public void MoveTo(Point target, float duration)
        {
            if (_duration == -1) 
            { 
                _duration = duration;
                startLoc = Location;
            }

            _ellapsed += Time.deltaTime;

            float t = _ellapsed / _duration;

            if (t > 25) { return; }

            Debug.WriteLineIf(formInstanceRef.debugLogging, $"_ellapsed: {_ellapsed} / _duration: {_duration} = {t}");

            int x = Convert.ToInt32(MathLogics.SmoothLerp(startLoc.X, target.X, t));
            int y = Convert.ToInt32(MathLogics.SmoothLerp(startLoc.Y, target.Y, t));

            Point next = new Point(x, y);
            gameObject.Location = next;
        }

        /*public void Initial()
        {
            if (Colision) World.gameObjectsPicBoxes.Add(gameObject);
        }*/

        public Form1 formInstanceRef;
        public PictureBox gameObject;
    }
}