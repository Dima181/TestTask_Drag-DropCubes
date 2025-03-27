using UnityEngine;
using Zenject;

namespace MyTask.CodeBase.Cam
{
    public class CameraControlInstaller : MonoInstaller<CameraControlInstaller>
    {
        [SerializeField] private Camera _screenCamera;

        public override void InstallBindings()
        {
            Container.Bind<Cameras>()
                .FromMethod(() => new Cameras(_screenCamera))
                .AsSingle();
        }
    }
}
