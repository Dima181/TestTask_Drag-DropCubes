using Gameplay.Lootbox.UI;
using Gameplay.LootboxIdentical.UI;
using Gameplay.UI.HUD;
using MyTask.CodeBase.UI;

namespace Gameplay.UI
{
    public class GameplayUIInstaller : UIInstaller<UIGameplayHUDView, UIGameplayHUDPresenter>
    {
        protected override string HudPrefabPath => "UI/HUD/Gameplay Hud";

        protected override void InstallBindingsInternal()
        {
            LootboxScrollUIInstaller.Install(Container);
            LootboxScrolllIdenticalUIInstaller.Install(Container);
        }
    }
}
