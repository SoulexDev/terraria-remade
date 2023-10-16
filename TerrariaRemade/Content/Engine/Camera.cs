using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    public class Camera : Entity
    {
        public static Camera Instance = new Camera();
        public Matrix Transform;

        public override void Awake()
        {
            
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            Transform = Matrix.CreateTranslation(-transform.position.X, -transform.position.Y, 0)
                * Matrix.CreateTranslation(GameRoot.ScreenSize.X / 2, GameRoot.ScreenSize.Y / 2, 0);
        }
    }
}
