using ContainerVervoer.Enums;

namespace ContainerVervoer.Models
{
    public class CoolValuableContainer : Container
    {
        public CoolValuableContainer(int weight)
        {
            Weight = weight;
            Cooled = true;
            Valuable = true;
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