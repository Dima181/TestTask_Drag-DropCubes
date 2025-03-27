namespace Gameplay.Cube.Model
{
    public class CubeBuilder
    {
        private ECube _id;
        private string _name;

        public CubeBuilder WithId(ECube id)
        {
            _id = id;
            return this;
        }

        public CubeBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CubeModel Build()
        {
            return new CubeModel(
                _id,
                _name,
                "Description");
        }
    }
}
