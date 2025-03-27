using System.Collections.Generic;
using UnityEngine;
using Gameplay.Cube.View;
using Gameplay.Cube.Installers;
using Object = UnityEngine.Object;
using Gameplay.Cube.Model;
using System.Linq;

namespace Gameplay.Tower.Model
{
    public class TowerPresenter
    {
        private readonly TowerModel _model;

        private readonly Transform _towerParent;
        private readonly CubeSpawn _cubeSpawn;
        private readonly List<GameObject> _cubes;
        private readonly ETower _towerId;

        public TowerPresenter(ETower towerId, TowerModel model, Transform towerParent, CubeSpawn cubeSpawn)
        {
            _towerId = towerId;
            _model = model;
            _towerParent = towerParent;
            _cubeSpawn = cubeSpawn;
            _cubes = new List<GameObject>();
        }

        public void SaveTower()
        {
            _model.Cubes.Clear();

            foreach (Transform cube in _towerParent)
            {
                if (cube.TryGetComponent(out CubeInstaller cubeInstaller))
                {
                    var cubeData = new CubeData
                    {
                        CubeEnum = cubeInstaller.Id,
                        Position = cube.localPosition
                    };

                    _model.Cubes.Add(cubeData);
                }
            }

            TowerModel.Save(_towerId, _model);
        }



        public List<GameObject> LoadTower()
        {
            _cubes.Clear();

            foreach (Transform child in _towerParent)
            {
                Object.Destroy(child.gameObject);
            }

            var loadedModel = TowerModel.Load(_towerId);
            List<GameObject> loadedCubes = new();

            foreach (var cubeData in loadedModel.Cubes.Take(loadedModel.Cubes.Count))
            {
                var cubePrefab = _cubeSpawn.GetCube(cubeData.CubeEnum);

                var cubeInstance = InitializeCube(cubePrefab, cubeData.CubeEnum, cubeData.Position, _towerParent);
                loadedCubes.Add(cubeInstance);
            }

            _cubes.AddRange(loadedCubes);
            return loadedCubes;
        }



        private GameObject InitializeCube(GameObject cardPrefab, ECube cubeModel, Vector3 position, Transform parent)
        {
            var clone = Object.Instantiate(cardPrefab, parent);

            RectTransform rect = clone.GetComponent<RectTransform>();

            rect.localPosition = position;

            clone.GetComponent<CubeItem>().enabled = false;

            if (clone.TryGetComponent<RootInstaller>(out var rootInstaller))
                Object.Destroy(rootInstaller);

            clone.GetComponent<CubeInstaller>().SetId(cubeModel);

            if (clone.TryGetComponent<CanvasGroup>(out var cloneCanvasGroup)) 
                cloneCanvasGroup.blocksRaycasts = false;

            clone.AddComponent<DraggableCube>();

            if (clone.TryGetComponent<CubeItem>(out var itemCube))
                Object.Destroy(itemCube);

            return clone;
        }
    }
}
