using ContainerVervoer.Enums;

namespace ContainerVervoer.Models
{
    public class ValuableContainer : Container
    {
        public ValuableContainer(int weight)
        {
            Weight = weight;
            Valuable = true;
            Cooled = false;
        }

        public override Error Compatible(Stack stack)
        {
            if (stack.HasValuable)
            {
                return Error.ValuableAlreadyOnTop;
            }
            
            stack.SetValuable();
            
            return Error.None;
        }

        public override void AddToStack(Stack stack)
        {
            stack.Containers.Add(this);
        }
    }
}