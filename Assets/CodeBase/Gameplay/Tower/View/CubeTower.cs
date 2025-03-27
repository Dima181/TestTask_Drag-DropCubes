using DG.Tweening;
using Extensions.Localization;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.Tower.View
{
    public class CubeTower : TowerAbstract
    {
        public override void TryPlaceCube(GameObject cube, PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(_dropZone, eventData.position, null, out Vector3 dropPosition);

            RectTransform cubeRect = cube.GetComponent<RectTransform>();
            float cubeHeight = cubeRect.rect.height * _dropZone.lossyScale.y;

            float screenWorldHeight = _dropZone.rect.height * _dropZone.lossyScale.y;
            float worldTopY = _dropZone.position.y + screenWorldHeight / 2;

            if (_cubes.Count == 0)
            {
                dropPosition.z = 0;
                cube.transform.position = dropPosition;
                AddCube(cube, dropPosition);
            }
            else
            {
                Vector3 lastCubePos = _cubes[_cubes.Count - 1].transform.position;

                if (dropPosition.y > lastCubePos.y + cubeHeight)
                {
                    float randomOffsetX = Random.Range(-cubeRect.rect.width / 2, cubeRect.rect.width / 2);
                    dropPosition = lastCubePos + new Vector3(randomOffsetX, cubeHeight, 0);

                    float cubeTop = dropPosition.y + cubeHeight / 2;
                    if (cubeTop <= worldTopY)
                    {
                        AddCube(cube, dropPosition);
                    }
                    else
                    {
                        _debugText.text = LocalizationManager.GetText(LocalizationKeys.LIMIT_HAS_BEEN_EXCEEDED);
                        cube.transform.DOScale(Vector3.zero, 0.3f).OnComplete(() => Destroy(cube));
                    }
                }
                else
                {
                    _debugText.text = LocalizationManager.GetText(LocalizationKeys.CUBE_MISSING_MESSAGE);
                    cube.transform.DOScale(Vector3.zero, 0.3f).OnComplete(() => Destroy(cube));
                }
            }
        }
    }
}
