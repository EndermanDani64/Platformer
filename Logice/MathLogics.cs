using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Logice
{
    internal static class MathLogics
    {
        public static float Lerp(float start, float end, float t)
        {
            return start + (end - start) * t;
        }
        public static float SmoothLerp(float start, float end, float t)
        {
            t = Math.Clamp(t, 0f, 1f);

            float smoothT = 3*t*t - 2*t*t*t;

            return Lerp(start, end, smoothT);
        }
    }
}
