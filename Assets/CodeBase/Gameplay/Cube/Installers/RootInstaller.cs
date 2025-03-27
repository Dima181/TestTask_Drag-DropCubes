using UnityEngine;
using Zenject;

namespace Gameplay.Cube.Installers
{
    public class RootInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            MonoInstaller[] installers = gameObject.GetComponents<MonoInstaller>();

            foreach (var installer in installers)
            {
                if (installer == this || installer == null)
                    continue;

                Container.Inject(installer);
                installer.InstallBindings();
            }
        }
    }
}
