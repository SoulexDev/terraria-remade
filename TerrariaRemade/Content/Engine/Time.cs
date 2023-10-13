using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    public class Time
    {
        private static float _deltaTime;
        public static float deltaTime { get { return _deltaTime; } set { _deltaTime = value; } }
        public static float _time;
        public static float time { get { return _time; } set { _time = value; } }
    }
}
