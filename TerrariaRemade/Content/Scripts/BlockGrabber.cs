using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrariaRemade.Content.Engine;

namespace TerrariaRemade.Content.Scripts
{
    public class BlockGrabber : Entity
    {
        public override void Awake()
        {
            
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            if (Input.GetMouse("LeftButton"))
            {
                Vector2 tilePos = TileMap.ScreenToTilePosition(Input.MousePosition);
                if (TileMap.TileExists((int)tilePos.X, (int)tilePos.Y))
                    TileMap.FillTile((int)tilePos.X, (int)tilePos.Y, 0);
            }
            if (Input.GetMouse("RightButton"))
            {
                Vector2 tilePos = TileMap.ScreenToTilePosition(Input.MousePosition);
                if (TileMap.TileInBounds((int)tilePos.X, (int)tilePos.Y) && !TileMap.TileExists((int)tilePos.X, (int)tilePos.Y))
                    TileMap.FillTile((int)tilePos.X, (int)tilePos.Y, 1);
            }
        }
    }
}