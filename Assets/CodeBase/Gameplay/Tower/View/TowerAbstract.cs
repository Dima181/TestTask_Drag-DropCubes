using DG.Tweening;
using Extensions.Localization;
using Gameplay.Cube.View;
using Gameplay.Tower.Model;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.Tower.View
{
    public abstract class TowerAbstract : MonoBehaviour
    {
        public TextMeshProUGUI DebugText => _debugText;
        public List<GameObject> Cubes => _cubes;
        public Transform TowerParent => _towerParent;
        public ETower TowerId => _towerId;

        [SerializeField] protected ETower _towerId;
        [SerializeField] protected RectTransform _dropZone;
        [SerializeField] protected Transform _towerParent;
        [SerializeField] protected TextMeshProUGUI _debugText;
        protected List<GameObject> _cubes = new();

        private TowerPresenter _presenter;

        public void SetPresenter(TowerPresenter presenter)
        {
            _presenter = presenter;
            _cubes = _presenter.LoadTower();
        }

        public void SaveTower()
        {
            _presenter.SaveTower();
            _debugText.text = LocalizationManager.GetText(LocalizationKeys.SAVE_TOWER_MESSAGE);
        }

        public abstract void TryPlaceCube(GameObject cube, PointerEventData eventData);

        public bool IsInDropZone(Vector3 screenPosition)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_dropZone, screenPosition, null, out Vector2 localPoint);
            return _dropZone.rect.Contains(localPoint);
        }

        protected void AddCube(GameObject cube, Vector3 dropPosition)
        {
            _cubes.Add(cube);
            cube.transform.SetParent(_towerParent, true);
            cube.transform.DOMove(dropPosition, 0.3f);
            cube.AddComponent<DraggableCube>();

            if (cube.TryGetComponent<CubeItem>(out var itemCube))
                Destroy(itemCube);

            _debugText.text = LocalizationManager.GetText(LocalizationKeys.CUBE_IS_INSTALLED);
        }

    }
}
