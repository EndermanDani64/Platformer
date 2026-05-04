using Platformer.Game;
using System.Diagnostics;

namespace Platformer.Logic
{
    internal static class World
    {
        public static int unitSquareSize = 50;

        public static List<GameObject> Layer0;
        public static List<GameObject> Layer1;
        public static List<GameObject> Layer2;
        public static List<GameObject> Layer3;

        public static List<GameObject> gameObjects = new();
        public static List<PictureBox> gameObjectsPicBoxes = new();

        public static int GetDistance(Point p1, Point p2)
        {
            return (int)Math.Sqrt((int)Math.Pow(Math.Abs(p2.Y - p1.Y), 2) + (int)Math.Pow(Math.Abs(p2.X - p1.X), 2));
        }

        /// <summary>
        /// Returns false if it doesn't collide with anything.
        /// </summary>
        public static bool CheckCollison(Point targetPoint, ref GameObject exeption)
        {
            foreach (PictureBox obj in gameObjectsPicBoxes)
            {
                Rectangle currentObjRect = new(obj.Location, obj.Size);

                return currentObjRect.Contains(targetPoint);
            }
            return false;
        }

        /// <summary>
        /// Checks all the World Layers and if 
        /// </summary>
        public static bool IsSameLayer(GameObject object1, GameObject object2)
        {
            if (Layer0.Contains(object1) && Layer0.Contains(object2)) return true;
            else if (Layer1.Contains(object1) && Layer1.Contains(object2)) return true;
            else if (Layer2.Contains(object1) && Layer2.Contains(object2)) return true;
            else if (Layer3.Contains(object1) && Layer3.Contains(object2)) return true;
            return false;
        }

        public static Form1 formInstanceRef;
    }
}