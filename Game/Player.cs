using Platformer.Logic.Fundamentals;
using System.Windows.Forms;

namespace Platformer.Game
{
    internal class Player
    {
        public (int, int) movingDir = (0, 0);
        public (int, int) targetDir = (0, 1);

        public GameObject playerCollision;
        public GameObject interactionCollision;
        public void Initial()
        {
            playerCollision = new(
                (playerObj.Location.X + (playerObj.Size.Width / 2), playerObj.Location.Y + playerObj.Size.Height - playerObj.Size.Width), 
                (playerObj.Size.Width, playerObj.Size.Width), true, formInstanceRef
            );

            interactionCollision = new(
                (playerObj.Location.X + (targetDir.Item1 * 45), (playerObj.Location.Y + ((playerObj.Size.Height / 2) / 2)) + (targetDir.Item2 * 60)), 
                (playerObj.Size.Width, playerObj.Size.Width), false, formInstanceRef
            );

            playerObj.SizeMode = PictureBoxSizeMode.CenterImage;

            playerCollision.baseObject.BackColor = Color.Gray;

            Time.Update += CheckCollision;
            Time.Update += Movment;
            Time.Update += UpdateBackground;
        }

        private void Movment()
        {
            int x = playerObj.Location.X + movingDir.Item1 * 2;
            int y = playerObj.Location.Y + movingDir.Item2 * 2;

            playerObj.Location = new Point(x, y);
            playerCollision.baseObject.Location = new Point(playerObj.Location.X, playerObj.Location.Y + playerObj.Size.Height - playerObj.Size.Width);
            interactionCollision.baseObject.Location = new Point(playerObj.Location.X + (targetDir.Item1 * 45), (playerObj.Location.Y + ((playerObj.Size.Height / 2) / 2)) + (targetDir.Item2 * 60));
        }

        private void CheckCollision()
        {
            foreach (PictureBox obj in World.gameObjectsPicBoxes)
            {
                if (obj == playerObj) continue;
                if (obj == playerCollision.baseObject) continue;

                if (playerCollision.baseObject.Bounds.IntersectsWith(obj.Bounds)) 
                {
                    // player alsó ellenőrzése

                    int overlapBottom = obj.Bottom - playerCollision.baseObject.Top;

                    // player felső ellenőrzése

                    int overlapTop = playerObj.Bottom - obj.Top;

                    // player bal oldal ellenőrzése

                    int overlapLeft = playerObj.Right - obj.Left;

                    // player jobb oldal ellenőrzése

                    int overlapRight = obj.Right - playerObj.Left;

                    int min = Math.Min(
                        Math.Min(overlapLeft, overlapRight),
                        Math.Min(overlapTop, overlapBottom)
                    );

                    if (min == overlapTop)
                    {
                        // player felülről érkezett (ráesett)
                        playerObj.Top -= overlapTop;
                    }
                    else if (min == overlapBottom)
                    {
                        // player alulról ment bele
                        playerObj.Top += overlapBottom;
                    }
                    else if (min == overlapLeft)
                    {
                        // player balról ment bele (jobbra mozgott)
                        playerObj.Left -= overlapLeft;
                    }
                    else if (min == overlapRight)
                    {
                        // player jobbról ment bele (balra mozgott)
                        playerObj.Left += overlapRight;
                    }
                }
            }
        }

        /*private PictureBox _lastBackgroundChild;
        private void UpdateBackground()
        {
            if (World.gameObjects.Count == 0) return;

            foreach (GameObject current in World.gameObjects)
            {
                //if (_lastBackgroundChild == current.baseObject) continue;

                if (playerObj.Bounds.IntersectsWith(current.baseObject.Bounds))
                {
                    playerObj.Controls.Add(current.baseObject);
                    current.baseObject.Location = new Point(0, 0);
                    current.baseObject.BackColor = Color.Transparent;

                    _lastBackgroundChild = current.baseObject;
                }
            }
        }*/

        private void UpdateBackground()
        {
            if (playerObj.Image == null) return;

            Bitmap bitmap0 = new Bitmap(playerObj.Image);
            var bt = MakeTransparent(bitmap0, Color.Transparent, 30);
            playerObj.Image = bt;
        }

        private Bitmap MakeTransparent(Bitmap bitmap, Color color, int tolerance)
        {
            Bitmap transparentImage = new Bitmap(bitmap);

            for (int i = transparentImage.Size.Width - 1; i >= 0; i--)
            {
                for (int j = transparentImage.Size.Height - 1; j >= 0; j--)
                {
                    var currentColor = transparentImage.GetPixel(i, j);
                    if (Math.Abs(color.R - currentColor.R) < tolerance &&
                      Math.Abs(color.G - currentColor.G) < tolerance &&
                      Math.Abs(color.B - currentColor.B) < tolerance)
                        transparentImage.SetPixel(i, j, color);
                }
            }

            transparentImage.MakeTransparent(color);
            return transparentImage;
        }

        public Form1 formInstanceRef;
        public PictureBox playerObj;
    }
}