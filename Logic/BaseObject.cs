using Platformer.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Logic
{
    internal class BaseObject
    {
        public PictureBox baseObject;

        private bool _colision;
        public bool Collision
        {
            get { return _colision; }
            set
            {
                _colision = value;
                World.gameObjectsPicBoxes.Remove(baseObject);
            }
        }

        public BaseObject(bool collision, Form1 formInstanceRef)
        {
            baseObject = new PictureBox();

            formInstanceRef.Controls.Add(baseObject);

            if (collision) { World.gameObjectsPicBoxes.Add(baseObject); }

            this.formInstanceRef = formInstanceRef;
        }

        public Form1 formInstanceRef;
    }
}
