namespace ContainerVervoer.Models
{
    public class CooledContainer : Container
    {
        public CooledContainer(int weight)
        {
            Weight = weight;
            Cooled = true;
            Valuable = false;
        }
    }
}