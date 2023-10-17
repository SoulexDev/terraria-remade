using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrariaRemade.Content.Engine;

namespace TerrariaRemade.Content.Scripts
{
    public class CameraController : Entity
    {
        private Camera cam;
        private float moveSpeed = 500;
        private TileCollider tileCollider = new TileCollider();
        public override void Awake()
        {
            cam = Camera.Instance;
            tileCollider.transform = transform;
            tileCollider.rectangle = new Rectangle((int)transform.position.X, (int)transform.position.Y, 100, 100);
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            Vector2 moveVector = Vector2.Zero;
            if (Input.GetKey(Keys.A) && !tileCollider.collidingLeft)
            {
                moveVector.X -= Time.deltaTime * moveSpeed;
            }
            if (Input.GetKey(Keys.D) && !tileCollider.collidingRight)
            {
                moveVector.X += Time.deltaTime * moveSpeed;
            }
            if (Input.GetKey(Keys.W) && !tileCollider.collidingUp)
            {
                moveVector.Y -= Time.deltaTime * moveSpeed;
            }
            if (Input.GetKey(Keys.S) && !tileCollider.collidingDown)
            {
                moveVector.Y += Time.deltaTime * moveSpeed;
            }

            Vector2 prevPos = transform.position;

            transform.position += moveVector;

            tileCollider.CheckCollisions(prevPos);

            cam.transform.position = transform.position;
        }
    }
}
