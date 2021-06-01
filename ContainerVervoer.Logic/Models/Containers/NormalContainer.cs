namespace ContainerVervoer.Models
{
    public class NormalContainer : Container
    {
        public NormalContainer(int weight)
        {
            Weight = weight;
            Cooled = false;
            Valuable = false;
        }
    }
}