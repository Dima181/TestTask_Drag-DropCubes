using Cysharp.Threading.Tasks;
using Gameplay.Cube.Model;
using Infrastructure.Pipeline.DataProviders;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Gameplay.Cube.Services
{
    public class LocalCubesDataProvider : LocalDataProvider<CubesCollection>
    {
        protected override async UniTask<CubesCollection> Load(
            DiContainer di, 
            DisposableManager disposableManager)
        {
            await UniTask.CompletedTask;
            var result = new List<CubeModel>();

            foreach (ECube cube in (ECube[])System.Enum.GetValues(typeof(ECube)))
            {
                result.Add(CreateCubeModel(cube));
            }

            return new CubesCollection(result.ToDictionary(cube => cube.Id));
        }

        private CubeModel CreateCubeModel(ECube cubeId)
        {
            return new CubeBuilder()
                .WithId(cubeId)
                .WithName(cubeId.ToString().Replace("_", " "))
                .Build();
        }
    }
}
