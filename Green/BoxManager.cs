﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Green
{
    class BoxManager
    {
        public List<Box> Boxes { get; private set; } // List of boxes
        Texture2D smallBoxTexture;
        Texture2D bigBoxTexture;
        Rectangle screen;

        Random randomGen;

        Vector2 scale;

        float boxTimerElapsed;

        public BoxManager(ContentManager content, Vector2 scale, Rectangle screen)
        {
            Boxes = new List<Box>();
            this.screen = screen;
            this.scale = scale;

            randomGen = new Random();

            smallBoxTexture = content.Load<Texture2D>("smallBox");
            bigBoxTexture = content.Load<Texture2D>("bigBox");

        }

        public void Update(GameTime time)
        {
            MakeBoxes(time);
            for (int i = 0; i < Boxes.Count; i++)
            {
                Boxes[i].Update(time);
                if (Boxes[i].Position.X > screen.Right)
                {
                    Boxes[i].Kill();
                    Boxes.Remove(Boxes[i]);
                }

            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i=0; i<Boxes.Count;i++)
            {
                Boxes[i].Draw(spriteBatch);
            }
        }

        private void MakeBoxes(GameTime time)
        {
            float boxRate = 3f;
            boxTimerElapsed += (float)time.ElapsedGameTime.TotalSeconds;

            if(boxTimerElapsed > boxRate)
            {
                bool isBigBox;
                Texture2D newTexture;
                Vector2 newPosition;
                float boxChance = .5f;
                float roll = (float)randomGen.NextDouble();
                if (roll < boxChance)
                {
                    isBigBox = false;
                    newTexture = smallBoxTexture;
                    newPosition = new Vector2(0, 144);
                }
                else
                {
                    isBigBox = true;
                    newTexture = bigBoxTexture;
                    newPosition = new Vector2(0, 128);
                }

                Boxes.Add(new Box(newTexture, newPosition, isBigBox, scale));
                boxTimerElapsed = 0;
            }

        }

    }
}
