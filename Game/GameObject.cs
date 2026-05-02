using Platformer.Logic;
using Platformer.Logice;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Game
{
    internal class GameObject
    {
        public Point Location;
        public Size Size;
        public bool kinematic = false;

        int _gravity = 0;
        bool _isAir = false;
        float _ellapsed = 0;
        float _duration = -1;

        public GameObject((int, int) location, (int, int) size)
        {
            gameObject = new PictureBox();
            Point _location = new(location.Item1 - size.Item1 / 2, location.Item2 - size.Item2 / 2);
            Location = _location;
            gameObject.Location = _location;
            Size _size = new(size.Item1, size.Item2);
            Size = _size;
            gameObject.Size = _size;
            gameObject.BackColor = Color.Blue;
        }

        private Point startLoc;
        private bool isMoving = false;
        /// <summary>
        /// Usable with the event Update.
        /// </summary>
        public void MoveTo(Point target, float duration)
        {
            if (_duration == -1) 
            { 
                _duration = duration;
                isMoving = true;
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

        public void ApplyGravity()
        {
            if (!_isAir && !kinematic) return;

            _gravity++;
            gameObject.Location = new Point(gameObject.Location.X, _gravity);
        }
        public void CheckIsAir()
        {
            //if (!kinematic) return;

            foreach (PictureBox obj in World.gameObjectsPicBoxes)
            {
                if (gameObject.Bounds.IntersectsWith(obj.Bounds)/* && _gravity > 0*/)
                {
                    _isAir = false;

                    if (!kinematic) return;

                    _gravity = 0;
                    gameObject.Top = obj.Height - gameObject.Height;
                }
                else _isAir = true;
            }
        }
        public void Initial()
        {
            World.gameObjectsPicBoxes.Add(gameObject);
            Time.Update += ApplyGravity;
            Time.Update += CheckIsAir;
        }

        public Form1 formInstanceRef;
        public PictureBox gameObject;
    }
}