using Platformer.Logic;
using System.Diagnostics;

namespace Platformer.Game
{
    internal class Player
    {
        int _gravity = 1;
        bool _isAir = true;
        bool _jumpCooldown = false;

        public (int, int) targetDir = (0, 0);

        float elapsed = 0f;
        float duration = .4f;
        Point start;
        Point target;
        bool jumping = false;
        bool temp = false;

        public void Movment()
        {
            //if (_isAir) return;

            if (!_jumpCooldown && targetDir.Item2 != 0 /*&& !World.CheckCollison(playerObj)*/) // ide ez nem szükséges szerintem
            {
                Debug.WriteLine("1th enter");
                start = playerObj.Location;
                target = new Point(playerObj.Location.X + targetDir.Item1 * 23, playerObj.Location.Y + targetDir.Item2 * 15);
                elapsed = 0f;
                jumping = true;
            }
            else if (jumping)
            {
                Debug.WriteLine($"2th enter, dt: {Time.deltaTime}, elapsed: {elapsed}");

                elapsed += Time.deltaTime;
                float t = elapsed / duration;

                if (t >= 1f)
                {
                    t = 1f;
                    jumping = false;
                }

                float smooth = t * (3f - 2f * t); // smoothstep

                int x = (int)(start.X + (target.X - start.X) * smooth);
                int y = (int)(start.Y + (target.Y - start.Y) * smooth);

                playerObj.Location = new Point(x + targetDir.Item2 * 3, y);
            }
            else
            {
                Debug.WriteLine("3th enter");

                int x = playerObj.Location.X + targetDir.Item1 * 3;
                int y = playerObj.Location.Y;

                playerObj.Location = new Point(x, y);
            }


        }

        public void CheckIsAir()
        {
            foreach (PictureBox obj in World.gameObjectsPicBoxes)
            {
                if (obj == playerObj) continue;

                if (playerObj.Bounds.IntersectsWith(obj.Bounds))
                {
                    int overlapLeft = 0;
                    int overlapRight = 0;
                    int overlapTop = 0;
                    int overlapBottom = 0;

                    // player alsó ellenőrzése

                    overlapBottom = obj.Bottom - playerObj.Top;

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

                        _isAir = false;
                        _gravity = 0;
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
                else _isAir = true;
            }
        }

        public void ApplyGravity()
        {
            if (!_isAir /*&& !_jumpCooldown*/) return;

            int y = playerObj.Location.Y;
            playerObj.Location = new Point(playerObj.Location.X, ++y);
        }

        public void Initial()
        {
            World.gameObjectsPicBoxes.Add(playerObj);
            Time.Update += ApplyGravity;
            Time.Update += CheckIsAir;
            Time.Update += Movment;
        }

        public Form1 formInstanceRef;
        public PictureBox playerObj;
    }
}