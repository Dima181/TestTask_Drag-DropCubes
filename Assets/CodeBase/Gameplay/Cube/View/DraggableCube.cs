using DG.Tweening;
using Gameplay.Cube.Model;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.Cube.View
{
    public class DraggableCube : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private CubeHole _hole;

        private Vector3 _originalPosition;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _hole = FindObjectOfType<CubeHole>();

            if (!TryGetComponent<CanvasGroup>(out _canvasGroup))
            {
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalPosition = _rectTransform.position;

            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;

            if (_hole.IsInHole(eventData.position))
            {
                _hole.TryThrowCubeInHole(gameObject);
            }
            else
            {
                transform.DOMove(_originalPosition, 0.3f);
            }
        }
    }
}
