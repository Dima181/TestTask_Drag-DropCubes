using Cysharp.Threading.Tasks;
using Gameplay.Lootbox.UI;
using Gameplay.LootboxIdentical.UI;
using MyTask.CodeBase.UI;
using MyTask.CodeBase.UI.Core;
using System.Linq;
using UniRx;
using Zenject;

namespace Gameplay.UI.HUD
{
    public class UIGameplayHUDPresenter : UIScreenPresenter<UIGameplayHUDView>
    {
        [Inject] private readonly UINavigator _uiNavigator;

        protected override UniTask BeforeShow(CompositeDisposable disposables)
        {
            _view.OnPlayNormalClicked
                .Subscribe(_ =>
                {
                    _uiNavigator.Perform<UILootboxScrollPopupPresenter>(p => p.ShowAndForget());
                    _view.Buttons.ToList().ForEach(button => button.gameObject.SetActive(false));
                })
                .AddTo(disposables);
            
            _view.OnPlayIdenticalClicked
                .Subscribe(_ =>
                {
                    _uiNavigator.Perform<UILootboxScrollIdenticalPopupPresenter>(p => p.ShowAndForget());
                    _view.Buttons.ToList().ForEach(button => button.gameObject.SetActive(false));
                })
                .AddTo(disposables);

            _view.OpenActoin
                .Subscribe(_ =>
                {
                    _view.Buttons.ToList().ForEach(button => button.gameObject.SetActive(true));
                });

            return UniTask.CompletedTask;
        }
    }
}
