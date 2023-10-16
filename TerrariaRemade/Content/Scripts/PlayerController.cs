using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrariaRemade.Content.Engine;

namespace TerrariaRemade.Content.Scripts
{
    public class PlayerController : Entity
    {
        private float moveSpeed = 3;
        private float jumpHeight = 2;
        private float gravity = 9.81f;

        public override void Awake()
        {

        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            Movement();
        }
        public void PlayerInput()
        {

        }
        public void Movement()
        {

        }
    }
}