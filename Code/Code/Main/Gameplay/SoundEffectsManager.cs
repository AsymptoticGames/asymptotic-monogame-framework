using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace AsymptoticMonoGameFramework{

    public class SoundEffectObject {
        
        public SoundEffect soundEffect;
        public string soundEffectFile;
        public float soundEffectVolume;

        public SoundEffectObject(string _soundEffectFile, float _soundEffectVolume) {
            soundEffectFile = _soundEffectFile;
            soundEffectVolume = _soundEffectVolume;
        }

        public void LoadSoundEffect() {
            soundEffect = Globals.content.Load<SoundEffect>(soundEffectFile);
        }
    }

    public enum SoundEffects {
        MenuUp,
        MenuDown,
        MenuLeft,
        MenuRight,
        MenuConfirm,
        MenuBack,
        MenuToggle,
        MenuSlider
    };

    public class SoundEffectsManager {
        
        public static Vector2 zeroPanVectorLocation = new Vector2(-1, -1);

        private Dictionary<SoundEffects, SoundEffectObject> soundEffectList = new Dictionary<SoundEffects, SoundEffectObject> {
                {SoundEffects.MenuUp, new SoundEffectObject("Audio/SoundEffects/Menu Up", 0.7f)},
                {SoundEffects.MenuDown, new SoundEffectObject("Audio/SoundEffects/Menu Down", 0.7f)},
                {SoundEffects.MenuLeft, new SoundEffectObject("Audio/SoundEffects/Menu Left", 0.7f)},
                {SoundEffects.MenuRight, new SoundEffectObject("Audio/SoundEffects/Menu Right", 0.7f)},
                {SoundEffects.MenuConfirm, new SoundEffectObject("Audio/SoundEffects/Menu Confirm", 0.9f)},
                {SoundEffects.MenuBack, new SoundEffectObject("Audio/SoundEffects/Menu Back", 1.0f)},
                {SoundEffects.MenuToggle, new SoundEffectObject("Audio/SoundEffects/Menu Toggle", 0.7f)},
                {SoundEffects.MenuSlider, new SoundEffectObject("Audio/SoundEffects/Menu Slider", 0.5f)},
            };

        public SoundEffectsManager() {

        }

        public void Update(GameTime gameTime) {

        }

        public void LoadContent() {
            foreach(KeyValuePair<SoundEffects, SoundEffectObject> soundEffectObject in soundEffectList) {
                soundEffectObject.Value.LoadSoundEffect();
            }
        }

        public void UnloadContent() {

        }

        public SoundEffectInstance PlaySoundEffect(SoundEffects soundEffect, bool looping, Vector2 location) {
            SoundEffectInstance soundInstance = soundEffectList[soundEffect].soundEffect.CreateInstance();
            if (soundInstance != null) {
                soundInstance.Volume = GetSoundVolume() * soundEffectList[soundEffect].soundEffectVolume;
                soundInstance.IsLooped = looping;
                soundInstance.Pitch = GetSoundPitch();
                soundInstance.Pan = GetSoundPan(location);
                soundInstance.Play();
            }

            return soundInstance;
        }

        private float GetSoundVolume() {
            return 0.2f * AudioConfig.soundEffectVolume;
        }

        private float GetSoundPitch() {
            return 0.0f;
        }

        private float GetSoundPan(Vector2 location) {
            if (location == zeroPanVectorLocation) {
                return 0.0f;
            } else {
                return (location.X / ResolutionConfig.virtualResolution.Item1) * 2.0f - 1.0f;
            }
        }
    }
}
