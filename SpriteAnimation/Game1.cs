using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace SpriteAnimation
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D hulkSprite;
        Texture2D block;
        Rectangle destRect;
        Rectangle sourceRect;

        bool collideright;
        bool collideleft;
        bool collidedown;
        bool collideup;

        Vector2 position = new Vector2();

        Vector2 blockposition = new Vector2(300, 300);

        Texture2D rect;
        int colCount;

        Point hulkFrame = new Point(32, 56);
        Point blockFrame = new Point(100, 100);

        int hulkCollisionRectOffset = -1;
        int blockCollisionRectOffset = -1;

        protected bool Collide()
        {
            Rectangle hulkRect = new Rectangle(
                (int)position.X + hulkCollisionRectOffset,
                (int)position.Y + hulkCollisionRectOffset,
                hulkFrame.X - (hulkCollisionRectOffset * 1),
                hulkFrame.Y - (hulkCollisionRectOffset * 1));

            Rectangle blockRect = new Rectangle(
                (int)blockposition.X + blockCollisionRectOffset,
                (int)blockposition.Y + blockCollisionRectOffset,
                blockFrame.X - (blockCollisionRectOffset * 1),
                blockFrame.Y - (blockCollisionRectOffset * 1));

            if(hulkRect.Intersects(blockRect))
            {
                colCount++;
                Debug.WriteLine("RIGHT: " + collideright.ToString() + " LEFT: " + collideleft.ToString() + " DOWN:  " + collidedown.ToString() + " UP:  " + collideup.ToString());
            }
            
            return hulkRect.Intersects(blockRect);
            

        }

        private KeyboardState ks;
        

        float elapsed;
        float delay = 100f;
        int frames = 0;
        int yLocation = 56;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //this.graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            destRect = new Rectangle(100, 100, 32, 56);
            base.Initialize();


            rect = new Texture2D(graphics.GraphicsDevice, 32, 56);

            Color[] data = new Color[80 * 30];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            rect.SetData(data);

            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            hulkSprite = Content.Load<Texture2D>("hulkSprite");
            block = Content.Load<Texture2D>("block");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected void animateRight(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (frames >= 3)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }
                elapsed = 0;
            }
            sourceRect = new Rectangle(40 * frames, 112, 40, 56);
        }
        protected void animateLeft(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (frames >= 3)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }
                elapsed = 0;
            }
            sourceRect = new Rectangle(40 * frames, 56, 40, 56);
        }
        protected void animateUp(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (frames >= 3)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }
                elapsed = 0;
            }
            sourceRect = new Rectangle(40 * frames, 168, 40, 56);
        }
        protected void animateDown(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (frames >= 3)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }
                elapsed = 0;
            }
            sourceRect = new Rectangle(40 * frames, 0, 40, 56);
        }

        public void freeMove()
        {
            collideright = false;
            collideleft = false;
            collidedown = false;
            collideup = false;
        }

        public void moveRight(GameTime gameTime)
        {
            if (yLocation != 112)
            {
                yLocation = 112;
            }
            animateRight(gameTime);
            position.X += 1f;
        }

        public void moveDown(GameTime gameTime)
        {
            if (yLocation != 0)
            {
                yLocation = 0;
            }
            animateDown(gameTime);
            position.Y += 1f;
        }

        public void moveUp(GameTime gameTime)
        {
            if (yLocation != 168)
            {
                yLocation = 168;
            }
            animateUp(gameTime);
            position.Y -= 1f;
        }

        public void moveLeft(GameTime gameTime)
        {
            if (yLocation != 56)
            {
                yLocation = 56;
            }
            animateLeft(gameTime);
            position.X -= 1f;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            ks = Keyboard.GetState();

           
            if(ks.IsKeyDown(Keys.Right))
            {
                if (Collide() && collidedown == true)
                {
                    moveRight(gameTime);
                }
                else if (Collide() && collideleft == true)
                {
                    moveRight(gameTime);
                }
                else if (Collide() && collideup == true)
                {
                    moveRight(gameTime);
                }
                else if (Collide())
                {
                    collideright = true;
                }
                else
                {
                    freeMove();
                    moveRight(gameTime);
                }

            }
            else if (ks.IsKeyDown(Keys.Left))
            {
                if (Collide() && collidedown == true)
                {
                    moveLeft(gameTime);
                }
                else if (Collide() && collideright == true)
                {
                    moveLeft(gameTime);
                }
                else if (Collide() && collideup == true)
                {
                    moveLeft(gameTime);
                }
                else if (Collide())
                {
                    collideleft = true;
                }
                else
                {
                    freeMove();
                    moveLeft(gameTime);
                }
            }
            
            else if (ks.IsKeyDown(Keys.Up))
            {
                
                if (Collide() && collidedown == true)
                {
                    moveUp(gameTime);
                }
                else if (Collide() && collideleft == true)
                {
                    moveUp(gameTime);
                }
                else if (Collide() && collideright == true)
                {
                    moveUp(gameTime);
                }
                else if (Collide())
                {
                    collideup = true;
                }
                else
                {
                    freeMove();
                    moveUp(gameTime);
                }
            }


            else if (ks.IsKeyDown(Keys.Down))
            {
                if (Collide() && collideright == true)
                {
                    moveDown(gameTime);
                }
                else if (Collide() && collideleft == true)
                {
                    moveDown(gameTime);
                }
                else if (Collide() && collideup == true)
                {
                    moveDown(gameTime);
                }
                else if (Collide())
                {
                    collidedown = true;
                }
                else
                {
                    freeMove();
                    moveDown(gameTime);
                }
            }
            

            destRect = new Rectangle((int)position.X, (int)position.Y, 32, 56);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(hulkSprite, destRect, sourceRect, Color.White);
            spriteBatch.Draw(block, blockposition, Color.White);


            spriteBatch.Draw(rect, destRect, sourceRect, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
