using DG.Tweening;
using Extensions.Localization;
using Gameplay.Cube.Installers;
using Gameplay.Cube.Model;
using Gameplay.Tower.View;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.Cube.View
{
    public class CubeItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Inject] private DiContainer _container;

        private RectTransform _rectTransform;
        private Transform _dragParent;
        private GameObject _clone;
        private CanvasGroup _canvasGroup;
        private ScrollRect _scrollRect;
        private TowerAbstract _tower;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();

            _dragParent = GameObject.FindGameObjectWithTag("DragParent").transform;
            _tower = FindObjectOfType<TowerAbstract>();

            _scrollRect = GetComponentInParent<ScrollRect>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_scrollRect != null) _scrollRect.enabled = false;

            _clone = Instantiate(gameObject, _dragParent);
            _clone.GetComponent<CubeItem>().enabled = false;

            if (_clone.TryGetComponent<RootInstaller>(out var rootInstaller))
                Destroy(rootInstaller);

            var cubeModel = _container.Resolve<CubeModel>();
            _clone.GetComponent<CubeInstaller>().Prepare(cubeModel);

            RectTransform cloneRect = _clone.GetComponent<RectTransform>();
            cloneRect.position = _rectTransform.position;
            cloneRect.sizeDelta = _rectTransform.sizeDelta;

            CanvasGroup cloneCanvasGroup = _clone.GetComponent<CanvasGroup>();
            if (cloneCanvasGroup != null) cloneCanvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_clone != null)
            {
                _clone.transform.position = eventData.position;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_scrollRect != null) _scrollRect.enabled = true;

            if (_clone != null)
            {
                if (_tower.IsInDropZone(_clone.transform.position))
                {
                    _tower.TryPlaceCube(_clone, eventData);
                }
                else
                {
                    _tower.DebugText.text = LocalizationManager.GetText(LocalizationKeys.CUBE_MISSING_MESSAGE);
                    _clone.transform.DOScale(Vector3.zero, 0.3f).OnComplete(() => Destroy(_clone));
                }
            }
        }
    }
}
