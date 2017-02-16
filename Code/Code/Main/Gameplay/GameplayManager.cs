using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework
{
    public class GameplayManager {
        
        public GameFlow gameFlow;

        public GameplayManager() {

        }

        public void LoadContent() {
            gameFlow = new GameFlow();
            gameFlow.LoadContent();

            Globals.gameInstance.gameState = GameState.inGame;
            Globals.gameInstance.isLoading = false;
            Globals.songManager.StartGameplay();
        }

        public void UnloadContent() {
            if(gameFlow != null)
                Globals.gameplayManager.gameFlow.UnloadContent();
        }

        public void Update(GameTime gameTime) {
            Globals.gameplayManager.gameFlow.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch) {
            Globals.gameplayManager.gameFlow.Draw(spriteBatch);
        }
    }
}
