using System;
using ContainerVervoer.Models;

namespace ContainerVervoer
{
    class Program
    {
        static void Main(string[] args)
        {
            Ship ship = new Ship();

            for(int i = 0; i < 60; i++)
                ship.AddContainer(new NormalContainer(30));
            
            for(int i = 0; i < 3; i++)
                ship.AddContainer(new CooledContainer(30));  
            
            for(int i = 0; i < 10; i++)
                ship.AddContainer(new ValuableContainer(30));  
            
            for(int i = 0; i < 2; i++)
                ship.AddContainer(new CoolValuableContainer(30));
                
            
            ship.Sort();
            
            ship.Export();
        }
    }
}