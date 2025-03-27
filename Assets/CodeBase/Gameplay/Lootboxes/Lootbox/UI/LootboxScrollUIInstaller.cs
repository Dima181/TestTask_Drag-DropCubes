using MyTask.CodeBase.UI.Core;
using Zenject;

namespace Gameplay.Lootbox.UI
{
    public class LootboxScrollUIInstaller : Installer<LootboxScrollUIInstaller>
    {
        private const string LOOTBOXSCROLL_POPUP_PATH = "UI/LootBox/LootboxScroll";

        public override void InstallBindings()
        {
            Container.BindPresenter<UILootboxScrollPopupPresenter>()
                .WithViewFromPrefab(LOOTBOXSCROLL_POPUP_PATH)
                .AsPopup();
        }
    }
}
