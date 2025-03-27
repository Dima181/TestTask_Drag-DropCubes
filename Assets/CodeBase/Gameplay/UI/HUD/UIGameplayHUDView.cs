using MyTask.CodeBase.UI.Core;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.HUD
{
    public class UIGameplayHUDView : UIScreenView
    {
        public IObservable<Unit> OnPlayNormalClicked => _playNormal.OnClickAsObservable();
        public IObservable<Unit> OnPlayIdenticalClicked => _playIdentical.OnClickAsObservable();

        public IReadOnlyList<Button> Buttons => new List<Button>() { _playNormal, _playIdentical };
        public Subject<Unit> OpenActoin => _openActoin;

        private Subject<Unit> _openActoin = new();
        [SerializeField] private Button _playNormal;
        [SerializeField] private Button _playIdentical;
    }
}
