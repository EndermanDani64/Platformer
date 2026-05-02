using Platformer.Game;

namespace Platformer.Logic
{
    internal static class World
    {
        public static List<GameObject> Layer0;
        public static List<GameObject> Layer1;
        public static List<GameObject> Layer2;
        public static List<GameObject> Layer3;

        public static List<GameObject> gameObjects = new();
        public static List<PictureBox> gameObjectsPicBoxes = new();

        public static float gravity = 9.14f;

        public static void CreateGameObject()
        {
            GameObject newGO = new GameObject((0, 0), (50, 50));
            gameObjects.Add(newGO);
            gameObjectsPicBoxes.Add(newGO.gameObject);
        }

        public static int GetDistance(Point p1, Point p2)
        {
            return (int)Math.Sqrt((int)Math.Pow(Math.Abs(p2.Y - p1.Y), 2) + (int)Math.Pow(Math.Abs(p2.X - p1.X), 2));
        }


        /// <summary>
        /// Returns false if it doesn't collide with anything.
        /// </summary>
        public static bool CheckCollison(PictureBox targetObj)
        {
            foreach (PictureBox obj in gameObjectsPicBoxes)
            {
                if (obj == targetObj) continue;

                if (targetObj.Bounds.IntersectsWith(obj.Bounds))
                {
                    return true;
                }
                else return false;
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

        private static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }
    }
}