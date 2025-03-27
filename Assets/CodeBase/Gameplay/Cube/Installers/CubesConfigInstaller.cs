using Gameplay.Cube.Configs;
using Gameplay.Cube.View;
using Zenject;

namespace Gameplay.Cube.Installers
{
    public class CubesConfigInstaller : Installer<CubesConfigInstaller>
    {
        private const string CUBES_CONFIG_PATH = "Scriptable Objects/Gameplay/Cubes/CubesConfig";

        public override void InstallBindings()
        {
            Container.Bind<CubesConfig>()
                .FromScriptableObjectResource(CUBES_CONFIG_PATH)
                .AsSingle();

            Container.Bind<CubeSpawn>()
                .AsSingle();
        }
    }
}
