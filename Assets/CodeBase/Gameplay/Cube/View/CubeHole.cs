using DG.Tweening;
using Extensions.Localization;
using Gameplay.Tower.View;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Cube.View
{
    public class CubeHole : MonoBehaviour
    {
        [SerializeField] private RectTransform _holeImage;
        [SerializeField] private TowerAbstract _tower;

        public bool IsInHole(Vector3 screenPosition)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_holeImage, screenPosition, null, out Vector2 localPoint);

            float a = _holeImage.rect.width / 2f;
            float b = _holeImage.rect.height / 2f;

            float normalizedX = localPoint.x / a;
            float normalizedY = localPoint.y / b;

            return (normalizedX * normalizedX + normalizedY * normalizedY) <= 1f;
        }

        public void TryThrowCubeInHole(GameObject cube)
        {
            cube.transform.DOScale(Vector3.zero, 0.3f).OnComplete(() => Destroy(cube));
            var rectCube = cube.GetComponent<RectTransform>();
            AnimateCubesDown(cube, rectCube);
            _tower.Cubes.Remove(cube);
            
            _tower.DebugText.text = LocalizationManager.GetText(LocalizationKeys.CUBE_THROWN_MESSAGE);
        }

        private void AnimateCubesDown(GameObject thrownCube, RectTransform rect)
        {
            if (thrownCube == null || rect == null)
                return;

            int thrownIndex = _tower.Cubes.IndexOf(thrownCube);
            if (thrownIndex == -1) return;

            float cubeHeight = rect.rect.height * rect.lossyScale.y;

            List<Tween> tweens = new();

            for (int i = thrownIndex + 1; i < _tower.Cubes.Count; i++)
            {
                GameObject cube = _tower.Cubes[i];
                if (cube == null) continue;

                Vector3 newPosition = cube.transform.position;
                newPosition.y -= cubeHeight;

                var tween = cube.transform
                    .DOMove(newPosition, 0.3f)
                    .SetEase(Ease.InOutQuad);

                tweens.Add(tween);
            }

            _tower.Cubes.Remove(thrownCube);

            DOTween.Sequence()
                .AppendInterval(0.3f)
                .OnComplete(() =>
                {
                    Destroy(thrownCube);
                });
        }
    }
}
