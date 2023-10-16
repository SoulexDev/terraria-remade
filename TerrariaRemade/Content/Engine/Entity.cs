using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    public abstract class Entity
    {
        public Transform transform = new Transform();
        public Renderer renderer = new Renderer();

        public bool isExpired;

        public abstract void Awake();
        public abstract void Start();
        public abstract void Update();
    }
}