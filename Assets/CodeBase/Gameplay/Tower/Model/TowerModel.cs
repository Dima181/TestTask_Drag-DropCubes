using System.Collections.Generic;
using System;
using UnityEngine;

namespace Gameplay.Tower.Model
{
    [Serializable]
    public class TowerModel
    {
        public List<CubeData> Cubes = new();

        public static void Save(ETower towerId, TowerModel model)
        {
            string json = JsonUtility.ToJson(model);
            string key = $"Tower_{towerId}";
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }

        public static TowerModel Load(ETower towerId)
        {
            string key = $"Tower_{towerId}";
            if (PlayerPrefs.HasKey(key))
            {
                string json = PlayerPrefs.GetString(key);
                return JsonUtility.FromJson<TowerModel>(json);
            }

            return new TowerModel();
        }

    }
}
