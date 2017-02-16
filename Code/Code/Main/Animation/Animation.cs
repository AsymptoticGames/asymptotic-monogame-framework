using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework
{
   public class Animation
    {
        private string name;
        private bool shouldLoop;
        private bool isComplete;
        private float framesPerSecond;
        private float timePerFrame;
        private float totalElapsedTime;
        private int currentFrame;
        private List<KeyFrame> keyFrames;
        private string transitionKey;
        private Texture2D spriteSheet;


        public string Name{
            get { return name; }
            set { name = value; }
        }

        public bool ShouldLoop{
            get { return shouldLoop; }
            set { shouldLoop = value; }
        }

        public Texture2D SpriteSheet {
            get { return spriteSheet;}
        }

        public float FramesPerSecond{
            get { return framesPerSecond; }
        }

        public List<KeyFrame> KeyFrames{
            get { return keyFrames; }
            set { keyFrames = value; }
        }

        public int CurrentFrame {
            get { return currentFrame; }
        }

        public KeyFrame CurrentKeyFrame{
            get { return keyFrames[currentFrame]; }
        }

        public bool IsComplete {
            get { return isComplete; }
            set { isComplete = value; }
        }

        public string TransitionKey{
            get { return transitionKey; }
        }

        public void SetFramesPerSecond(float value) {
            framesPerSecond = value;
            timePerFrame = 1.0f / framesPerSecond;
        }

        public Animation(){
            name = string.Empty;
            shouldLoop = false;
            isComplete = false;
            framesPerSecond = 0;
            timePerFrame = 0;
            totalElapsedTime = 0;
            currentFrame = -1;
            keyFrames = new List<KeyFrame>();
        }

        public Animation(string name, bool shouldLoop, float framesPerSecond, string path, string transitionKey){
            this.name = name;
            this.shouldLoop = shouldLoop;
            this.framesPerSecond = framesPerSecond;
            this.transitionKey = transitionKey;
            spriteSheet = Globals.content.Load<Texture2D>(path);

            timePerFrame = 1.0f / framesPerSecond;
            keyFrames = new List<KeyFrame>(60);
            isComplete = false;
            currentFrame = -1;
            totalElapsedTime = 0;
        }

        public void Reset(){
            currentFrame = 0;
            totalElapsedTime = 0;
            isComplete = false;
        }

        public void LoadLineAnimation(int width, int height, int num) {
            for (int i = 0; i < num; i++) {
                AddKeyFrame(i * width, 0, width, height);
            }
        }

        public void LoadRectAnimation(int width, int height, int perLine, int num) {
            int x, y = 0;

            for (int i = 0; i < num; i++) {
                x = (i * width) % (width * perLine);
                if (x == 0 && i != 0)
                    y += height;
                AddKeyFrame(x, y, width, height);
            }
        }

        public void LoadSingleAnimation(int width, int height) {
            AddKeyFrame(0, 0, width, height);
        }

        public void AddKeyFrame(int x, int y, int width, int height){
            KeyFrame keyFrame = new KeyFrame(x, y, width, height);
            keyFrames.Add(keyFrame);
        }

        public void LoadContent(string textureName){
            spriteSheet = Globals.content.Load<Texture2D>(textureName);
        }

        public void LoadContent(Texture2D spriteSheet){
            this.spriteSheet = spriteSheet;
        }

        public void Update(GameTime gameTime){
            totalElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyFrame keyFrame = keyFrames[currentFrame];

            if(totalElapsedTime >= timePerFrame){
                if(currentFrame >= keyFrames.Count - 1){
                    if (shouldLoop){
                        currentFrame = 0;
                        isComplete = false;
                    }
                    else{
                        isComplete = true;
                    }
                }
                else{
                    currentFrame++;
                }

                totalElapsedTime -= totalElapsedTime;
            }
        }

    
    }
}
