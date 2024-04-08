using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _22DH110521_Lab05_DFS
{
    internal class Graph
    {
        //Properties:số đỉnh
        public int N { get; set; }

        //Properties:đỉnh bắt đầu
        public int S { get; set; }
        //Properties:đỉnh kết thúc
        public int E { get; set; }

        //public int visitedVertices { get; set; }
        List<int> visitedVertices;


        //Attributes:danh sách kề
        LinkedList<int>[] adjList;
        //List<List<int>> adjList;
        //Attributes:Mang kt duoc vieng tham hay chua
        bool[] visited;
        //Attributes:danh lien truoc cua dinh ()dinh cha
        int[] pre;
        ////đỉnh
        //public int v { get; set; }
        internal void ListCntVertices(string fname)
        {
            //Doc ds ke tu tap tin fname
            ReadAdjList(fname);

            //Duyet do thi de tim cac dinh lien thong voi dinh s
            DFSBai1(S);

            //Viet dinh lien thong vao tap tin
            WriteAdjList(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void ReadAdjList(string fname)
        {

        }
       

        private void DFSBai1(int S)
        {
            //Khoi tao visited va pre 

            visited = new bool[N + 1];
            pre = new int[N + 1];
            DFS(S);
        }

        private void DFS(int s)
        {//Khởi tạo
            Stack<int> StThuTuDuyet = new Stack<int>();

            visited[s] = true;

            pre[s] = -1;

            StThuTuDuyet.Push(s);//Đưa vào hàng đợi


            while (StThuTuDuyet.Count != 0)
            {
                int u = StThuTuDuyet.Pop();//Lấy ra

                foreach (int v in adjList[u - 1])
                {
                    if (visited[v])//=true
                        continue;

                    visited[v] = true;
                    pre[v] = u;
                    StThuTuDuyet.Push(v);

                }
            }
        }

        private void WriteAdjList(string fname)
        {

            int count = 0;
            string str = "";
            for (int i = 0; i < pre.Length; i++)
            {
                if (pre[i] > 0)
                {
                    count++;
                    str += String.Format("{0,-3}", i);
                }
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                if (count == 0)
                {
                    file.Write(string.Format("Khong co duong di tu {0} ", S));
                    return;
                }
                file.WriteLine(" => " + count);
                file.Write(string.Format(" => {0} ", str));


            }
        }



        //Bai02
        public void PathDFS(string fname)
        {
            //Đọc danh sách kề từ tập tin
            ReadAdjListPathDFS(fname);

            //Duyệt đồ thị tìm đường đi từ s đến e
            PathDFSBai2();

            //Viết danh sách các đỉnh trong đường đi từ s đến e vào tập tin
            WritePathDFS(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void ReadAdjListPathDFS(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            S = Int32.Parse(line[1].Trim());
            E = Int32.Parse(line[2].Trim());

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
        private void PathDFSBai2()
        {
            DFS();
        }

        private void DFS()
        {
            //Khoi tao visited va pre 

            visited = new bool[N + 1];
            pre = new int[N + 1];
            DFS(S);
        }

        private void WritePathDFS(string fname)
        {
            {
                List<int> path = TracePath(S, E);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
                {

                    if (path.Count == 0)
                    {

                        file.Write(string.Format("không có đường đi từ {0} đến {1}", S, E));
                        return;
                    }
                    file.WriteLine(path.Count);
                    foreach (int v in path)

                        file.Write(String.Format("{0,-3}", v));
                }
            }
        }

        private List<int> TracePath(int s, int t)
        {
            List<int> path = new List<int>();
            //int v = pre[t];

            //if (v > 0)
            //    path.Add(t);

            //{
            //    while (true)
            //    {
            //        if (v == -1)
            //            break;
            //        else
            //        path.Insert(0, v);
            //        v = pre[v];
            //    }
            //}

            for(int i = t; i!= -1; i = pre[i])
            {
                path.Add(i);
            }
            path.Reverse();
            return path;
        }
    }
}
