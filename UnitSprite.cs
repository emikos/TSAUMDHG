﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TSAUMDHG
{
    class UnitSprite : Sprite
    {
        //Mouse movement
        //MouseState prevMouseState;
        int timeSinceLastFrame = 0;
        Point lastTile = new Point(Int32.MinValue, Int32.MinValue);

        public UnitSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, Vector2 origin, Point startTile)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, null, 0, 0f, origin)
        {
            this.lastTile = startTile;
        }
        public UnitSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, int millisecondsPerFrame, Vector2 origin, Point startTile)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame, null, 0, 0f, origin)
        {
            this.lastTile = startTile;
        }

        public override Vector2 direction
        {
            get { return speed; }
        }

        public Vector2 UpdatePosition(List<MapSprite> pathList)
        {
            MapSprite currentPathSprite = null;
            Vector2 newPosition = position;
            float offset = 0.0f;




            if (direction.X < 0 && position.Y % pathList[0].GetFrameSize.Y == 0)
            {
                currentPathSprite = null;
                for (int index = 0; index < pathList.Count; index++)
                {
                    offset = ((position.X + direction.X) - (pathList[index].GetCurrentFrame.X /*+ pathList[index].GetFrameSize.X*/));

                    if (((position.X - (position.X % pathList[index].GetFrameSize.X)) - pathList[index].GetFrameSize.X) == pathList[index].GetCurrentFrame.X &&
                          (position.Y - (position.Y % pathList[index].GetFrameSize.Y)) == pathList[index].GetCurrentFrame.Y)
                    {
                        currentPathSprite = pathList[index];
                    }
                }

                if (position.Y % pathList[0].GetFrameSize.Y == 0)
                {

                }
                if (currentPathSprite != null)
                {
                    newPosition.X += direction.X;
                }
                else
                {
                    offset = position.X % pathList[0].GetFrameSize.X;

                    if (offset > Math.Abs(direction.X))
                    {
                        newPosition.X += direction.X;
                    }
                    else
                    {
                        newPosition.X -= (position.X % pathList[0].GetFrameSize.X);
                    }

                }
            }

            if (direction.X > 0 && position.Y % pathList[0].GetFrameSize.Y == 0)
            {
                currentPathSprite = null;
                for (int index = 0; index < pathList.Count; index++)
                {
                    offset = ((position.X + pathList[index].GetFrameSize.X + direction.X) - (pathList[index].GetCurrentFrame.X /*+ pathList[index].GetFrameSize.X*/));

                    if (((position.X - (position.X % pathList[index].GetFrameSize.X)) + pathList[index].GetFrameSize.X) == pathList[index].GetCurrentFrame.X &&
                          (position.Y - (position.Y % pathList[index].GetFrameSize.Y)) == pathList[index].GetCurrentFrame.Y)
                    {
                        currentPathSprite = pathList[index];
                    }
                }

                if (currentPathSprite != null)
                {
                    newPosition.X += direction.X;
                }
                else
                {
                    offset = position.X % pathList[0].GetFrameSize.X;

                    if (offset > Math.Abs(direction.X))
                    {
                        newPosition.X += direction.X;
                    }
                    else
                    {
                        newPosition.X -= (position.X % pathList[0].GetFrameSize.X);
                    }

                }
            }

            if (direction.Y < 0 && position.X % pathList[0].GetFrameSize.X == 0)
            {
                currentPathSprite = null;
                for (int index = 0; index < pathList.Count; index++)
                {
                    if (((position.Y - (position.Y % pathList[index].GetFrameSize.Y)) - pathList[index].GetFrameSize.Y) == pathList[index].GetCurrentFrame.Y &&
                          (position.X - (position.X % pathList[index].GetFrameSize.X)) == pathList[index].GetCurrentFrame.X)
                    {
                        currentPathSprite = pathList[index];
                    }
                }

                if (currentPathSprite != null)
                {
                    newPosition.Y += direction.Y;
                }
                else
                {
                    offset = position.Y % pathList[0].GetFrameSize.Y;

                    if (offset > Math.Abs(direction.Y))
                    {
                        newPosition.Y += direction.Y;
                    }
                    else
                    {
                        newPosition.Y -= (position.Y % pathList[0].GetFrameSize.Y);
                    }

                }
            }

            if (direction.Y > 0 && position.X % pathList[0].GetFrameSize.X == 0)
            {
                currentPathSprite = null;
                for (int index = 0; index < pathList.Count; index++)
                {
                    if (((position.Y - (position.Y % pathList[index].GetFrameSize.Y)) + pathList[index].GetFrameSize.Y) == pathList[index].GetCurrentFrame.Y &&
                          (position.X - (position.X % pathList[index].GetFrameSize.X)) == pathList[index].GetCurrentFrame.X)
                    {
                        currentPathSprite = pathList[index];
                    }
                }

                if (currentPathSprite != null)
                {
                    newPosition.Y += direction.Y;
                }
                else
                {
                    offset = (position.Y) % pathList[0].GetFrameSize.Y;



                    if (offset > Math.Abs(direction.Y))
                    {
                        newPosition.Y += direction.Y;
                    }
                    else
                    {
                        newPosition.Y -= offset;
                    }

                }
            }

            return newPosition;
        }

        public void Update(GameTime gameTime, Rectangle clientBounds, List<MapSprite> pathList)
        {
            position = UpdatePosition(pathList);

            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if((timeSinceLastFrame > base.GetMillisecondsPerFrame) &&
               (direction.X != 0 || direction.Y != 0))
            {
                timeSinceLastFrame = 0;
                if (Math.Abs(direction.X) > Math.Abs(direction.Y))
                {
                    SetSpriteEffect(SpriteEffects.None);
                    if (direction.X < 0)
                    {
                        SetSpriteEffect(SpriteEffects.FlipHorizontally);
                    }

                    currentFrame.X++;
                    currentFrame.Y = 2;
                    if (currentFrame.X > GetSheetSize.X)
                    {
                        currentFrame.X = 0;
                    }
                }
                else
                {
                    if (direction.Y < 0)
                    {
                        currentFrame.Y = 0;
                    }
                    else
                    {
                        currentFrame.Y = 1;
                    }
                    currentFrame.X++;
                    if(currentFrame.X > GetSheetSize.X)
                    {
                        currentFrame.X = 0;
                    }
                }
            }
            //base.Update(gameTime, clientBounds);
        }
    }
}