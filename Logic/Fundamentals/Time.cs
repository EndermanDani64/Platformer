using Platformer.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Platformer.Logic.Fundamentals
{
    internal static class Time
    {
        private static bool running = false;
        public static float time { get; private set; }
        public static float deltaTime { get; private set; }

        private static DateTime lastFrame;

        public static event Action Update;

        // main methods

        /// <summary>
        /// Waits x miliseconds and then it runs the function.
        /// </summary>
        public static async void DelayCall(Action onCompletion, int ms)
        {
            await Task.Delay(ms);
            onCompletion();
        }

        /// <summary>
        /// Gets a void returning function, and when the time is up, it runs the function.
        /// </summary>
        public static async void Sleep(int ms)
        {
            await Task.Delay(ms);
            return;
        }

        // base methods

        public static void StartGameTime()
        {
            if (running) return;
            timerRef.Interval = 1;
            timerRef.Start();

            timerRef.Tick += UpdateTimeData;

            running = true;
        }
        public static void StopGameTime()
        {
            if (!running) return;
            timerRef.Stop();
            running = false;
        }

        // submethods

        private static void UpdateTimeData(object o, EventArgs e)
        {
            DateTime now = DateTime.Now;

            if (lastFrame != default)
            {
                deltaTime = (float)(now - lastFrame).TotalSeconds;
                time += deltaTime;
            }

            lastFrame = now;
            Update?.Invoke();
        }

        // references

        public static System.Windows.Forms.Timer timerRef;
    }
}
