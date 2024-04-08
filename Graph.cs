using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace lab08ltdt
{
    internal class Graph
    {
        //Constant
        public const int Inf = 1000;
        //max weight
        protected int maxWeight = 0;
        //Adjacency matrix
        double[,] arrGraph;
        //List Circles
        List<Tuple<int, int, int>> listCircle;//2
        // Adjacency List: Weight
        List<List<Tuple<int, int>>> adjWList;// 1
        // Adjacency List: Weight
        List<List<Tuple<int, int>>> adjWListG2S;// 3,4
        // Number of Vertices
        int numVertices;
        // Number of Edges
        int numEdges;
        // Number of Rows
        int nRow;
        // Number of Columns
        int nCol;
        // Start vertex 
        int sVertex;
        // End vertex 
        int eVertex;
        // Intermediary vertex 
        int iVertex;
        // Start node 
        Tuple<int, int> sNode;
        bool[] color;
        int[] back;
        // Distance for shortest path 
        double[] distance;



        //Bai1
        public void GoMargin(string fname)
        {
            ReadGoMargin(fname); // Task 1.1
            GoMargin(); // Task 1.2
            int g = (int)Point2Num(sNode.Item1, sNode.Item2);
            WriteGoMargin(fname.Substring(0, fname.Length - 3) + "out", (int)distance[eVertex] + (int)distance[sVertex + 13]); // Task 1.3


        }

        // Task 1.1.
        protected void ReadGoMargin(string fname)
        {
            string[] lines = File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            nRow = int.Parse(line[0].Trim());
            nCol = int.Parse(line[1].Trim());
            numVertices = nRow * nCol;
            sNode = new Tuple<int, int>(int.Parse(line[2].Trim()) - 1, int.Parse(line[3].Trim()) - 1);
            sVertex = Point2Num(sNode.Item1, sNode.Item2);//

            arrGraph = new double[nRow, nCol]; // 
            eVertex = nRow * nCol;

            for (int i = 1; i < lines.Length; i++)
            {
                line = lines[i].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    SetNode(i - 1, j, Int32.Parse(line[j].Trim())); // 

                }
            }
        }

        //
        private int Point2Num(int i, int j)
        {
            return i * nCol + j;
        }

        // 
        public void SetNode(int i, int j, int value)
        {
            if (i < 0 && i < numVertices && j > -1 && j < numVertices)
            {
                Console.WriteLine($"Index out of range: ({i},{j})");
                return;
            }
            arrGraph[i, j] = value;
        }

        // 
        public int GetNode(int i, int j)
        {
            if (i < 0 && i < numVertices && j < 0 && j >= numVertices)
            {
                Console.WriteLine($"Index out of range: ({i},{j})");
                return Int32.MinValue;
            }
            return (int)arrGraph[i, j];
        }

        // 
        protected void GoMargin()
        {
            InitBoolArray(false); // 
            InitIntArray(-1, 1); // 
            InitIntArray(Inf, 2);

            int[] dx = { -1, 0, 0, 1 };
            int[] dy = { 0, -1, 1, 0 };

            int g = Point2Num(sNode.Item1, sNode.Item2);
            distance[g] = 0;

            do
            {
                g = eVertex;

                for (int i = 0; i < numVertices; i++)
                {
                    if (!color[i] && distance[g] > distance[i])
                    {
                        g = i;
                    }
                }

                if (distance[g] == Inf || g == eVertex)
                {
                    break;
                }

                if (IsMargin(g))
                {
                    eVertex = g;
                    break;
                }

                color[g] = true;

                int xcur = 0;
                int ycur = 0;

                Num2Point(g, ref xcur, ref ycur);

                for (int i = 0; i < 4; i++)
                {
                    int x = xcur + dx[i];
                    int y = ycur + dy[i];

                    if (!InBoard(x, y))
                        continue;

                    int v = Point2Num(x, y);
                    if (!color[v])
                    {
                        double d = distance[g] + arrGraph[x, y];
                        if (distance[v] > d)
                        {
                            distance[v] = d;
                            back[v] = g;
                        }
                    }
                }

            } while (true);

        }


        private void InitBoolArray(bool value = false)
        {
            color = new bool[numVertices];
            for (int i = 0; i < color.Length; i++)
                color[i] = value;
        }
        protected void InitIntArray(int value, int type = 1)
        {
            if (type == 1)
            {
                back = new int[numVertices];
                for (int i = 0; i < back.Length; i++)
                    back[i] = value;
                return;
            }
            if (type == 2)
            {
                distance = new double[numVertices + 1];
                for (int i = 0; i < distance.Length; i++)
                    distance[i] = value;
                return; // Remove the extra increment here
            }
        }



        // Task 1.2.3
        private bool IsMargin(int g)
        {
            int i = 0;
            int j = 0;
            Num2Point(g, ref i, ref j);
            return (i == 0) || (i == (nRow - 1)) || (j == 0) || (j == (nCol - 1));
        }

        // Task 1.2.4
        private void Num2Point(int num, ref int i, ref int j)
        {
            i = num / nCol;
            j = num % nCol;
        }

        // Task 1.2.5
        private bool InBoard(int x, int y)
        {
            return (x > -1) && (x < nRow) && (y > -1) && (y < nCol);
        }
        private void printArray(int type = 1)
        {
            if (type == 1)
            {
                foreach (int d in distance)
                    Console.WriteLine(String.Format("{0,-5}", d));
                Console.WriteLine();
                return;
            }
            if (type == 2)
            {
                foreach (int d in back)
                    Console.Write(String.Format("{0,-5}", d));
                Console.WriteLine();
                return;
            }
        }




        private void WriteGoMargin(string fname, int value, bool isInt = true)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                if (isInt)
                    file.Write(String.Format("{0:F0}", value));
                else
                    file.Write(String.Format("{0:F2}", value));
            }
        }


        //Bai 2
        public void ChooseCity(string fname)
        {
            ReadMatrix2AdjList(fname); // Task 2.1
            eVertex = numVertices;
            // Task 2.2
            Tuple<int, double> city = ChooseCity();
            WriteCity(fname.Substring(0, fname.Length - 3) + "out", city); // Task 2.3
        }
        protected void ReadMatrix2AdjList(string fname)
        {
            // adjacency List 
            string[] lines = System.IO.File.ReadAllLines(fname);
            numVertices = Int32.Parse(lines[0].Trim());
            Console.WriteLine("Number of vertices: " + numVertices);
            adjWList = new List<List<Tuple<int, int>>>();

            for (int i = 0; i < numVertices; i++)
            {
                adjWList.Add(new List<Tuple<int, int>>());
            }

            for (int i = 0; i < numVertices; i++)
            {
                string[] line = lines[i + 1].Trim().Split(' ');

                for (int j = 0; j < line.Length; j++)
                {
                    int w = Int32.Parse(line[j].Trim()); //w weight
                    if (w == 0)
                        continue;
                    adjWList[i].Add(new Tuple<int, int>(j, w));
                    //adjWList[j].Add(new Tuple<int, int>(i, w)); // Uncomment this line if the graph is undirected
                }
            }

            Console.WriteLine("Read complete (end of file)!");
        }

        protected Tuple<int, double> ChooseCity()
        {
            Tuple<int, double> res = new Tuple<int, double>(-1, Inf);

            for (int v = 0; v < numVertices; v++)
            {
                AdjMatrixShortestPath(v); // Task 2.2.1

                double dist = distance[Max()]; // Task 2.2.2

                if (res.Item2 > dist)
                {
                    res = new Tuple<int, double>(v, dist);
                }
            }

            return res;

        }
        protected void AdjMatrixShortestPath(int g)
        {
            InitIntArray(-1); // Initialize back array
            InitIntArray(Inf, 2); // Initialize distance array
            InitBoolArray(false); // Initialize color array
            distance[g] = 0;

            do
            {
                g = eVertex;

                for (int i = 0; i < numVertices; i++)
                {
                    if (!color[i] && distance[g] > distance[i])
                    {
                        g = i;
                    }
                }

                if (distance[g] == Inf || g == eVertex)
                {
                    break;
                }

                color[g] = true;
                //adjWList=new List<List<Tuple<int, int>>>(g);
                foreach (Tuple<int, int> city in adjWList[g])
                {
                    int v = city.Item1;
                    int w = city.Item2;

                    if (!color[v])
                    {
                        double d = distance[g] + w;

                        if (distance[v] > d)
                        {
                            distance[v] = d;
                            back[v] = g;
                        }
                    }
                }
            } while (true);
        }
        private int Max()
        {
            int pos = 0;

            for (int i = 1; i < numVertices; i++)
            {
                if (distance[pos] < distance[i])
                {
                    pos = i;
                }
            }

            return pos;
        }
        private void WriteCity(string fname, Tuple<int, double> city)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.WriteLine(city.Item1 + 1);
                file.WriteLine("{0:f0}", city.Item2); // Print distance with 2 decimal places
            }
        }
        //Bai03
        internal void MoveCircle(string fname)
        {
            ReadMoveCircle(fname);
            GemerateAdjMatrix();
            DAdjMatrixShortestPath(sVertex);
            WriteCircle(fname.Substring(0, fname.Length - 3) + "OUT", distance[eVertex]);
        }

        private void ReadMoveCircle(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            numVertices = Int32.Parse(line[0].Trim());
            sVertex = Int32.Parse(line[1].Trim()) - 1;
            eVertex = Int32.Parse(line[2].Trim()) - 1;
            Console.WriteLine("\t Number of vertices: " + numVertices);
            listCircle = new List<Tuple<int, int, int>>();
            for (int i = 0; i < numVertices; i++)
            {
                line = lines[i + 1].Split(' ');
                int x = Int32.Parse((line[0]).Trim());
                int y = Int32.Parse((line[1]).Trim());
                int r = Int32.Parse((line[2]).Trim());
                listCircle.Add(new Tuple<int, int, int>(x, y, r));
            }
        }

        private void GemerateAdjMatrix()
        {
            arrGraph = new double[numVertices, numVertices];
            int sz = numVertices - 1;
            for (int i = 0; i < sz; i++)
            {
                arrGraph[i, i] = 0;
                for (int j = 0; j < numVertices; j++)
                {
                    double dis = Math.Max(0, Math.Sqrt(Math.Pow(listCircle[i].Item1 - listCircle[j].Item1, 2) + Math.Pow(listCircle[i].Item2 - listCircle[j].Item2, 2)) - listCircle[i].Item3 - listCircle[j].Item3);
                    arrGraph[i, j] = dis;
                    arrGraph[j, i] = dis;
                }
            }
        }

        private void DAdjMatrixShortestPath(int g)
        {
            InitBoolArray(false);
            InitIntArray(-1);
            InitIntArray(Inf, 2);
            distance[g] = 0;
            do
            {
                g = eVertex;
                for (int i = 0; i < numVertices; i++)
                {
                    if (!color[i] && distance[g] > distance[i])
                    {
                        g = i;
                    }
                }
                if ((distance[g] == Inf) || g == eVertex)
                    break;
                color[g] = true;
                for (int v = 0; v < numVertices; v++)
                {
                    if (!color[v])
                    {
                        double d = distance[g] + arrGraph[g, v];
                        if (distance[v] > d)
                        {
                            distance[v] = d;
                            back[v] = g;
                        }
                    }
                }
            }
            while (true);
        }
        private void WriteCircle(string fname, double value, bool isInt = true)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                if (isInt)
                    file.Write(String.Format($"{Math.Round(value, 2)}"));
                else
                    file.Write(String.Format($"{value}"));
            }
        }


        //Bai 4

        public void Go2School(string fname)
        {
            ReadG2S(fname); // Task 4.1
            int value = G02School(); // Task 4.2
            WriteGoMargin4(fname.Substring(0, fname.Length - 3) + "out", value); // Task 4.3
        }

        private void WriteGoMargin4(string fname, int value, bool isInt = true)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                if (isInt)
                    file.Write(String.Format("{0:F0}", value));
                else
                    file.Write(String.Format("{0:F2}", value));
            }
        }

        protected void ReadG2S(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            numVertices = Int32.Parse(line[0].Trim());
            numEdges = Int32.Parse(line[1].Trim());
            sVertex = Int32.Parse(line[2].Trim()) - 1;
            eVertex = Int32.Parse(line[2].Trim())  ;

            Console.WriteLine("\t Number of vertices: " + numVertices);
            if (line.Length > 4)
                iVertex = Int32.Parse(line[4].Trim()) - 1;
            Console.WriteLine("\t Number of vertices : " + numVertices);

            adjWListG2S = new List<List<Tuple<int, int>>>();

            for (int i = 0; i < numVertices; i++)
            {
                adjWListG2S.Add(new List<Tuple<int, int>>());
            }

            for (int i = 0; i < numEdges; i++)
            {
                line = lines[i + 1].Split(' ');
                Console.WriteLine(lines[i + 1]);
                int v1 = Int32.Parse(line[0].Trim()) - 1;
                int v2 = Int32.Parse(line[1].Trim()) - 1;
                int w1 = Int32.Parse(line[2].Trim());
                int w2 = Int32.Parse(line[3].Trim());
                adjWListG2S[v1].Add(new Tuple<int, int>(v2, Math.Min(w1, w2)));
            }
        }

        protected int G02School()
        {
            int temp = eVertex;
            eVertex = iVertex;

            InitIntArray(-1); // Path
            InitIntArray(Inf, 2); // Distance - d
            InitBoolArray(false); // Status
            distance[sVertex] = 0;
            SubSPG2S(sVertex); // shortest(l,k) // Task 4.2.1
            List<int> phase1 = TracePath(); // duong di 1->k // Task 4.2.2
            eVertex = temp; // revert eVertex to its original value
            for (int i = 0; i < numVertices; i++)
            {
                if (!phase1.Contains(i + 1))
                {
                    color[i] = false;
                    back[i] = -1;
                    // distance[i] = Inf; // Uncomment this line if you want to reset distance[i] to Inf
                }
            }
            SubSPG2S(iVertex);
            return (int)distance[eVertex]; // return the distance to iVertex
        }

        private void SubSPG2S(int g)
        {
            do
            {
                g = eVertex;
                for (int i = 0; i < numVertices; i++)
                {
                    if (!color[i] && distance[g] > distance[i])
                    {
                        g = i;
                    }
                }

                if ((distance[g] == Inf) || g == eVertex)
                {
                    break;
                }

                color[g] = true;

                foreach (Tuple<int, int> v in adjWListG2S[g])
                {
                    if (!color[v.Item1])
                    {
                        int w = v.Item2;
                        double d = distance[g] + w;
                        if (distance[v.Item1] > d)
                        {
                            distance[v.Item1] = d;
                            back[v.Item1] = g;
                        }
                    }
                }
            } while (true);
        }

        protected List<int> TracePath()
        {
            List<int> res = new List<int>();
            int y = eVertex;
            int x = sVertex;
            res.Add(y);
            int v = back[y];
            if (v != -2)
            {
                while (v != x)
                {
                    res.Insert(0, v);
                    v = back[v];
                }
                if (v == x)
                {
                    res.Insert(0, v);
                }
            }
            return res;
        }
    }
}

