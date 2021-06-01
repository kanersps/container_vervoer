using System;
using System.Collections.Generic;
using System.Linq;
using ContainerVervoer.Enums;

namespace ContainerVervoer.Models
{
    public class ContainerRow
    {
        public List<Stack> Stacks { get; set; } = new List<Stack>();

        public Error AddContainer(Container container, int maxStacks)
        {
            if (Stacks.Count == 0)
            {
                for (int i = 0; i < maxStacks; i++)
                {
                    Stacks.Add(new Stack());
                }
            }
            
            bool didAdd = false;

            
            if (container.Cooled && container.Valuable || container.Cooled)
            {
                return Stacks[0].AddContainer(container);
            }

            if (container.Valuable)
            {
                Error err = AddNearCenter(container);
                /*
                Error err = Stacks[0].AddContainer(container);

                if (err != Error.None)
                {
                    err = Stacks[^1].AddContainer(container);
                }
                */
                return err;
            }

            //Stack stack = Stacks.OrderBy(_stack => _stack.Containers.Sum(_container => _container.Weight)).First();
            
            
            
            if (AddNearCenter(container) == Error.None)
            {
                didAdd = true;
            }

            if (!didAdd)
            {
                return Error.StackFull;
            }

            return Error.None;
        }

        public Error AddNearCenter(Container container)
        {
            Error e = Error.None;

            int arraySize = Stacks.Count;
            int start = arraySize / 2;

            for (int i=0; i < arraySize; i++) {
                int index = (start+((i%2==0)?i/2:arraySize-(i+1)/2))%arraySize;

                bool added = false; 
                
                if (Stacks.ElementAtOrDefault(index + 1) == null)
                {
                    e = Stacks[index].AddContainer(container);
                    if (e == Error.None)
                    {
                        added = true;
                    }
                }
                else
                {
                    if (Stacks[index + 1].HasValuable && Stacks[index + 1].Containers.Count - 1 > Stacks[index].Containers.Count)
                    {
                        e = Stacks[index].AddContainer(container);
                        if (e == Error.None)
                        {
                            added = true;
                        }
                    } else if (!Stacks[index + 1].HasValuable)
                    {
                        e = Stacks[index].AddContainer(container);
                        if (e == Error.None)
                        {
                            added = true;
                        }
                    }
                }

                if (e == Error.None && added)
                {
                    break;
                }
            }

            return e;
        }
    }
}