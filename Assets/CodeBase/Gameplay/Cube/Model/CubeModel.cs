namespace Gameplay.Cube.Model
{
    public class CubeModel
    {
        public ECube Id => _id;
        public string Name => _name;
        public string Description => _description;

        private readonly ECube _id;
        private readonly string _name;
        private readonly string _description;

        public CubeModel(
            ECube id, 
            string name, 
            string description)
        {
            _id = id;
            _name = name;
            _description = description;
        }
    }
}
