using System;
using System.Collections.Generic;
using System.Linq;
using ContainerVervoer.Enums;

namespace ContainerVervoer.Models
{
    public class Ship
    {
        public List<ContainerRow> Rows { get; set; } = new List<ContainerRow>();
        public List<Container> UnsortedContainers { get; set; } = new List<Container>();
        public int Width { get; } = 5;
        public int Length { get;  } = 3;

        public int Weight
        {
            get
            {
                return Rows.Sum(row => row.Stacks.Sum(stack => stack.Weight));
            }
        }

        public void AddContainer(Container container)
        {
            UnsortedContainers.Add(container);
        }

        public void Sort()
        {
            UnsortedContainers = UnsortedContainers.OrderByDescending(a => a.Weight).ToList();
            List<Container> NormalContainers = UnsortedContainers.FindAll(container => !container.Cooled && !container.Valuable);
            List<Container> CooledContainers = UnsortedContainers.FindAll(container => container.Cooled && !container.Valuable);
            List<Container> ValuableContainers = UnsortedContainers.FindAll(container => (!container.Cooled && container.Valuable));
            List<Container> CoolValuableContainers = UnsortedContainers.FindAll(container => (container.Cooled && container.Valuable));

            for (int i = 0; i < Width; i++)
            {
                ContainerRow row = new ContainerRow();
                Rows.Add(row);
            }
            
            SortContainers(CooledContainers);
            SortContainers(CoolValuableContainers);
            SortContainers(ValuableContainers);
            SortContainers(NormalContainers);

            SanityChecks();
        }

        private bool SortContainers(List<Container> containers)
        {
            bool didAdd = false;

            foreach (Container container in containers)
            {
                container.SanityCheck();
                
                ContainerRow row = Rows.OrderBy(_row => _row.Stacks.Sum(stack => stack.Weight)).First();

                if (row.AddContainer(container, Length) == Error.None)
                {
                    didAdd = true;
                }

                if (!didAdd)
                {
                    throw new Exception("Was unable to add all containers!");
                }
            }

            return didAdd;
        }

        public void SanityChecks()
        {
            if (Weight < ((Length * Width * 150) * 0.5))
            {
                //throw new Exception("Ship will capsize due to not half of total weight used");
            }
        }

        public void Export()
        {
            string exportString = $"https://i872272core.venus.fhict.nl/ContainerVisualizer/index.html?width={ Width }&length={ Length }";
            
            int rowId = 0;

            string Stacks = "&stacks=";
            string Weights = "&weights=";
            
            foreach (ContainerRow row in Rows)
            {
                rowId++;

                if (Stacks != "&stacks=")
                    Stacks += "/";
                if (Weights != "&weights=")
                    Weights += "/";

                int stackId = 0;

                bool firstStack = true;
                
                foreach (Stack stack in row.Stacks)
                {
                    stackId++;

                    if (!firstStack)
                    {
                        Stacks += ",";
                        Weights += ",";
                    }

                    firstStack = false;

                    bool first = true;
                    foreach (Container container in stack.Containers)
                    {
                        int ContainerType = 1;

                        if (container.Valuable)
                            ContainerType = 2;
                        if (container.Cooled)
                            ContainerType = 3;
                        if (container.Valuable && container.Cooled)
                            ContainerType = 4;

                        if (first)
                        {
                            Stacks += $"{ContainerType}";
                            Weights += $"{container.Weight}";
                        }
                        else
                        {
                            Stacks += $"-{ContainerType}";
                            Weights += $"-{container.Weight}";
                        }
                        
                        first = false;
                    }
                }
            }

            exportString += Stacks;
            exportString += Weights;

            Console.WriteLine(exportString);
        }
    }
}