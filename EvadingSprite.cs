﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TSAUMDHG
{
    class EvadingSprite : Sprite
    {
        // Save a reference to the sprite manager to
        // use to get the player position
        SpriteManager spriteManager;

        //Evasion range variables
        float evasionSpeedModifier;
        int evasionRange;
        bool evade = false;

        public EvadingSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, Point collisionOffset, Point currentFrame,
            Point sheetSize, Vector2 speed, string collisionCueName,
            SpriteManager spriteManager, float evasionSpeedModifier,
            int evasionRange, int scoreValue, Color color)
            : base(textureImage, position, frameSize, collisionOffset,
            currentFrame, sheetSize, speed, collisionCueName, scoreValue, 0f, 0f, Vector2.Zero, color)
        {
            this.spriteManager = spriteManager;
            this.evasionSpeedModifier = evasionSpeedModifier;
            this.evasionRange = evasionRange;
        }
        public EvadingSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, Point collisionOffset, Point currentFrame,
            Point sheetSize, Vector2 speed, int millisecondsPerFrame,
            string collisionCueName, SpriteManager spriteManager,
            float evasionSpeedModifier, int evasionRange,
            int scoreValue, Color color)
            : base(textureImage, position, frameSize, collisionOffset,
            currentFrame, sheetSize, speed, millisecondsPerFrame,
            collisionCueName, scoreValue, 0f, 0f, Vector2.Zero, color)
        {
            this.spriteManager = spriteManager;
            this.evasionSpeedModifier = evasionSpeedModifier;
            this.evasionRange = evasionRange;
        }

        public override Vector2 GetDirection()
        {
            return speed;
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            // First, move the sprite along its direction vector
            position += speed;

            // Use the player position to move the sprite closer in
            // the X and/or Y directions
            Vector2 player = spriteManager.GetPlayerPosition();
            
            if (evade)
            {
                // Move away from the player horizontally
                if (player.X < position.X)
                    position.X += Math.Abs(speed.Y);
                else if (player.X > position.X)
                    position.X -= Math.Abs(speed.Y);
            
                // Move away from the player vertically
                if (player.Y < position.Y)
                    position.Y += Math.Abs(speed.X);
                else if (player.Y > position.Y)
                    position.Y -= Math.Abs(speed.X);
            }
            else
            {
                if (Vector2.Distance(position, player) < evasionRange)
                {
                    // Player is within evasion range,
                    // reverse direction and modify speed
                    speed *= -evasionSpeedModifier;
                    evade = true;
                }
            }
            base.Update(gameTime, clientBounds);
        }
    }
}