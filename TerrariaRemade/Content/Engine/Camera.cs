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
        public float zoom = 1;
        private Rectangle frustum;
        private Vector2 borderedScreenSize => GameRoot.ScreenSize * 1.1f;

        public override void Awake()
        {
            frustum = new Rectangle(transform.position.ToPoint(), borderedScreenSize.ToPoint());
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            Transform = Matrix.CreateTranslation(-transform.position.X, -transform.position.Y, 0)
                * Matrix.CreateTranslation(GameRoot.ScreenSize.X / 2, GameRoot.ScreenSize.Y / 2, 0)
                * Matrix.CreateScale(zoom);

            frustum.Location = transform.position.ToPoint() - (borderedScreenSize / 2).ToPoint();
        }
        public bool IsInFrustum(Vector2 worldPosition)
        {
            return frustum.Contains(worldPosition);
        }
    }
}
