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

        public bool collidingLeft;
        public bool collidingRight;
        public bool collidingUp;
        public bool collidingDown;

        private int collisionRadius = 5;

        private Vector2 delta;

        public void CheckCollisions(Vector2 prevPos)
        {
            collidingLeft = false;
            collidingRight = false;
            collidingUp = false;
            collidingDown = false;

            delta = transform.position - prevPos;
            rectangle.Location = new Point((int)prevPos.X, (int)prevPos.Y);

            for (int x = (int)prevPos.X - collisionRadius; x < collisionRadius + (int)prevPos.X; x++)
            {
                for (int y = (int)prevPos.Y - collisionRadius; y < collisionRadius + (int)prevPos.Y; y++)
                {
                    Vector2 tilePos = TileMap.ScreenToTilePosition(new Vector2(x, y));

                    if (!TileMap.TileExists((int)tilePos.X, (int)tilePos.Y))
                    {
                        return;
                    }

                    int tileSize = (int)(TileMap.tileSize * TileMap.scale);

                    Rectangle tileRect = new Rectangle((int)tilePos.X + tileSize / 2, (int)tilePos.Y + tileSize / 2, tileSize, tileSize);

                    collidingLeft = IsTouchingLeft(tileRect);
                    collidingRight = IsTouchingRight(tileRect);
                    collidingUp = IsTouchingUp(tileRect);
                    collidingDown = IsTouchingDown(tileRect);
                }
            }

            if (collidingLeft || collidingRight)
                transform.position.X = prevPos.X;
            if (collidingUp || collidingDown)
                transform.position.Y = prevPos.Y;
        }
        private bool IsTouchingRight(Rectangle otherRect)
        {
            return rectangle.Right + delta.X > otherRect.Left &&
              rectangle.Left < otherRect.Left;
        }

        private bool IsTouchingLeft(Rectangle otherRect)
        {
            return rectangle.Left + delta.X < otherRect.Right &&
              rectangle.Right > otherRect.Right;
        }

        private bool IsTouchingDown(Rectangle otherRect)
        {
            return rectangle.Bottom + delta.Y > otherRect.Top &&
              rectangle.Top < otherRect.Top;
        }

        private bool IsTouchingUp(Rectangle otherRect)
        {
            return rectangle.Top + delta.Y < otherRect.Bottom &&
              rectangle.Bottom > otherRect.Bottom;
        }
    }
}