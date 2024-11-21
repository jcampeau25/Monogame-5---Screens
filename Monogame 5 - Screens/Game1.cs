using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame_5___Screens
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window;

        Texture2D tribbleGreyTexture, tribbleCreamTexture, tribbleBrownTexture, tribbleOrangeTexture, spaceTexture, tribbleIntroTexture;

        Rectangle tribbleGreyRect, tribbleCreamRect, tribbleBrownRect, tribbleOrangeRect, backgroundRect;

        Vector2 tribbleGreySpeed, tribbleBrownSpeed, tribbleOrangeSpeed, tribbleCreamSpeed;

        SpriteFont bouncefont, introfont;

        Random generator = new Random();

        Color tribbleColour = Color.White;

        float seconds;

        int bounces = 0;
        enum Screen
        {
            Intro,
            TribbleYard
        }

        Screen screen;

        MouseState mouseState;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            window = new Rectangle(0, 0, 800, 500);
            tribbleGreyRect = new Rectangle(generator.Next(1, 700), generator.Next(1, 400), 100, 100);
            tribbleGreySpeed = new Vector2(6, 6);
            tribbleBrownRect = new Rectangle(generator.Next(1, 700), generator.Next(1, 400), 100, 100);
            tribbleBrownSpeed = new Vector2(10, 3);
            tribbleCreamRect = new Rectangle(generator.Next(1, 700), generator.Next(1, 400), 100, 100);
            tribbleCreamSpeed = new Vector2(8, 0);
            tribbleOrangeRect = new Rectangle(generator.Next(1, 700), generator.Next(1, 400), 100, 100);
            tribbleOrangeSpeed = new Vector2(5, 5);


            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();
            seconds = 0f;
            screen = Screen.Intro;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            tribbleGreyTexture = Content.Load<Texture2D>("tribbleGrey");
            tribbleBrownTexture = Content.Load<Texture2D>("tribbleBrown");
            tribbleCreamTexture = Content.Load<Texture2D>("tribbleCream");
            tribbleOrangeTexture = Content.Load<Texture2D>("tribbleOrange");
            spaceTexture = Content.Load<Texture2D>("space");
            tribbleIntroTexture = Content.Load<Texture2D>("tribble_intro");
            bouncefont = Content.Load<SpriteFont>("bounceFont");
            introfont = Content.Load<SpriteFont>("introFont");
            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here



            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    screen = Screen.TribbleYard;
                }
            }

            else if (screen == Screen.TribbleYard)
            {
                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

                tribbleGreyRect.X += (int)tribbleGreySpeed.X;
                if (tribbleGreyRect.Right > window.Width || tribbleGreyRect.Left < 0)
                {
                    tribbleGreySpeed.X *= -1;
                    tribbleColour = new Color(generator.Next(1, 256), generator.Next(1, 256), generator.Next(1, 256));
                    bounces += 1;
                }
                tribbleGreyRect.Y += (int)tribbleGreySpeed.Y;
                if (tribbleGreyRect.Bottom > window.Height || tribbleGreyRect.Top < 0)
                {
                    tribbleGreySpeed.Y *= -1;
                    tribbleColour = new Color(generator.Next(1, 256), generator.Next(1, 256), generator.Next(1, 256));
                    bounces += 1;
                }

                tribbleBrownRect.X += (int)tribbleBrownSpeed.X;
                if (tribbleBrownRect.Right > window.Width || tribbleBrownRect.Left < 0)
                {
                    tribbleBrownSpeed.X *= -1;
                    bounces += 1;
                }
                tribbleBrownRect.Y += (int)tribbleBrownSpeed.Y;
                if (tribbleBrownRect.Bottom > window.Height || tribbleBrownRect.Top < 0)
                {
                    tribbleBrownSpeed.Y *= -1;
                    bounces += 1;
                }

                tribbleCreamRect.X += (int)tribbleCreamSpeed.X;
                if (tribbleCreamRect.Left > window.Width)
                    tribbleCreamRect.X = -100;

                tribbleOrangeRect.X += (int)tribbleOrangeSpeed.X;
                if (tribbleOrangeRect.Right > window.Width || tribbleOrangeRect.Left < 0)
                {
                    tribbleOrangeSpeed.X *= -1;
                    bounces += 1;
                }
                tribbleOrangeRect.Y += (int)tribbleOrangeSpeed.Y;
                if (tribbleOrangeRect.Bottom > window.Height || tribbleOrangeRect.Top < 0)
                {
                    tribbleOrangeSpeed.Y *= -1;
                    bounces += 1;
                }
                if (bounces >= 100 || seconds >= 25)
                    Exit();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(tribbleIntroTexture, new Rectangle(0, 0, 800, 500), Color.White);
                _spriteBatch.DrawString(bouncefont, ("Click the screen to begin"), new Vector2(460, 10), Color.DarkBlue);
                _spriteBatch.DrawString(bouncefont, ("The program will end after"), new Vector2(460, 50), Color.DarkBlue);
                _spriteBatch.DrawString(bouncefont, ("100 bounces or 25 seconds"), new Vector2(460, 90), Color.DarkBlue);



            }
            else if (screen == Screen.TribbleYard)
            {

                _spriteBatch.Draw(spaceTexture, window, Color.White);
                _spriteBatch.Draw(tribbleGreyTexture, tribbleGreyRect, tribbleColour);
                _spriteBatch.Draw(tribbleBrownTexture, tribbleBrownRect, Color.White);
                _spriteBatch.Draw(tribbleCreamTexture, tribbleCreamRect, Color.White);
                _spriteBatch.Draw(tribbleOrangeTexture, tribbleOrangeRect, Color.White);
                _spriteBatch.DrawString(bouncefont, ("Bounces: " + bounces).ToString(), new Vector2(10, 10), Color.White);
                _spriteBatch.DrawString(bouncefont, ("Time: " + seconds).ToString(), new Vector2(10, 50), Color.White);

            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
