using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
        private int brushRadius = 1;
        public override void Awake()
        {
            
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            if (Input.GetKeyDown(Keys.E))
                brushRadius++;
            if(Input.GetKeyDown(Keys.Q))
                brushRadius--;

            if (Input.GetMouse("LeftButton"))
            {
                TileMap map = TileManager.GetLocalMap((int)Input.MousePosition.X);
                if (map == null)
                    return;
                Vector2 tilePos = map.ScreenToTilePosition(Input.MousePosition);

                for (int x = -brushRadius; x < brushRadius; x++)
                {
                    for (int y = -brushRadius; y < brushRadius; y++)
                    {
                        if (map.TileExists((int)tilePos.X + x, (int)tilePos.Y + y))
                            map.FillTile((int)tilePos.X + x, (int)tilePos.Y + y, 0);
                    }
                }
            }
            if (Input.GetMouse("RightButton"))
            {
                TileMap map = TileManager.GetLocalMap((int)Input.MousePosition.X);
                if (map == null)
                    return;
                Vector2 tilePos = map.ScreenToTilePosition(Input.MousePosition);

                for (int x = -brushRadius; x < brushRadius; x++)
                {
                    for (int y = -brushRadius; y < brushRadius; y++)
                    {
                        if (!map.TileExists((int)tilePos.X + x, (int)tilePos.Y + y))
                            map.FillTile((int)tilePos.X + x, (int)tilePos.Y + y, 3);
                    }
                }
            }
        }
    }
}