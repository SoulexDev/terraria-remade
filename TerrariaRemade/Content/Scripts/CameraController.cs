using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TerrariaRemade.Content.Engine;

namespace TerrariaRemade.Content.Scripts
{
    public class CameraController : Entity
    {
        private Camera cam;
        private float moveSpeed = 500;
        public override void Awake()
        {
            cam = Camera.Instance;
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            Vector2 moveVector = Vector2.Zero;
            if (Input.GetKey(Keys.A))
            {
                moveVector.X -= Time.deltaTime * moveSpeed;
            }
            if (Input.GetKey(Keys.D))
            {
                moveVector.X += Time.deltaTime * moveSpeed;
            }
            if (Input.GetKey(Keys.W))
            {
                moveVector.Y -= Time.deltaTime * moveSpeed;
            }
            if (Input.GetKey(Keys.S))
            {
                moveVector.Y += Time.deltaTime * moveSpeed;
            }

            transform.position += moveVector;

            cam.transform.position = transform.position;
        }
    }
}
