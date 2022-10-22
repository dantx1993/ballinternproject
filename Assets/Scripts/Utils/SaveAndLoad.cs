using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallMaze
{
    public class SaveAndLoad
    {
        public static void SaveData<T>(string key, T data)
        {
            string json = data.ToJson();
            PlayerPrefs.SetString(key, json);
        }
        public static T LoadData<T>(string key, T defaultData)
        {
            string json = PlayerPrefs.GetString(key, null);
            Debug.Log(json);
            if(string.IsNullOrEmpty(json))
            {
                return defaultData;
            }
            T result = json.ToObject<T>();
            return result;
        }
    }
}
