using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntClassLibrary
{
    public class Ant
    {
        private int[,] distanceMatrix;
        private double[,] pheromoneMatrix;

        private int[] Cities; // массив городов который надо посетить
        private int currentСity; // текущий город

        public List<int> T; // маршрут
        public int L = 0; // длина маршрута

        private double Alpha;//коэф. настрйока важности феромона
        private double Beta;//коэф. настрйока важности длины ребра
        private double Rho; // коэф. испарения
        private int Q; // нормировочный коэффициент длины



        public Ant(int cities ,int CurCity, double alpha, double beta, double rho, int q, int[,] distanceMatrix, double[,] pheromoneMatrix)
        {
            T = new List<int>();


            this.Alpha = alpha;
            this.Beta = beta;
            this.Rho = rho;
            this.Q = q;

            this.Cities = new int[cities];
            this.currentСity = CurCity;

            this.Cities[currentСity] = 1;

            

            this.distanceMatrix = distanceMatrix;
            this.pheromoneMatrix = pheromoneMatrix;

        }

        public void Restart()
        {
            for(int i = 0; i < Cities.Length; i++)
            {
                if(i != currentСity)
                {
                    Cities[i] = 0;
                }
            }

            T = new List<int>();
            L = 0;
        }

        public bool GoNextCity() //возвращает true если города для обхода есть, иначе false
        {
            if (CheckNext() > 1)
            {
                T.Add(currentСity);
                currentСity = GetNextCity();
                Cities[currentСity] = 1;
                pheromoneMatrix[T.Last(), currentСity] += Math.Round(CalculationPheromone(), 2);
            }
            else if(CheckNext() == 1)
            {
                for(int i = 0; i < Cities.Length; i++)
                {
                    if(Cities[i] == 0)
                    {
                        T.Add(currentСity);
                        currentСity = i;
                        Cities[currentСity] = 1;
                        pheromoneMatrix[T.Last(), currentСity] += Math.Round(CalculationPheromone(), 2);
                    }
                }
            }
            else
            {
                T.Add(currentСity);
                currentСity = T.ElementAt(0);
                pheromoneMatrix[T.Last(), currentСity] += Math.Round(CalculationPheromone(), 2);

                L = RouteLength();
                //по всем городам прошли
                return false;
            }
            
            return true;
        }

        private int GetNextCity()
        {
            Dictionary<int, double> dictionaryProbability = new Dictionary<int, double>();

            for (int i = 0; i < Cities.Length; i++)
            {
                if (Cities[i] == 0)
                {
                    dictionaryProbability.Add(i, CalculationProbability(i));
                }
            }

            var keyValuePair = dictionaryProbability.ToArray();

            double sum = 0;

            foreach(KeyValuePair<int, double> element in keyValuePair)
            {
                sum += element.Value;
            }

            Random rand = new Random();

            double randNumber = rand.NextDouble() * sum;

            int key = -1;
            sum = 0;

            foreach (KeyValuePair<int, double> element in keyValuePair)
            {
                sum += element.Value;

                if (randNumber < sum)
                {
                    key = element.Key;
                    break;
                }
            }

            return key;
        }


        private double CalculationProbability(int indexCity)
        {
            double sum = 0;

            for(int i = 0; i < Cities.Length; i++)
            {
                if (Cities[i] == 0 && i != indexCity)
                {
                    sum += Math.Pow(pheromoneMatrix[currentСity, i], Alpha) * Math.Pow(CalculationHeuristicDesire(i), Beta);
                }
            }

            return (Math.Pow(pheromoneMatrix[currentСity, indexCity], Alpha) * Math.Pow(CalculationHeuristicDesire(indexCity), Beta))/sum;
        }

        private double CalculationHeuristicDesire(int indexCity)
        {
            return Math.Round((1/(double)distanceMatrix[currentСity, indexCity]), 3);
        }

        private int CheckNext()
        {
            int count = 0;
            for(int i = 0; i < Cities.Length; i++)
            {
                if(Cities[i] == 0)
                {
                    count++;
                }
            }

            return count;
            
        }


        private double CalculationPheromone()
        {
            return Q / (double)distanceMatrix[T.Last(), this.currentСity];
            //return Q / (double)RouteLength();
        }


        private int RouteLength()
        {
            int length = 0;

            for(int i = 0; i < T.Count - 1; i++)
            {
                length += distanceMatrix[T.ElementAt(i), T.ElementAt(i+1)]; 
            }

            length += distanceMatrix[T.Last(), T.ElementAt(0)];

            return length;
        }

        
    }
}
