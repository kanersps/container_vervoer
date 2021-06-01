using System;
using ContainerVervoer.Enums;

namespace ContainerVervoer.Models
{
    public class Container
    {
        public int Weight { get; protected init; }
        public bool Valuable { get; protected init; }
        public bool Cooled { get; protected init; }

        public virtual Error Compatible(Stack stack)
        {
            return Error.None;
        }

        public void SanityCheck()
        {
            if (Weight > 30)
            {
                throw new Exception("Too much weight in a single container, can not exceed 30!");
            }
        }

        public virtual void AddToStack(Stack stack)
        {
            stack.Containers.Insert(0, this);
        }
    }
}