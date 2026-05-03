using Platformer.Logic;
using System.Diagnostics;

namespace Platformer.Game
{
    internal class Player
    {
        public (int, int) targetDir = (0, 0);
        GameObject playerCollision;

        public void Movment()
        {
            int x = playerObj.Location.X + targetDir.Item1 * 2;
            int y = playerObj.Location.Y + targetDir.Item2 * 2;

            playerObj.Location = new Point(x, y);
            playerCollision.baseObject.Location = new Point(playerObj.Location.X, playerObj.Location.Y + playerObj.Size.Height - playerObj.Size.Width);
        }

        public void CheckCollision()
        {
            foreach (PictureBox obj in World.gameObjectsPicBoxes)
            {
                if (obj == playerObj) continue;
                if (obj == playerCollision.baseObject) continue;

                if (playerCollision.baseObject.Bounds.IntersectsWith(obj.Bounds)) 
                {

                    int overlapLeft = 0;
                    int overlapRight = 0;
                    int overlapTop = 0;
                    int overlapBottom = 0;

                    // player alsó ellenőrzése

                    overlapBottom = obj.Bottom - playerCollision.baseObject.Top;

                    // player felső ellenőrzése

                    overlapTop = playerObj.Bottom - obj.Top;

                    // player bal oldal ellenőrzése

                    overlapLeft = playerObj.Right - obj.Left;

                    // player jobb oldal ellenőrzése

                    overlapRight = obj.Right - playerObj.Left;

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

        public void Initial()
        {
            playerCollision = new(
                (playerObj.Location.X + (playerObj.Size.Width / 2), playerObj.Location.Y + playerObj.Size.Height - playerObj.Size.Width), 
                (playerObj.Size.Width, playerObj.Size.Width), true, formInstanceRef
            );

            playerCollision.formInstanceRef = formInstanceRef;
            playerCollision.baseObject.BackColor = Color.Gray;

            Time.Update += CheckCollision;
            Time.Update += Movment;
        }

        public Form1 formInstanceRef;
        public PictureBox playerObj;
    }
}