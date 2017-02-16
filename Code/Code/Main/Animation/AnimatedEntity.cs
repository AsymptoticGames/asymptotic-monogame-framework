using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsymptoticMonoGameFramework
{
    public class AnimatedEntity
    {
        private Dictionary<string, Animation> animations;
        private Animation currentAnimation;
        private Vector2 position;
        private Vector2 size;
        private Vector2 origin;
        private float rotation;
        private SpriteEffects flipEffect;
        private Color tintColor;

        public Vector2 Position{
            get { return position; }
            set { position = value; }
        }

        public Vector2 Size {
            get { return size; }
            set { size = value; }
        }

        public float Rotation{
            get { return rotation; }
            set { rotation = value; }
        }

        public Vector2 Origin {
            get { return origin; }
            set { origin = value; }
        }

        public SpriteEffects FlipEffect{
            get { return flipEffect; }
            set { flipEffect = value; }
        }

        public Color TintColor {
            get { return tintColor; }
            set { tintColor = value; }
        }

        public string CurntAnimationName {
            get { return currentAnimation.Name; }
        }

        public Animation CurrentAnimation {
            get { return currentAnimation;}
        }

        public Rectangle BoundingRect() {
            return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public AnimatedEntity(){
            animations = new Dictionary<string, Animation>(24);
            position = Vector2.Zero;
            origin = Vector2.Zero;
            size = Vector2.Zero;
            rotation = 0;
            flipEffect = SpriteEffects.None;
            tintColor = Color.White;
        }

        public AnimatedEntity(Vector2 position, Vector2 size, Color tintColor) {
            animations = new Dictionary<string, Animation>(24);
            origin = Vector2.Zero;
            rotation = 0;
            flipEffect = SpriteEffects.None;

            this.position = position;
            this.size = size;
            this.tintColor = tintColor;

            if (tintColor == null)
                tintColor = Color.White;
        }


        public void AddAnimation(Animation animation){
            if (!animations.ContainsKey(animation.Name)){
                animations.Add(animation.Name, animation);
            }
            else{
                throw new ApplicationException("Animation key is already used");
            }
        }

        public void PlayAnimation(string key){
            if(string.IsNullOrEmpty(key) || !animations.ContainsKey(key)){
                return;
            }
            if(currentAnimation != null){
                if(currentAnimation.Name == key) {
                    return;
                }
            }
            currentAnimation = animations[key];
            currentAnimation.Reset();
        }

        public void ResetAndPlayAnimation(string key) {
            if (string.IsNullOrEmpty(key) || !animations.ContainsKey(key)) {
                return;
            }
            currentAnimation = animations[key];
            currentAnimation.Reset();
        }

        public void Update(GameTime gameTime){
            if(currentAnimation != null){
                currentAnimation.Update(gameTime);

                if (currentAnimation.IsComplete){
                    if (!string.IsNullOrEmpty(currentAnimation.TransitionKey)){
                        PlayAnimation(currentAnimation.TransitionKey);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle boundingRect) {
            if (currentAnimation != null) {
                spriteBatch.Draw(currentAnimation.SpriteSheet, boundingRect, currentAnimation.CurrentKeyFrame.Source, tintColor, rotation, origin, flipEffect, 0);
            }
        }

    }
}
