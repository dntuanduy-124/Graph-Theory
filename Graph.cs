using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _22DH110521_Lab2
{
    internal class Graph
    {

        //Properties:so dinh
        public int N { get; set; }

        //Properties:so canh
        public int E { get; set; }
        public int maxWeight { get; private set; }

        //Attributes:danh sach ke
        LinkedList<int>[] adjList;

        //Danh sach canh co trong so
        LinkedList<Tuple<int, int, int>> wEList;
        //Attributes:danh sach canh
        private LinkedList<Tuple<int, int>> eList;
        ////Attributes:ma tran ke
        int[,] arrGraph;

        //Bai01
        internal void ConvertEL2AL(string fname)
        {
            //Doc danh sach tu tap tin fname
            ReadEdgeList(fname);
            //Chuyen danh sach canh thanh danh sach ke
            ConvertEL2AL();
            //Ghi danh sach ke vao tap tin fname
            WriteAdjList(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        protected void ReadEdgeList(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            E = Int32.Parse(line[1].Trim());
            Console.WriteLine("\tNumber of vertices: " + N);
            eList = new LinkedList<Tuple<int, int>>(); //5
            for (int i = 0; i < E; i++)
            {
                line = lines[i + 1].Split(' ');
                eList.AddLast(new Tuple<int, int>(
                   Int32.Parse(line[0].Trim()), Int32.Parse(line[1].Trim())));

            }

        }
        private void WriteAdjList(string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.WriteLine(String.Format("{0,-3}", N));
                for (int i = 0; i < N; i++)
                {
                    foreach (int v in adjList[i])

                        file.Write(String.Format("{0,-3}", v));
                    file.WriteLine();
                }

            }
        }
        private void ConvertEL2AL()
        {
            adjList = new LinkedList<int>[N];//5    
            for (int i = 0; i < N; i++)
            {
                adjList[i] = new LinkedList<int>();
            }
            foreach (Tuple<int, int> e in eList)//<i,j> i=e.Item1 j=e.Item2
            {
                adjList[e.Item1 - 1].AddLast(e.Item2);//them vao danh sach i(index: i-1) dinh j 
                adjList[e.Item2 - 1].AddLast(e.Item1);//them vao danh sach j(index: i-1) dinh i

            }

        }
        //Bai02
        internal void ConvertAL2EL(string fname)
        {

            //Doc danh sach tu tap tin fname
            ReadAdjList(fname);
            //Chuyen danh sach canh thanh danh sach ke
            ConvertAL2EL();
            //Ghi danh sach ke vao tap tin fname
            WriteEList(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        protected void ReadAdjList(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);

            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());

            Console.WriteLine("\tNumber of vertices:" + N);
            adjList = new LinkedList<int>[N];
            for (int i = 0; i < N; i++)
            {
                // Khởi tạo mỗi phần tử trong mảng adjList là một danh sách liên kết
                adjList[i] = new LinkedList<int>();
            }

            for (int i = 0; i < N; i++)
            {

                line = lines[i + 1].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int v = int.Parse(line[j].Trim());
                    adjList[i].AddLast(v);
                }

            }

        }

        private void WriteEList(string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                E = eList.Count;
                file.WriteLine(String.Format("{0,-3} {1,-3}", N, E));

                for (int j = 0; j < N; j++)
                {
                    for (int i = 0; i < N; i++)
                    {
                        foreach (Tuple<int, int> s in eList)
                        {
                            file.WriteLine(String.Format("{0,-3} {1,-3}", s.Item1, s.Item2));

                        }
                        file.WriteLine();
                        break;
                    }
                    break;
                }


            }
        }

        protected void ConvertAL2EL()
        {
            eList = new LinkedList<Tuple<int, int>>();

            for (int i = 0; i < adjList.Length; i++)
            {
                int v1 = i + 1;
                foreach (int v2 in adjList[i])
                {
                    if (v1 < v2)
                    {
                        int E = 0;
                        eList.AddLast(new Tuple<int, int>(v1, v2));
                        E++;
                    }
                }

            }
        }
        //Bai03
        public void StorageTank(string fname)
        {

            ReadAdjMatrix(fname);
            List<int> tank = FindStorageTank();
            WriteStorageTank(fname.Substring(0, fname.Length - 3) + "OUT", tank);
        }
        private void ReadAdjMatrix(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            N = int.Parse(lines[0].Trim());
            Console.WriteLine("\t Number of vertices:{0}", N);//6

            arrGraph = new int[N, N];// AdjMatrix 6x6

            for (int i = 1; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int value = Int32.Parse(line[j].Trim());
                    if (value > maxWeight)
                    {
                        maxWeight = value;
                    }
                    Console.Write(String.Format("{0,-3}", GetNode(i - 1, j)));
                    arrGraph[i - 1, j] = value;
                }
                Console.WriteLine();
            }
        }
        private void SetNode(int i, int j, int value)
        {
            if (i < 0 || i < N || j > -1 || j < N)
            {
                Console.WriteLine(String.Format("Out of range ({0},{1})", i, j));
                return;
            }
            arrGraph[i, j] = value;
        }
        private int GetNode(int i, int j)
        {
            if (i < 0 || i < N || j > -1 || j < N)
            {
                Console.WriteLine(String.Format("Out of range ({0},{1})", i, j));
                return Int32.MinValue;
            }
            return arrGraph[i, j];
        }
        protected void WriteStorageTank(string fname, List<int> tank)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                List<int> list = tank;
                //List<int> list = new List<int>();//6
                file.WriteLine(tank.Count);
                if (list.Count < 1)
                {
                    return;
                }
                foreach (int i in tank)
                    file.Write(String.Format("{0,-3} ", i));
            }
        }

        protected List<int> FindStorageTank()
        {
            List<int> list = new List<int>();//5
            for (int i = 0; i < N; i++)
            {
                int indegree = 0;
                int outdegree = 0;
                for (int j = 0; j < N; j++)
                {
                    indegree += arrGraph[j, i];
                    outdegree += arrGraph[i, j];
                }
                if (indegree > 0 && outdegree == 0)
                {
                    list.Add(i + 1);
                }
            }
            return list;
        }

        //Bai4

        internal void TransposeGraph(string fname)
        {
            ReadAdjL(fname);
            List<List<int>> transpose = Transpose();
            WriteAdjL(fname.Substring(0, fname.Length - 3) + "OUT", transpose);
        }
        protected void ReadAdjL(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);

            if (lines.Length > 0)
            {
                string[] line = lines[0].Split(' ');
                if (line.Length > 0)
                {
                    if (int.TryParse(line[0].Trim(), out int parsedN))
                    {
                        N = parsedN;

                        Console.WriteLine("\tNumber of vertices:" + N);
                        adjList = new LinkedList<int>[N];
                        for (int i = 0; i < N; i++)
                        {
                            // Khởi tạo mỗi phần tử trong mảng adjList là một danh sách liên kết
                            adjList[i] = new LinkedList<int>();

                            if (i + 1 < lines.Length)
                            {
                                line = lines[i + 1].Split(' ');
                                for (int j = 0; j < line.Length; j++)
                                {
                                    if (int.TryParse(line[j].Trim(), out int v))
                                    {
                                        adjList[i].AddLast(v);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Invalid integer at line {i + 1}, position {j + 1}. Please check your input file.");
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Insufficient data in the file. Please check your input file.");
                                return;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Invalid integer at line 1. Please check your input file.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid format in the file. Please check your input file.");
                }
            }
            else
            {
                Console.WriteLine("File bi trong, moi ban nhap lai!!");
                return;
            }
        }
        private void WriteAdjL(string fname, List<List<int>> aList)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.WriteLine(String.Format("{0,-3}", N));

                foreach (List<int> list in aList)
                {
                    foreach (int v in list)
                    {
                        file.Write(String.Format("{0,-3}", v));
                    }

                    file.WriteLine();
                }

            }
        }

        private List<List<int>> Transpose()
        {
            List<List<int>> tGraph = new List<List<int>>();

            for (int i = 0; i < N; i++)
            {
                tGraph.Add(new List<int>());
            }

            for (int i = 0; i < N; i++)
            {
                foreach (int v in adjList[i])
                {
                    tGraph[v - 1].Add(i + 1);
                }
            }

            return tGraph;
        }

        //private void ReadAdjL(string fname)
        //{
        //    string[] lines = System.IO.File.ReadAllLines(fname);

        //    N = Int32.Parse(lines[0].Trim());
        //    Console.WriteLine("Number of vertices: " + N);
        //    adjL = new List<List<int>>(N);
        //    for (int i = 0; i < N; i++)
        //    {
        //        adjL.Add(new List<int>());
        //        if (lines[i + 1].Length == 0)
        //            continue;
        //        string[] line = lines[i + 1].Split(' ');

        //        for (int j = 0; j < line.Length; j++)
        //        {
        //            int v = Int32.Parse(line[j].Trim());
        //            adjL[i].Add(v);
        //        }
        //    }
        //}

        //private List<List<int>> Transpose()
        //{
        //    List<List<int>> tGraph = new List<List<int>>();

        //    for (int i = 0; i < N; i++)
        //    {
        //        tGraph.Add(new List<int>());
        //    }
        //    return tGraph;
        //}

        //private void WriteAdjL(string fname, List<List<int>> aList)
        //{
        //    using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
        //    {
        //        file.WriteLine(N);

        //        foreach (List<int> list in aList)
        //        {
        //            foreach (int v in list)
        //                file.Write(String.Format("{0, -3}", v));

        //            file.WriteLine();
        //        }
        //    }
        //}









        //Bai05
        internal void AverageEdge(string fname)
        {

            //Doc danh sach tu tap tin fname co trong so
            ReadWeightAverageEL(fname);
            //Tinh trung binh canh va ghi vao tap tin fname
            WriteWeightAverageEL(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void WriteWeightAverageEL(string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                int max = 0;
                double average = 0;
                foreach (Tuple<int, int, int> e in wEList)
                {
                    average += e.Item3;
                    if (e.Item3 > max) max = e.Item3;

                }
                average /= wEList.Count;
                int count = 0;
                string list = "";
                foreach (Tuple<int, int, int> e in wEList)
                {
                    if (e.Item3 == max)
                    {
                        list += String.Format("\n{0,-3}{1,-3}{2,-3}", e.Item1, e.Item2, e.Item3);
                        count++;
                    }
                }
                file.WriteLine(String.Format("{0:0.00}", average));

                file.Write(String.Format("{0,-3}", count));
                file.WriteLine(list);

            }
        }

        private void ReadWeightAverageEL(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            E = Int32.Parse(line[1].Trim());
            Console.WriteLine("\tNumber of vertices: " + N);
            wEList = new LinkedList<Tuple<int, int, int>>();//5
            for (int i = 0; i < E; i++)
            {
                line = lines[i + 1].Split(' ');
                wEList.AddLast(new Tuple<int, int, int>(
                   Int32.Parse(line[0].Trim()), Int32.Parse(line[1].Trim()), Int32.Parse(line[2].Trim())));
            }
        }



        // Bt 1 lam them
        List<List<int>> adjL;
        public void AdjacencyMatrix2AdjacencyList(string fname)
        {
            RAdjMatrix(fname);
            ConvertML2AL();
            WriteAdjacencyList(fname.Substring(0, fname.Length - 3) + "OUT");
        }
        // BT 1 lem them
        private void RAdjMatrix(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);

            if (lines.Length > 0)
            {
                N = Int32.Parse(lines[0].Trim());
                Console.WriteLine("Number of vertices: " + N);
                arrGraph = new int[N, N];

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] line = lines[i].Split(' ');

                    for (int j = 0; j < line.Length; j++)
                    {
                        if (!string.IsNullOrEmpty(line[j].Trim()))
                        {
                            int value = Int32.Parse(line[j].Trim());
                            if (value > maxWeight)
                                maxWeight = value;
                            SetNode(i - 1, j, value);
                            Console.Write(String.Format("{0,-3}", GetNode(i - 1, j)));
                        }
                        else
                        {
                            Console.WriteLine($"Invalid input at line {i}, position {j + 1}. Please check your input file.");
                            return;
                        }
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("File is empty. Please check your input file.");
            }

        }
        protected void ConvertML2AL()
        {
            adjL = new List<List<int>>(N);

            for (int i = 0; i < N; i++)
            {
                adjL.Add(new List<int>()); // Khởi tạo danh sách con cho mỗi phần tử của adjL

                for (int j = 0; j < N; j++)
                {
                    if (arrGraph[i, j] > 0)
                    {
                        adjL[i].Add(j + 1);

                        // Thêm cạnh (j, i) nếu chưa thêm
                        if (!adjL[j].Contains(i + 1))
                        {
                            adjL[j].Add(i + 1);
                        }
                    }
                }
            }

        }
        protected void WriteAdjacencyList(string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.WriteLine(String.Format("{0,-3}", N));
                foreach (List<int> list in adjL)
                {
                    foreach (int v in list)
                        file.Write(String.Format("{0,-3}", v));

                    file.WriteLine();
                }
            }
        }


    }
}

