using Minerva.Localization;
using System.Collections.Generic;
using UnityEngine;

namespace Amlos
{
    [CreateAssetMenu(fileName = "Game Data", menuName = "Amlos/Game Data")]
    public class GameData : ScriptableObject
    {
        public static GameData instance;

        [Header("Common Prefab")]
        public GameObject confirmWindow;
        public GameObject infoWindow;

        [Header("Levels")]
        public Level mainHall;
        public List<Level> levels;

        [Header("Player wordline")]
        public PlayerWordLineData playerWordLineData;

        [Header("Localization")]
        public LanguageFileManager languageFilesManager;

        public Level GetLevel(string name)
        {
            return levels.Find(l => l.roomName == name);
        }
    }
}