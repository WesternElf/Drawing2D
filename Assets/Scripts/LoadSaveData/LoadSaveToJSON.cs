using System.IO;
using GameControl;
using UnityEngine;

namespace LoadSaveData
{
    public static class LoadSaveToJSON
    {
        private const string COUNT_KEY = "Coin count";
        public static void SaveParams(GameParameters gameParameters)
        {
            JSONObject newJSON = new JSONObject();
            newJSON.Add(COUNT_KEY, gameParameters.CoinCount);

            File.WriteAllText(GetFilePath(), newJSON.ToString());
            Debug.Log(GetFilePath());
        }

        public static GameParameters LoadParams()
        {
            GameParameters gameParameters = new GameParameters();
            var jsonString = File.ReadAllText(GetFilePath());
            JSONObject newJSON = JSON.Parse(jsonString) as JSONObject;
            gameParameters.CoinCount = newJSON[COUNT_KEY];
            return gameParameters;
        }

        private static string GetFilePath()
        {
            return Application.persistentDataPath + "/GameSave.json";
        }
    }
}
