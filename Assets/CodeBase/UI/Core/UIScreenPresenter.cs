using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UniRx;
using Zenject;

namespace MyTask.CodeBase.UI.Core
{
    public abstract class UIScreenPresenter<TView> : UIScreenPresenterBase<TView>, ISimpleUIScreenPresenter
        where TView : UIScreenView
    {
        private CompositeDisposable _disposables;
        protected CompositeDisposable Disposables => _disposables;

        public async UniTask Show()
        {
            _disposables?.Dispose();
            _disposables = new();

            await BeforeShow(_disposables);

            await _view.Show();

            await AfterShow(_disposables);

            IUIScreenPresenter.OnShow.Execute(this);
        }

        public void ShowAndForget() => Show().Forget();

        protected virtual void OnDispose() { }

        public sealed override void Dispose()
        {
            OnDispose();
            _disposables?.Dispose();
            _disposables = null;
        }

        protected abstract UniTask BeforeShow(CompositeDisposable disposables);
        protected virtual UniTask AfterShow(CompositeDisposable disposables) => UniTask.CompletedTask;
        protected override async UniTask AfterHide()
        {
        }
    }
}
