using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AsymptoticMonoGameFramework {

    public class AudioSettingsMenu : MenuScreen {

        private MenuLabel musicVolumeLabel;
        private MenuLabel soundEffectsVolumeLabel;

        private MenuSlider musicVolumeSlider;
        private MenuSlider soundEffectsVolumeSlider;
        private MenuButton applyChangesButton;
        private MenuButton backButton;

        private Texture2D menuBackground;

        private float originalMusicVolumeValue;
        private float originalSoundEffectsVolumeValue;

        public AudioSettingsMenu() {
            originalMusicVolumeValue = AudioConfig.musicVolume;
            originalSoundEffectsVolumeValue = AudioConfig.soundEffectVolume;
        }

        public AudioSettingsMenu(MenuScreen _parentMenu) : base(_parentMenu) {
            originalMusicVolumeValue = AudioConfig.musicVolume;
            originalSoundEffectsVolumeValue = AudioConfig.soundEffectVolume;
        }

        public override void MenuScreenOpened() {
            base.MenuScreenOpened();
            originalMusicVolumeValue = AudioConfig.musicVolume;
            originalSoundEffectsVolumeValue = AudioConfig.soundEffectVolume;
            musicVolumeSlider.value = originalMusicVolumeValue;
            soundEffectsVolumeSlider.value = originalSoundEffectsVolumeValue;
        }

        public override void LoadContent() {
            menuBackground = Globals.content.Load<Texture2D>("Menues/PauseMenu/pause-menu-background");

            Vector2 buttonSize = new Vector2(400, 80);

            musicVolumeLabel = new MenuLabel(
                    Globals.content.Load<SpriteFont>("Fonts/candara-bold-28"),
                    new Vector2(ResolutionConfig.virtualResolution.Item1 / 2, 420),
                    MusicVolumeLabelText(),
                    CustomColors.veryLightOrange
                );

            soundEffectsVolumeLabel = new MenuLabel(
                    Globals.content.Load<SpriteFont>("Fonts/candara-bold-28"),
                    new Vector2(ResolutionConfig.virtualResolution.Item1 / 2, 550),
                    SoundEffectVolumeLabelText(),
                    CustomColors.veryLightOrange
                );

            musicVolumeSlider = new MenuSlider(new Vector2(660, 450), this, AudioConfig.musicVolume);
            soundEffectsVolumeSlider = new MenuSlider(new Vector2(660, 580), this, AudioConfig.soundEffectVolume);
            soundEffectsVolumeSlider.soundEffectOnSliderChange = true;

            applyChangesButton = new MenuButton(
                    new Vector2(760, 680),
                    this,
                    buttonSize,
                    "Apply Changes"
                );

            backButton = new MenuButton(
                    new Vector2(760, 780),
                    this,
                    buttonSize,
                    "Back"
                );

            labelList.Add(musicVolumeLabel);
            labelList.Add(soundEffectsVolumeLabel);

            buttonList.Add(musicVolumeSlider);
            buttonList.Add(soundEffectsVolumeSlider);
            buttonList.Add(applyChangesButton);
            buttonList.Add(backButton);
        }

        public override void UnloadContent() {

        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            AudioConfig.musicVolume = musicVolumeSlider.value;
            AudioConfig.soundEffectVolume = soundEffectsVolumeSlider.value;

            musicVolumeLabel.text = MusicVolumeLabelText();
            soundEffectsVolumeLabel.text = SoundEffectVolumeLabelText();

            if (AudioConfig.musicVolume != originalMusicVolumeValue || AudioConfig.soundEffectVolume != originalSoundEffectsVolumeValue) {
                backButton.buttonTextLabel.text = "Discard Changes";
            } else {
                backButton.buttonTextLabel.text = "Back";
            }
        }

        protected override void BackPressed() {
            AudioConfig.SaveAudioSettings();
            currentlySelectedButtonIndex = 0;
            parentMenu.CloseSubMenu();
        }

        public override void ButtonClicked(MenuButton button) {
            base.ButtonClicked(button);
            if(button == applyChangesButton) {
                ApplyChangesButtonPressed();
            } else if (button == backButton) {
                BackButtonPressed();
            }
        }

        private void ApplyChangesButtonPressed() {
            originalMusicVolumeValue = AudioConfig.musicVolume;
            originalSoundEffectsVolumeValue = AudioConfig.soundEffectVolume;
            BackPressed();
        }

        private void BackButtonPressed() {
            AudioConfig.musicVolume = originalMusicVolumeValue;
            musicVolumeSlider.value = originalMusicVolumeValue;
            AudioConfig.soundEffectVolume = originalSoundEffectsVolumeValue;
            soundEffectsVolumeSlider.value = originalSoundEffectsVolumeValue;
            BackPressed();
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(menuBackground, new Rectangle(320, 180, 1280, 720), currentSubMenuScreenIndex >= 0 ? CustomColors.darkerGray : Color.White);
            base.Draw(spriteBatch);
        }

        private string MusicVolumeLabelText() {
            return "Music Volume: " + HelperFunctions.RoundFloat((AudioConfig.musicVolume * 100.0f), 0);
        }

        private string SoundEffectVolumeLabelText() {
            return "Sound Effects Volume: " + HelperFunctions.RoundFloat((AudioConfig.soundEffectVolume * 100.0f), 0);
        }
    }
}
