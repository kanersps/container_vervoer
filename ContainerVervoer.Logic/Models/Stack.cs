using System;
using System.Collections.Generic;
using System.Linq;
using ContainerVervoer.Enums;

namespace ContainerVervoer.Models
{
    public class Stack
    {
        public List<Container> Containers { get; } = new();
        public bool HasValuable { get; private set; } = false;

        public int Weight
        {
            get
            {
                return Containers.Sum(container => container.Weight);
            }
        }

        public bool IsFull(Container container)
        {
            if (Containers.Count == 0)
                return false;

            if (Weight - Containers[0].Weight > 120)
            {
                return true;
            }

            if (Weight + container.Weight > 150)
            {
                return true;
            }

            return false;
        }

        public Error AddContainer(Container container)
        {
            if (IsFull(container))
            {
                return Error.StackFull;
            }

            Error error = container.Compatible(this);
            if (error == Error.None)
            {
                container.AddToStack(this);
            }
            
            return error;
        }

        public void SetValuable()
        {
            HasValuable = true;
        }
    }
}