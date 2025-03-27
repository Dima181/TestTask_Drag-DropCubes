using AYellowpaper.SerializedCollections;
using Gameplay.Cube.Model;
using UnityEngine;

namespace Gameplay.Cube.Configs
{
    [CreateAssetMenu(menuName = "Configs/Cubes/" + nameof(CubesConfig), fileName = nameof(CubesConfig))]
    public class CubesConfig : ScriptableObject
    {
        public SerializedDictionary<ECube, GameObject> CubePrefabsByType => _cubePrefabsByType;

        [SerializedDictionary("Cube Type", "Cube Prefab"), SerializeField]
        private SerializedDictionary<ECube, GameObject> _cubePrefabsByType;

        public GameObject Get(ECube id) => 
            _cubePrefabsByType[id];
    }
}
