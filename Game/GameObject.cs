using Platformer.Logic;
using Platformer.Logic.Base;
using Platformer.Logic.Fundamentals;
using System.Diagnostics;

namespace Platformer.Game
{
    internal class GameObject : BaseObject
    {
        private Point _location;
        public Point Location
        {
            get { return _location; }
            set
            {
                _location = value;
                baseObject.Location = value;
            }
        }

        private Size _size;
        public Size Size
        {
            get { return _size; }
            set
            {
                _size = value;
                baseObject.Size = value;
            }
        }

        private bool _colision = true;
        public bool Colision
        {
            get { return _colision; }
            set
            {
                _colision = value;

                if (value == false) World.gameObjectsPicBoxes.Remove(baseObject);
                else if (value == true && !World.gameObjectsPicBoxes.Contains(baseObject)) 
                {
                    World.gameObjectsPicBoxes.Add(baseObject);
                }
            }
        }

        float _ellapsed = 0;
        float _duration = -1;

        public GameObject((int, int) location, (int, int) size, bool collision, Form1 formInstanceRef) : base(collision, formInstanceRef)
        {
            Colision = collision;

            Location = new(location.Item1, location.Item2);
            Size = new(size.Item1, size.Item2);

            baseObject.BackColor = Color.Blue;

            World.gameObjects.Add(this);
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
            baseObject.Location = next;
        }
    }
}