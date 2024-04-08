using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab03_22DH110521
{
    internal class Graph
    {
        //Properties:số đỉnh
        public int N { get; set; }

        //Properties:đỉnh bắt đầu
        public int S { get; set; }
        //Properties:đỉnh kết thúc
        public int T { get; set; }

        public int list { get; set; }
        public int sVertex { get; set; }


        //Attributes:danh sách kề
        LinkedList<int>[] adjList;
        //Attributes:Mang kt duoc vieng tham hay chua
        bool[] visited;
        //Attributes:danh lien truoc cua dinh ()dinh cha
        int[] pre;


        //Bai01
        internal void ConnectedVertex(string fname)
        {
            //Doc ds ke tu tap tin fname
            ReadAdjList(fname);

            //Duyet do thi de tim cac dinh lien thong voi dinh s
            BFSBai1();

            //Viet dinh lien thong vao tap tin
            WriteAdjList(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void BFSBai1()
        {
            //Khoi tao visited va pre 

            visited = new bool[N + 1];
            pre = new int[N + 1];
            BFSBai1(S);
        }
        List<int> lien_thong = new List<int>();
        public void BFSBai1(int s)
        {
            //Khởi tạo
            Queue<int> q = new Queue<int>();

            visited[s] = true;

            pre[s] = -1;

            q.Enqueue(s);//Đưa vào hàng đợi


            while (q.Count != 0)
            {
                int u = q.Dequeue();//Lấy ra

                foreach (int v in adjList[u - 1])
                {
                    if (visited[v])//=true
                        continue;

                    visited[v] = true;
                    pre[v] = u;
                    q.Enqueue(v);
                    lien_thong.Add(v);
                }
            }
        }




        private void ReadAdjList(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            S = Int32.Parse(line[1].Trim());

            adjList = new LinkedList<int>[N];

            for (int i = 0; i < N; i++)
            {
                adjList[i] = new LinkedList<int>();
                if (lines[i + 1].Length == 0)
                    continue;
                line = lines[i + 1].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int v = int.Parse(line[j].Trim());
                    adjList[i].AddLast(v);
                }


            }
        }








        private void WriteAdjList(string fname)
        {
            //int count = 0;
            string str = "";


            for (int i = 0; i < lien_thong.Count; i++)
            {

                {

                    str += String.Format("{0,-3}", lien_thong[i]);

                }

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
                {

                    if (lien_thong.Count == 0)
                    {

                        file.Write(String.Format("Không có đỉnh liên thông với {0}", S));
                        continue;
                    }
                    file.WriteLine(lien_thong.Count);

                    file.Write(str);


                    // Ghi các đỉnh liên thông
                    //for (int j = 0; j < pre.Length; j++)
                    //{
                    //    if (pre[i] > 0)
                    //    {
                    //        foreach (int v in adjList[i])
                    //        {
                    //            file.Write(String.Format("{0,-3}", v));
                    //        }
                    //    }
                    //}


                }
            }


        }

        //Bai02
        public void PathBFS(string fname)
        {
            //Đọc danh sách kề từ tập tin
            ReadAdjList2(fname);

            //Duyệt đồ thị tìm đường đi từ s đến t
            PathBFSBai2();

            //Viết danh sách các đỉnh trong đường đi từ s đến t vào tập tin
            WritePathBFS(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void BFS()
        {
            //Khoi tao visited va pre 

            visited = new bool[N + 1];
            pre = new int[N + 1];
            BFS(S);
        }
        public void BFS(int s)
        {
            //Khởi tạo
            Queue<int> q = new Queue<int>();

            visited[s] = true;

            pre[s] = -1;

            q.Enqueue(s);//Đưa vào hàng đợi


            while (q.Count != 0)
            {
                int u = q.Dequeue();//Lấy ra

                foreach (int v in adjList[u - 1])
                {
                    if (visited[v])//=true
                        continue;

                    visited[v] = true;
                    pre[v] = u;
                    q.Enqueue(v);

                }
            }

        }

        private void PathBFSBai2()
        {
            BFS();
        }

        private void ReadAdjList2(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            S = Int32.Parse(line[1].Trim());
            T = Int32.Parse(line[2].Trim());

            adjList = new LinkedList<int>[N];

            for (int i = 0; i < N; i++)
            {
                adjList[i] = new LinkedList<int>();
                if (lines[i + 1].Length == 0)
                    continue;
                line = lines[i + 1].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int v = int.Parse(line[j].Trim());
                    adjList[i].AddLast(v);
                }

            }
        }
        protected void WritePathBFS(string fname)
        {
            List<int> path = TracePath(S, T);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {

                if (path.Count == 0)
                {

                    file.Write(string.Format("không có đường đi từ {0} đến {1}", S, T));
                    return;
                }
                file.WriteLine(path.Count);
                foreach (int v in path)

                    file.Write(String.Format("{0,-3}", v));
            }
        }
        //protected void PathBFS1()
        //{
        //    //Gọi lại hàm duyệt đồ thị từ đỉnh s
        //    BFS();//Hàm BFS ở câu 1
        //}

        protected List<int> TracePath(int s, int t)
        {
            List<int> path = new List<int>();
            int v = pre[t];

            if (v > 0)
                path.Add(t);

            {
                while (true)
                {
                    path.Insert(0, v);
                    v = pre[v];
                    if (v == -1)
                        break;
                }
            }

            return path;
        }


        // Bai 3
        public void ktraLienThong(string fname)
        {
            ReadAdjListBFSBai3(fname);
            BFS3();
            WriteBFSBai3(fname.Substring(0, fname.Length - 3) + "OUT");//////
        }

        //// Bai 4
        //public void demLienThong(string fname)
        //{
        //    ReadAdjListBFSBai4(fname);
        //    BFS4();
        //    WriteBFSBai4(fname.Substring(0, fname.Length - 3) + "OUT");
        //}


        //Bai03
        public void BFSConnected(string fname)
        {
            ReadAdjListBFSBai3(fname);
            BFS3();
            WriteBFSBai3(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void ReadAdjListBFSBai3(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());


            adjList = new LinkedList<int>[N];

            for (int i = 0; i < N; i++)
            {
                adjList[i] = new LinkedList<int>();
                if (lines[i + 1].Length == 0)
                    continue;
                line = lines[i + 1].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int v = int.Parse(line[j].Trim());
                    adjList[i].AddLast(v);
                }

            }
        }

        public void BFS3()
        {
            visited = new bool[N + 1];
            pre = new int[N + 1];
            BFSBai3(sVertex);
        }
        public void BFSBai3(int s)
        {
            Queue<int> kq = new Queue<int>();
            visited[s] = true;
            pre[s] = -1;
            kq.Enqueue(s);

            while (kq.Count != 0)
            {
                int u = kq.Dequeue();//Đỉnh xét
                foreach (int dinh in adjList[u])
                {
                    if (visited[dinh])
                        continue;
                    visited[dinh] = true;
                    pre[dinh] = u;
                    kq.Enqueue(dinh);
                }
            }

        }
        protected void WriteBFSBai3(string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                for (int i = 1; i < pre.Length; i++)
                {
                    if (pre[i] == 0)
                    {
                        file.WriteLine("NO");
                        break;
                    }
                    else
                    {
                        Console.Write("\nYES");
                        break;
                    }

                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sVertex"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        private List<int> BFS3(int sVertex, int z)
        {
            //Khởi tạo

            Queue<int> q = new Queue<int>();
            BFS();
            visited[z] = true;
            pre[z] = -1;

            q.Enqueue(z);//Đưa vào hàng đợi

            while (q.Count != 0)
            {
                int u = q.Dequeue();//Lấy ra
                foreach (int v in adjList[u - 1])
                {
                    if (visited[v])//=true
                        continue;

                    visited[v] = true;
                    pre[v] = u;
                    q.Enqueue(v);


                }
            }
            return BFS3(sVertex, sVertex);
        }

        //Bai04

        public void ConnectedComponents(string fname)
        {
            ReadAdjListBFSBai4(fname);
            BFS4();
            WriteBFSBai4(fname.Substring(0, fname.Length - 3) + "OUT");
        }
        private void ReadAdjListBFSBai4(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());


            adjList = new LinkedList<int>[N];

            for (int i = 0; i < N; i++)
            {
                adjList[i] = new LinkedList<int>();
                if (lines[i + 1].Length == 0)
                    continue;
                line = lines[i + 1].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int v = int.Parse(line[j].Trim());
                    adjList[i].AddLast(v);
                }

            }
        }

        public void BFS4()
        {
            visited = new bool[N + 1];
            pre = new int[N + 1];

            BFSBai4();
        }
        int soMienLienThong = 0;
        public void BFSBai4()
        {
            Queue<int> kq = new Queue<int>();


            for (int i = 1; i <= N; i++)
            {
                if (pre[i] == 0)
                {
                    soMienLienThong++;
                    visited[i] = true;
                    pre[i] = -1;
                    kq.Enqueue(i);
                    while (kq.Count != 0)
                    {
                        int u = kq.Dequeue();// đỉnh xét
                        foreach (int dinh in adjList[u - 1])
                        {
                            if (visited[dinh])
                                continue;
                            visited[dinh] = true;
                            pre[dinh] = u;
                            kq.Enqueue(dinh);
                        }
                    }
                }

            }


        }
        protected void WriteBFSBai4(string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.Write(soMienLienThong);
            }
        }


        // Bt 1 lam them


        int[,] arrGraph;
        List<List<int>> adjL;
        public int maxWeight { get; private set; }

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


