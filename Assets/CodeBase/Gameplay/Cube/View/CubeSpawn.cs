using Gameplay.Cube.Configs;
using Gameplay.Cube.Model;
using UnityEngine;
using Zenject;

namespace Gameplay.Cube.View
{
    public class CubeSpawn
    {
        [Inject] private CubesConfig _cubesConfig;

        public GameObject GetCube(ECube id)
        {
            var cubePrefab = _cubesConfig.Get(id);

            if (cubePrefab == null)
            {
                Debug.LogError($"Prefab not found for id: {id}");
                return null;
            }

            return cubePrefab;
        }
    }
}
