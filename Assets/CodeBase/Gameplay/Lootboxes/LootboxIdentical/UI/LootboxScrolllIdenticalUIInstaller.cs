using MyTask.CodeBase.UI.Core;
using Zenject;

namespace Gameplay.LootboxIdentical.UI
{
    public class LootboxScrolllIdenticalUIInstaller : Installer<LootboxScrolllIdenticalUIInstaller>
    {
        private const string LOOTBOXSCROLLIDENTICAL_POPUP_PATH = "UI/LootBox/LootboxScrollIdentical";

        public override void InstallBindings()
        {
            Container.BindPresenter<UILootboxScrollIdenticalPopupPresenter>()
                .WithViewFromPrefab(LOOTBOXSCROLLIDENTICAL_POPUP_PATH)
                .AsPopup();
        }
    }
}
