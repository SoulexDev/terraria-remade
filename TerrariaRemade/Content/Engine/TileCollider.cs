using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    public class TileCollider
    {
        public Transform transform;
        public Rectangle rectangle;

        private int collisionRadius = 5;

        private Vector2 delta;

        public void CheckCollisions(Vector2 prevPos)
        {
            rectangle.Location = transform.position.ToPoint();
            //Rectangle prevRect = rectangle;

            //prevRect.Location = prevPos.ToPoint();

            //delta = transform.position - prevPos;
            //for (int x = (int)prevPos.X - collisionRadius; x < collisionRadius + (int)prevPos.X; x++)
            //{
            //    for (int y = (int)prevPos.Y - collisionRadius; y < collisionRadius + (int)prevPos.Y; y++)
            //    {
            //        Vector2 tilePos = TileMap.ScreenToTilePosition(new Vector2(x, y));

            //        if (!TileMap.TileExists((int)tilePos.X, (int)tilePos.Y))
            //        {
            //            continue;
            //        }

            //        int tileSize = (int)(TileMap.tileSize * TileMap.scale);

            //        Rectangle tileRect = new Rectangle((tilePos + (Vector2.One * tileSize / 2)).ToPoint(), 
            //            (Vector2.One * tileSize).ToPoint());

            //        //if (!rectangle.Intersects(tileRect))
            //        //    continue;

            //        CollisionSide colSide = CollisionHelperAABB.GetCollisionSide(prevRect, tileRect, delta);

            //        transform.position = CollisionHelperAABB.GetCorrectedLocation(rectangle, tileRect, colSide);
            //    }
            //}
            //return;
            delta = transform.position - prevPos;

            for (int x = (int)prevPos.X - collisionRadius; x < collisionRadius + (int)prevPos.X; x++)
            {
                for (int y = (int)prevPos.Y - collisionRadius; y < collisionRadius + (int)prevPos.Y; y++)
                {
                    Vector2 tilePos = TileMap.ScreenToTilePosition(new Vector2(x, y));

                    if (!TileMap.TileExists((int)tilePos.X, (int)tilePos.Y))
                    {
                        continue;
                    }

                    int tileSize = (int)(TileMap.tileSize * TileMap.scale);

                    Rectangle tileRect = new Rectangle(tilePos.ToPoint(),
                        (Vector2.One * tileSize).ToPoint());

                    if (IsTouchingLeft(tileRect))
                        transform.position.X = tileRect.Right + rectangle.Width / 2;
                    if (IsTouchingRight(tileRect))
                        transform.position.X = tileRect.Left - rectangle.Width / 2;
                    if (IsTouchingUp(tileRect))
                        transform.position.Y = tileRect.Bottom + rectangle.Height / 2;
                    if (IsTouchingDown(tileRect))
                        transform.position.Y = tileRect.Top - rectangle.Height / 2;
                }
            }
        }
        private bool IsTouchingRight(Rectangle otherRect)
        {
            return rectangle.Right > otherRect.Left &&
              rectangle.Left < otherRect.Left;
        }

        private bool IsTouchingLeft(Rectangle otherRect)
        {
            return rectangle.Left < otherRect.Right &&
              rectangle.Right > otherRect.Right;
        }

        private bool IsTouchingDown(Rectangle otherRect)
        {
            return rectangle.Bottom > otherRect.Top &&
              rectangle.Top < otherRect.Top;
        }

        private bool IsTouchingUp(Rectangle otherRect)
        {
            return rectangle.Top < otherRect.Bottom &&
              rectangle.Bottom > otherRect.Bottom;
        }
    }
}