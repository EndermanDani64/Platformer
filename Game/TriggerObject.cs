using Platformer.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Game
{
    internal class TriggerObject : GameObject
    {
        public event System.Action OnTriggerEnter;

        public TriggerObject((int, int) location, (int, int) size, bool collision, GameObject playerColliderRef, Form1 formInstanceRef) 
            : base(location, size, collision, formInstanceRef)
        {
            Location = new(location.Item1, location.Item2);
            Size = new(size.Item1, size.Item2);

            baseObject.BackColor = Color.Red;

            this.playerColliderRef = playerColliderRef;
            Time.Update += CheckTrigger;
        }

        public void CheckTrigger()
        {
            Rectangle triggerRect = baseObject.Bounds;
            Rectangle playerRect = playerColliderRef.baseObject.Bounds;

            if (triggerRect.IntersectsWith(playerRect))
            {
                OnTriggerEnter?.Invoke();
                Debug.WriteLine("trigger has been triggered by the player");
            }
        }

        private GameObject playerColliderRef;
    }
}
