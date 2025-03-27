using Gameplay.Tower.View;
using MyTask.CodeBase.UI.Core;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Lootbox.Scroll.View
{
    public class UIScrollView : UIScreenView
    {
        public RectTransform RequiresContainer => _requiresContainer;
        public List<RectTransform> ItemList { get => _itemList; set => _itemList = value; }
        public IObservable<Unit> OnExitButtonClicked => _exitButton.OnClickAsObservable();
        public IObservable<Unit> OnSaveButtonClicked => _saveButton.OnClickAsObservable();

        public TowerAbstract Tower => _tower;

        [SerializeField] private RectTransform _requiresContainer;
        [SerializeField] private List<RectTransform> _itemList;
        [SerializeField] private TowerAbstract _tower;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _saveButton;
    }
}
