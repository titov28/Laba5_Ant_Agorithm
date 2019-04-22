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
                new Point(50,27),    //1
                new Point(24, 107),  //2
                new Point(51, 63),   //3
                new Point(157, 46),  //4
                new Point(113, 115), //5
                new Point(60, 128),  //6
                new Point(113, 44),  //7
                new Point(130, 80),  //8
            };

            Console.WriteLine("|{0, 10}|{1, 10}|{2, 10}|{3, 10}|{4, 10}|{5, 10}|", "Alpha", "Beta", "Rho", "iterations", "time", "L");
            Console.WriteLine("|{0, 10}|{1, 10}|{2, 10}|{3, 10}|{4, 10}|{5, 10}|", "----------", "----------", "----------", "----------", "----------", "----------");
            ControlObject trl = new ControlObject(pnt, 0.25, 0.75, 0.5, 100, 1000, 8);
            trl.RouteSearch();
            trl.Print();
            trl.ShowDistanceMatrix();
            trl.ShowPheromoneMatrix();
            trl.ShowTandL();


            Console.Write("\n");

            Console.WriteLine("|{0, 10}|{1, 10}|{2, 10}|{3, 10}|{4, 10}|{5, 10}|", "Alpha", "Beta", "Rho", "iterations", "time", "L");
            Console.WriteLine("|{0, 10}|{1, 10}|{2, 10}|{3, 10}|{4, 10}|{5, 10}|", "----------", "----------", "----------", "----------", "----------", "----------");
            for (double i = 0; i <= 1; i += 0.25)
            {
                for(double j = 0; j <= 1; j += 0.25)
                {
                    ControlObject tr2 = new ControlObject(pnt, i, 1 - i, j, 100, 1000, 8);
                    tr2.RouteSearch();
                    tr2.Print();
                }
                Console.WriteLine("|{0, 10}|{1, 10}|{2, 10}|{3, 10}|{4, 10}|{5, 10}|", "----------", "----------", "----------", "----------", "----------", "----------");
            }

            Console.ReadLine();
        }
    }
}
