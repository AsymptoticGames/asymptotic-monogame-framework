using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework{

    public enum GameState {
        inStartMenues,
        inLoadingScreen,
        inGame
    }

    public class Game1 : Game {
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Viewport viewport;
        public SettingsManager settingsManager;

        public GameState gameState;
        public bool isLoading = false;
        
        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            this.IsMouseVisible = true;
            Content.RootDirectory = "Content";
            
            Resolution.Init(ref graphics);
        }

        protected override void Initialize() {
            gameState = GameState.inStartMenues;

            Globals.content = Content;
            Globals.gameWindow = Window;
            Globals.gameInstance = this;
            Globals.startMenuesManager = new StartMenuesManager();
            Globals.loadingScreenManager = new LoadingScreenManager();
            Globals.gameplayManager = new GameplayManager();
            Globals.songManager = new SongManager();
            Globals.soundEffectsManager = new SoundEffectsManager();
            
            SettingsManager.LoadAllSettings();

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            viewport = GraphicsDevice.Viewport;

            GlobalTextures.LoadContent();
            Globals.startMenuesManager.LoadContent();
            Globals.loadingScreenManager.LoadContent();
            Globals.songManager.LoadContent();
            Globals.soundEffectsManager.LoadContent();
        }

        protected override void UnloadContent() {
            GlobalTextures.UnloadContent();
            Globals.startMenuesManager.UnloadContent();
            Globals.loadingScreenManager.UnloadContent();
            Globals.gameplayManager.UnloadContent();
            Globals.songManager.UnloadContent();
            Globals.soundEffectsManager.UnloadContent();
        }

        protected override void Update(GameTime gameTime) {
            if (IsActive) {
                if (gameState == GameState.inStartMenues) {
                    Globals.startMenuesManager.Update(gameTime);
                }

                if (gameState == GameState.inLoadingScreen) {
                    Globals.loadingScreenManager.Update(gameTime);
                }

                if (gameState == GameState.inGame) {
                    Globals.gameplayManager.Update(gameTime);
                }

                Globals.songManager.Update(gameTime);
                Globals.soundEffectsManager.Update(gameTime);
                PlayerControls.Update();
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime) {
            Resolution.BeginDraw();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
            
            if (gameState == GameState.inStartMenues) {
                Globals.startMenuesManager.Draw(spriteBatch);
            }else if(gameState == GameState.inLoadingScreen) {
                Globals.loadingScreenManager.Draw(spriteBatch);
            }else if(gameState == GameState.inGame) {
                Globals.gameplayManager.Draw(spriteBatch);
            }

            base.Draw(gameTime);
            spriteBatch.End();
        }

        public void ExitGameplay() {
            gameState = GameState.inStartMenues;
            Globals.startMenuesManager.startMenuesState = StartMenuesState.MainMenu;
            GameplaySettingsManager.ResetToDefault();
            Globals.songManager.StopGameplay();
            Globals.songManager.StartMainMenu();
        }
    }
}
