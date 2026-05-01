using Microsoft.VisualBasic.Devices;
using Platformer.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platformer.Game
{
    internal class Player
    {
        int _gravity = 1;
        bool _isAir = true;
        bool _jumpCooldown = false;

        public (int, int) targetDir = (0, 0);

        float elapsed = 0f;
        float duration = 6f;
        Point start;
        Point target;
        bool jumping = false;
        public void Movment()
        {
            //if (_isAir) return;

            int x = playerObj.Location.X + targetDir.Item1 * 3;
            int y = playerObj.Location.Y;
            //GameObject t = null;

            if (!_jumpCooldown && targetDir.Item2 != 0 /*&& !World.CheckCollison(playerObj)*/) // ide ez nem szükséges szerintem
            {
                Debug.WriteLine("1th enter");
                start = playerObj.Location;
                target = new Point(x, playerObj.Location.Y + targetDir.Item2 * 15);
                elapsed = 0f;
                jumping = true;

                /*y = playerObj.Location.Y + targetDir.Item2 * 15;

                _jumpCooldown = true;
                Time.Timeout(() => { _jumpCooldown = false; }, 600);*/
            }
            else if (jumping)
            {
                Debug.WriteLine("2th enter");
                3
                elapsed += Time.deltaTime;
                float t = elapsed / duration;

                if (t >= 1f)
                {
                    t = 1f;
                    jumping = false;
                }

                float eased = t * t * (3f - 2f * t); // smoothstep

                int newX = (int)(start.X + (target.X - start.X) * eased);
                int newY = (int)(start.Y + (target.Y - start.Y) * eased);

                playerObj.Location = new Point(newX, newY);
            }
            else
            {
                Debug.WriteLine("3th enter");
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