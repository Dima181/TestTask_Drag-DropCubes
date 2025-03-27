using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Infrastructure.Transitions
{
    public class OutBlackScreenTransitionView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _alphaGroup;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        public async UniTask Prepare()
        {
            gameObject.SetActive(true);
            _alphaGroup.alpha = 0;
            await UniTask.Yield();
        }

        public async UniTask Apply()
        {
            _alphaGroup.alpha = 1;

            await _alphaGroup.DOFade(0, _duration).SetEase(_ease).AsyncWaitForCompletion();

            gameObject.SetActive(false);
        }

        public void ResetAlpha()
        {
            _alphaGroup.alpha = 0;
        }
    }
}