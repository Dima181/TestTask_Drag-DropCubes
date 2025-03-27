using System.Collections;
using System.Collections.Generic;

namespace Gameplay.Cube.Model
{
    public class CubesCollection : IEnumerable<CubeModel>
    {
        public IReadOnlyDictionary<ECube, CubeModel> Cubes => _cubes;

        private readonly IReadOnlyDictionary<ECube, CubeModel> _cubes;

        public CubesCollection(IReadOnlyDictionary<ECube, CubeModel> cubes) =>
            _cubes = cubes;

        public CubeModel Get(ECube Id) =>
            Cubes[Id];

        public IEnumerator<CubeModel> GetEnumerator() =>
            Cubes.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            Cubes.Values.GetEnumerator();
    }
}
