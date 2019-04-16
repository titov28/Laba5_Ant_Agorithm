using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntClassLibrary
{
    public class ControlObject
    {
        //матрица расстояний
        private int[,] distanceMatrix;  //=
                                        // {
                                        //     {  0,  84,  36, 109, 108 },
                                        //     { 84,   0,  52, 146,  89 },
                                        //     { 36,  52,   0, 107,  81 },
                                        //     {109, 146, 107,   0,  82 },
                                        //     {108,  89,  81,  82,   0 }
                                        //};

        private Point[] vertexArray;

        private double[,] pheromoneMatrix;

        private double Alpha;//коэф. настрйока важности феромона
        private double Beta;//коэф. настрйока важности длины ребра
        private double Rho; // коэф. испарения
        private int Q; // нормировочный коэффициент длины

        private List<int> T; // кротчаййший маршрут

        private int L; //длина кротчайшего маршрута

        private int TimeMax; // количество времени жизни колонии

        private Ant[] antArray;


        //public ControlObject()
        //{
        //    distanceMatrix = null;
        //    vertexArray = null;
        //    pheromoneMatrix = null;
        //}

        public ControlObject(Point[] vertex, double alpha, double beta, double rho, int q, int m, int antCount)
        {

            T = null;
            L = 0;

            this.vertexArray = vertex;
            InitDistanceMatrix();
            InitPheromoneMatrix();

            this.Alpha = alpha;
            this.Beta = beta;
            this.Rho = rho;
            this.Q = q;
            this.TimeMax = m;


            antArray = new Ant[antCount];
            int k = 0;

            for(int i = 0; i < antArray.Length; i++ , k++)
            {
                if (k < vertexArray.Length)
                {
                    antArray[i] = new Ant(vertexArray.Length, k, Alpha, Beta, Rho, Q, distanceMatrix, pheromoneMatrix);
                }
                else
                {
                    k = 0;
                    antArray[i] = new Ant(vertexArray.Length, k, Alpha, Beta, Rho, Q, distanceMatrix, pheromoneMatrix);
                }
            }

        }

        public void RouteSearch()
        {
            for(int i = 0; i < TimeMax; i++)
            {
                for(int j = 0; j < antArray.Length; j++)
                {
                    if (!antArray[j].GoNextCity())
                    {
                        if(T == null && L == 0)
                        {
                            T = antArray[j].T;
                            L = antArray[j].L;
                        }
                        else
                        {
                            if(L > antArray[j].L)
                            {
                                T = antArray[j].T;
                                L = antArray[j].L;
                            }
                        }

                        antArray[j].Restart();
                    }
                }
                CalculationOfPheromoneReduction();

            }
        }

        private void CalculationOfPheromoneReduction()
        {
            int locArrayLenght = vertexArray.Length;

            for (int i = 0; i < locArrayLenght; i++)
            {
                for (int j = 0; j < locArrayLenght; j++)
                {
                    if (distanceMatrix[i, j] != 0)
                    {
                        pheromoneMatrix[i, j] += (1 - Rho);// *pheromoneMatrix[i, j];
                    }


                    if(pheromoneMatrix[i, j] < 0.1)
                    {
                        pheromoneMatrix[i, j] = 0.1;
                    }

                }
            }
        }


        //public void SetVertexArray(Point[] vertexs)
        //{
        //    this.vertexArray = vertexs;
        //    InitDistanceMatrix();
        //    InitPheromoneMatrix();
        //}

        private void InitDistanceMatrix() // инициализация матрицы расстояний
        {
            int locArrayLenght = vertexArray.Length;
            distanceMatrix = new int[locArrayLenght, locArrayLenght];

            for(int i = 0; i < locArrayLenght; i++)
            {
                for(int j = 0; j < locArrayLenght; j++)
                {
                    distanceMatrix[i, j] = CalculateLenght(vertexArray[i].X, vertexArray[i].Y, vertexArray[j].X, vertexArray[j].Y);
                }
            }
        }

        private void InitPheromoneMatrix() // инициализация матрицы феромона
        {
            int locArrayLenght = vertexArray.Length;
            pheromoneMatrix = new double[locArrayLenght, locArrayLenght];

            for (int i = 0; i < locArrayLenght; i++)
            {
                for (int j = 0; j < locArrayLenght; j++)
                {
                    if (distanceMatrix[i, j] != 0)
                    {
                        pheromoneMatrix[i, j] = 0.1;
                    }
                }
            }
        }


        private int CalculateLenght(int X1, int Y1, int X2, int Y2) // расчет расстояния между двумя точками
        {
            return (int)Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
        }


        public void ShowDistanceMatrix()
        {

            Console.Write("\n");
            Console.Write("DistanceMatrix\n\n");

            for (int i = 0; i < distanceMatrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < distanceMatrix.GetUpperBound(1) + 1; j++)
                {
                    Console.Write("{0, 10}",distanceMatrix[i, j]);
                }

                Console.Write("\n");

            }
        }

        public void ShowPheromoneMatrix()
        {
            Console.Write("\n");
            Console.Write("PheromoneMatrix\n\n");

            for (int i = 0; i < pheromoneMatrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < pheromoneMatrix.GetUpperBound(1) + 1; j++)
                {
                    Console.Write("{0, 10:0.00}", pheromoneMatrix[i, j]);
                }

                Console.Write("\n");

            }
        }

        public void ShowTandL()
        {
            Console.Write("\n");
            Console.Write("The shortest route\n\n");

            for(int i = 0; i < T.Count; i++)
            {
                Console.Write("{0, 5}", T[i]);
            }

            Console.Write("\n\n");
            Console.Write("Shortest Route Length: {0, 5} \n\n", L);


        }
    }
}
