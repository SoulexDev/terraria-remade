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
        //private TileCollider tileCollider = new TileCollider();
        public override void Awake()
        {
            cam = Camera.Instance;
            renderer.sprite = TextureLoader.character;
            //tileCollider.transform = transform;
            transform.scale = Vector2.One * 3;
            transform.origin = renderer.size / 2;
            //tileCollider.rectangle = new Rectangle(transform.position.ToPoint(), (renderer.size * 3).ToPoint());
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

            Vector2 prevPos = transform.position;

            transform.position += moveVector;

            //tileCollider.CheckCollisions(prevPos);

            cam.transform.position = transform.position;
        }
    }
}
