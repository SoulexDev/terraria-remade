using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TerrariaRemade.Content.Engine;

namespace TerrariaRemade.Content.Scripts
{
    public class Block : Entity
    {
        public override void Awake()
        {
            renderer.sprite = TextureLoader.stoneBlock;
            transform.position = GameRoot.ScreenSize / 2;
            //transform.scale = Vector2.One * 200;
            transform.origin = renderer.size / 2;
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            
        }
    }
}
