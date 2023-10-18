using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    public static class Extensions
    {
        public static Color Add(this Color a, Color b)
        {
            Vector3 combinedValues = a.ToVector3() + b.ToVector3();
            return new Color(combinedValues.X, combinedValues.Y, combinedValues.Z);
        }
        public static Color Divide(this Color a, float b)
        {
            Vector3 dividedValues = a.ToVector3()/b;
            return new Color(dividedValues.X, dividedValues.Y, dividedValues.Z);
        }
    }
}