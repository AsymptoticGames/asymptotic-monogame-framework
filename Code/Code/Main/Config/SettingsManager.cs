using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace AsymptoticMonoGameFramework
{
    public class SettingsManager
    {
        private string folderName = "AsymptoticMonoGameFramework";
        private string fileName = "asymptotic-monogame-framework-settings.dat";
        public Settings settings;

        public SettingsManager() {
            settings = new Settings();

            /*** Uncomment if you want to see where the settings file is stored on your local hard drive. full path is printed to console ***/
            //OutputFullFilePathOfSettingsFile();

            /*** Uncomment if changing controls in DefaultControls.cs or ControlsConfig.numGamepads ***/
            //DeleteSettingsFile();
        }

        public void Load() {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null)) {
                if (!isf.DirectoryExists(folderName))
                    return;

                string filePath = Path.Combine(folderName, fileName);
                
                if (!isf.FileExists(filePath))
                    return;
                
                using (IsolatedStorageFileStream stream = isf.OpenFile(filePath, FileMode.Open)) {
                    XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                    settings = (Settings)serializer.Deserialize(stream);
                }
            }
        }

        public void Save() {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null)) {
                if (!isf.DirectoryExists(folderName))
                    isf.CreateDirectory(folderName);

                string filePath = Path.Combine(folderName, fileName);
                
                using (IsolatedStorageFileStream stream = isf.CreateFile(filePath)) {
                    XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                    serializer.Serialize(stream, settings);
                }
            }
        }

        public static void LoadAllSettings() {
            Globals.gameInstance.settingsManager = new SettingsManager();
            Globals.gameInstance.settingsManager.Load();
            
            ResolutionConfig.LoadGraphicalSettings();
            AudioConfig.LoadAudioSettings();
            ControlsConfig.LoadControlsSettings();
        }

        private void OutputFullFilePathOfSettingsFile() {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null)) {
                string filePath = Path.Combine(folderName, fileName);

                if (!isf.DirectoryExists(folderName) || !isf.FileExists(filePath)) {
                    Console.WriteLine("Settings Filepath: ERROR: Settings file not created yet. try again.");
                    return;
                }
                
                using (IsolatedStorageFileStream stream = isf.OpenFile(filePath, FileMode.Open)) {
                    string fullFilePath = stream.GetType().GetField("m_FullPath", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(stream).ToString();
                    Console.WriteLine("Settings Filepath: " + fullFilePath);
                }
            }
        }

        private void DeleteSettingsFile() {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null)) {
                string filePath = Path.Combine(folderName, fileName);
                if (!isf.DirectoryExists(folderName) || !isf.FileExists(filePath))
                    return;

                isf.DeleteFile(filePath);
            }
        }
    }
}
