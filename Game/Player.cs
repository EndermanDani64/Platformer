using Platformer.Logic;
using System.Diagnostics;

namespace Platformer.Game
{
    internal class Player
    {
        bool _isAir = true;

        public (int, int) targetDir = (0, 0);

        float _elapsed = 0f;
        float _duration = .4f;
        Point _start;
        Point _target;
        bool _jumping = false;
        public void Movment()
        {
            //if (_isAir) return;

            if (targetDir.Item2 != 0 && !_jumping)
            {
                Debug.WriteLineIf(true, "1th enter");
                _start = playerObj.Location;
                _target = new Point(playerObj.Location.X + targetDir.Item1 * 23, playerObj.Location.Y + targetDir.Item2 * 15);
                _elapsed = 0f;
                _jumping = true;
            }
            else if (targetDir.Item2 != 0 && _jumping)
            {
                Debug.WriteLineIf(true, $"2th enter, dt: {Time.deltaTime}, elapsed: {_elapsed}");

                _elapsed += Time.deltaTime;
                float t = _elapsed / _duration;

                if (t >= 1f)
                {
                    t = 1f;
                    _jumping = false;
                }

                float smooth = t * (3f - 2f * t); // smoothstep

                int x = (int)(_start.X + (_target.X - _start.X) * smooth);
                int y = (int)(_start.Y + (_target.Y - _start.Y) * smooth);

                playerObj.Location = new Point(x + targetDir.Item2 * 3, y);
            }
            else // formInstanceRef.debugLogging 
            { 
                Debug.WriteLineIf(true, $"3th enter, targetDir = {targetDir}");

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