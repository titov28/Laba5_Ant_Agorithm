using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntClassLibrary;
namespace MainProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Point[] pnt = 
            {
                new Point(50,27),
                new Point(24, 107),
                new Point(51, 63),
                new Point(157, 46),
                new Point(113, 115)
            };

            ControlObject trl = new ControlObject(pnt, 0.25, 0.75, 0.25, 100, 30, 5);
            trl.RouteSearch();
            trl.ShowTandL();

            trl.ShowPheromoneMatrix();
            Console.ReadLine();
        }
    }
}
