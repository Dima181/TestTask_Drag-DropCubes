using Cysharp.Threading.Tasks;
using Gameplay.Cube.Installers;
using Gameplay.Cube.Model;
using Gameplay.Cube.View;
using Gameplay.LootboxIdentical.Scroll.View;
using Gameplay.Tower.Model;
using Gameplay.UI.HUD;
using MyTask.CodeBase.UI.Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.LootboxIdentical.UI
{
    public class UILootboxScrollIdenticalPopupPresenter : UIScreenPresenter<UIScrollIdenticalView>
    {
        [Inject] private CubeSpawn _cubeSpawn;
        [Inject] private CubesCollection _collection;
        [Inject] private DiContainer _diContainer;
        [Inject] private UIGameplayHUDView _gameplayHUDView;
        private TowerPresenter _presenter;

        protected override UniTask BeforeShow(CompositeDisposable disposables)
        {
            foreach (var cube in _collection.Cubes)
            {
                var cardPrefab = _cubeSpawn.GetCube(cube.Key);

                var subContainer = _diContainer.CreateSubContainer();

                InitializeCube(cardPrefab, cube.Value, subContainer);
            }

            _view.Tower.SetPresenter(InitialTowerPresenter());

            _view.OnSaveButtonClicked
                .Subscribe(_ =>
                {
                    _view.Tower.SaveTower();
                });

            _view.OnExitButtonClicked
               .Subscribe(_ =>
               {
                   Hide().Forget();
                   ClearPreviousCards();
                   _gameplayHUDView.OpenActoin.OnNext(Unit.Default);
               })
               .AddTo(disposables);

            return UniTask.CompletedTask;
        }

        private TowerPresenter InitialTowerPresenter()
        {
            return _presenter = new TowerPresenter(
                _view.Tower.TowerId, 
                TowerModel.Load(_view.Tower.TowerId),
                _view.Tower.TowerParent,
                _cubeSpawn);
        }

        private void InitializeCube(GameObject cardPrefab, CubeModel building, DiContainer subContainer)
        {
            var installer = subContainer.InstantiatePrefabForComponent<RootInstaller>(
                                cardPrefab,
                                _view.RequiresContainer);

            if (building == null)
            {
                Debug.Log("building is null");
            }

            installer.GetComponent<CubeInstaller>().Prepare(building);

            subContainer.Inject(installer);
            installer.InstallBindings();

            _view.ItemList.Add(installer.GetComponent<RectTransform>());
        }

        private void ClearPreviousCards()
        {
            foreach (var item in _view.ItemList)
            {
                UnityEngine.Object.Destroy(item.gameObject);
            }
            _view.ItemList.Clear();
        }
    }
}
