using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Tibia.Windows.Client
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager Graphics;

        GameDesktop Desktop;
        MouseState LastMouseState;

        public Game()
        {
            Graphics = new GraphicsDeviceManager(this);
            

            Graphics.PreparingDeviceSettings += PrepareDevice;
            Graphics.PreferredBackBufferWidth = 1280;
            Graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            // Setup the window
            IsFixedTimeStep = false;
            Graphics.SynchronizeWithVerticalRetrace = false;
            //graphics.GraphicsDevice.PresentationParameters.PresentationInterval = PresentInterval.One;
            //graphics.PreferWaitForVerticalTrace = false;
            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            Graphics.ApplyChanges();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Initialize the context
            UIContext.Initialize(Window, Graphics, Content);
            UIContext.Load();

            // Create the game frame
            Desktop = new GameDesktop();
            Desktop.Load();
            Desktop.CreatePanels();

            // Initial layout of all views
            Desktop.LayoutSubviews();
            Desktop.NeedsLayout = true;

            ///////////////////////////////////////////////////////////////////
            // For debugging read a TMV file as input

            FileInfo file = new FileInfo("./Test.tmv");
            Stream virtualStream = null;

            FileStream fileStream = file.OpenRead();
            if (file.Extension == ".tmv")
                virtualStream = new System.IO.Compression.GZipStream(fileStream, System.IO.Compression.CompressionMode.Decompress);
            else
                virtualStream = fileStream;

            // Add the initial state
            TibiaMovieStream MovieStream = new TibiaMovieStream(virtualStream, file.Name);
            ClientState State = new ClientState(MovieStream);

            MovieStream.PlaybackSpeed = 50;
            State.ForwardTo(new TimeSpan(0, 30, 0));

            // If fast-forwarded, client tab immediately,
            // otherwise delay until we receive the login packet.
            if (State.Viewport.Player == null)
            {
                State.Viewport.Login += delegate(ClientViewport Viewport)
                {
                    Desktop.AddClient(State);
                };
            }
            else
            {
                Desktop.AddClient(State);
            }

            State.Update(new GameTime());
            /*
            while (MS.Elapsed.TotalMinutes < 0 || MS.Elapsed.Seconds < 0)
                Protocol.parsePacket(InStream.Read());
             */
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            // Do input handling

            // First check left mouse button
            if (LastMouseState != null)
            {
                if (LastMouseState.LeftButton != mouse.LeftButton)
                    Desktop.MouseLeftClick(mouse);
            }

            // Send the mouse moved event
            if (LastMouseState.X != mouse.X || LastMouseState.Y != mouse.Y)
                Desktop.MouseMove(mouse);

            // Save the state for next frame so we can see what changed
            LastMouseState = mouse;

            // Update the game state
            Desktop.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            try
            {
                Desktop.Draw(null, Window.ClientBounds);
            }
            catch (Exception e)
            {
                throw;
            }

            base.Draw(gameTime);
        }

        protected void PrepareDevice(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
        }
    }
}
