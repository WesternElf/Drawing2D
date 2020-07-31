using System.IO;
using GameControl;
using UnityEngine;

namespace LoadSaveData
{
    public static class LoadSaveToJSON
    {
        private const string COUNT_KEY = "Coin count";
        private const string SOUND_KEY = "Sound volume";
        private const string MUSIC_KEY = "Music volume";
        public static void SaveParams(GameParameters gameParameters)
        {
            JSONObject newJSON = new JSONObject();
            newJSON.Add(COUNT_KEY, gameParameters.CoinCount);
            newJSON.Add(SOUND_KEY, gameParameters.SoundVolume);
            newJSON.Add(MUSIC_KEY, gameParameters.MusicVolume);

            File.WriteAllText(GetFilePath(), newJSON.ToString());
        }

        public static GameParameters LoadParams()
        {
            GameParameters gameParameters = new GameParameters();
            var jsonString = File.ReadAllText(GetFilePath());
            JSONObject newJSON = JSON.Parse(jsonString) as JSONObject;

            gameParameters.MusicVolume = newJSON[MUSIC_KEY];
            gameParameters.SoundVolume = newJSON[SOUND_KEY];
            gameParameters.CoinCount = newJSON[COUNT_KEY];

            return gameParameters;
        }

        public static void ClearAllData()
        {
            GameParameters gameParameters = new GameParameters();
            SaveParams(gameParameters);
        }

        private static string GetFilePath()
        {
            return Application.persistentDataPath + "/GameSave.json";
        }

    }
}
