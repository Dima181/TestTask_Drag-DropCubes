using Gameplay.Cube.Model;
using Zenject;

namespace Gameplay.Cube.Installers
{
    public class CubeInstaller : MonoInstaller
    {
        public CubeModel CubeModel => _cubeModel;
        public ECube Id => _id;


        private ECube _id;
        private CubeModel _cubeModel;


        public void Prepare(
            CubeModel cubeModel)
        {
            _cubeModel = cubeModel;
            _id = cubeModel.Id;
        }
        
        public void SetId(ECube id) => 
            _id = id;

        public override void InstallBindings()
        {
            Container.Bind<CubeModel>().FromInstance(_cubeModel).AsSingle();
        }
    }
}
