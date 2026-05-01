using Platformer.Logic;
using System;
using System.Collections.Generic;
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