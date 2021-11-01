using System;
using System.IO;
using UnityEngine;

namespace Amlos
{
    [Serializable]
    public class Configuration
    {
        [SerializeField] private GameSoundData sound;
        [SerializeField] private string language = "en_us";

        public string Language { get => language; set { language = value; } }
        public GameSoundData Sound { get => sound; set => sound = value; }
    }

    public static class ConfigurationLoader
    {
        public static string ConfigPath => Application.persistentDataPath + "/config.json";

        /// <summary>
        /// read configuration from
        /// </summary>
        public static Configuration GetConfiguration()
        {
            string settingPath;
            Configuration configuration;
            if (!File.Exists(ConfigPath))
            {
                Debug.Log("Create Configuration");
                configuration = new Configuration();
                settingPath = JsonUtility.ToJson(configuration);
                File.WriteAllText(ConfigPath, settingPath);
            }
            else
            {
                settingPath = File.ReadAllText(ConfigPath);
                configuration = JsonUtility.FromJson<Configuration>(settingPath);
            }

            Debug.Log("Configuration import complete");
            return configuration;
        }

        public static void Save(Configuration configuration)
        {
            var txt = JsonUtility.ToJson(configuration);
            File.WriteAllText(ConfigPath, txt);
        }

    }



}