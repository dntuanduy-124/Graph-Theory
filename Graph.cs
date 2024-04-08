using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;
namespace Lab04_22DH110521
{
    internal class Graph
    {
        //Properties:so dinh
        public int N { get; set; }

        //Properties:so canh
        public int E { get; set; }


        //Attributes:danh sach ke
        LinkedList<int>[] adjList;


        //Attributes:danh sach canh
        private LinkedList<Tuple<int, int>> eList;
        ////Attributes:ma tran ke
        int[,] arrGraph;
        //Bai01
        //Properties:đỉnh bắt đầu
        public int S { get; set; }
        //Properties:đỉnh kết thúc
        public int T { get; set; }

        public int list { get; set; }
        public int sVertex { get; set; }





        //Attributes:Mang kt duoc vieng tham hay chua
        bool[] visited;
        //Attributes:danh lien truoc cua dinh ()dinh cha
        int[] pre;


        public void ConnectedComponents(string fname)
        {
            ReadAdjListBFSBai1(fname);
            BFS1();
            WriteBFSBai1(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void BFS1()
        {
            visited = new bool[N + 1];
            pre = new int[N + 1];
            BFSBai1();
        }

        int soMienLienThong = 0;
        //Danh sách lưu liên thông(lkDinh)
        List<List<int>> dinhNoi = new List<List<int>>();

        public void BFSBai1()
        {
            Queue<int> q = new Queue<int>();
            int[] duongDi = new int[N];
            int start = 1;

            for (int i = 1; i <= N; i++)
            {
                //liệt kê các phần tử miền liên thông(lietke)
                List<int> lietke = new List<int>();
                if (pre[i] == 0)
                {
                    soMienLienThong++;
                    visited[i] = true;
                    pre[i] = -1;
                    q.Enqueue(i);
                    while (q.Count != 0)
                    {
                        //Đỉnh đang xét
                        int u = q.Dequeue();
                        lietke.Add(u);
                        foreach (int dinh in adjList[u - 1])
                        {
                            if (visited[dinh])
                                continue;

                            visited[dinh] = true;
                            pre[dinh] = u;
                            q.Enqueue(dinh);
                        }
                    }
                }
                if (lietke.Count != 0)
                {
                    dinhNoi.Add(lietke); // Thêm danh sách của miền liên thông vào danh sách chung
                }
            }
        }


        private void WriteBFSBai1(string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {

                file.WriteLine(soMienLienThong);
                for (int i = 0; i < dinhNoi.Count; i++)
                {
                    for (int j = 0; j < dinhNoi[i].Count; j++)
                    {
                        file.Write(dinhNoi[i][j] + " ");
                        continue;
                    }
                    file.WriteLine();
                }
            }

        }


        private void ReadAdjListBFSBai1(string fname)
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
        //Bai02
        //Bỏ cạnh thứ nhất
        int removed1;
        //Bỏ cạnh thứ hai
        int removed2;
        public void BridgeBFS(string fname)
        {
            ReadAdjListBFSBai2(fname);
            bool flag = IsBridgeBFS();
            WriteBridgeBFS(fname.Substring(0, fname.Length - 3) + "OUT", flag); ;
        }
        private void ReadAdjListBFSBai2(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            removed1 = int.Parse(line[1].Trim()) - 1;
            removed2 = int.Parse(line[2].Trim()) - 1;

            Console.WriteLine("Number of vertices: " + N);
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

        private bool IsBridgeBFS()
        {
            bool res = false;
            //Dem so mien lien thong truoc khi xoa canh
            int prev = ContComponentBFS2().Count;
            //Xoa canh 
            adjList[removed1].Remove(removed2);
            adjList[removed2].Remove(removed1);
            //Dem so mien lien thong sau khi xoa
            int cur = ContComponentBFS2().Count;

            //Phuc hoi lai canh da xoa
            adjList[removed1].AddLast(removed2);
            adjList[removed2].AddLast(removed1);
            if (prev < cur)
            {
                res = true;
            }
            return res;
        }

        private List<List<int>> ContComponentBFS2()
        {
            List<List<int>> list = new List<List<int>>();
            InitArray();
            for (int i = 0; i < N; i++)
            {
                if (color[i] == 0)
                {
                    List<int> res = BFSBridge(i);
                }
            }
            return list;
        }

        private List<int> BFSBridge(int i)
        {
            List<int> res = new List<int>();
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(i);
            color[i] = 0;
            while (queue.Count > 0)
            {
                int v = queue.Dequeue();
                foreach (var kq in adjList[i])
                {
                    continue;
                    if (color[kq] == 0)


                    {
                        queue.Enqueue(kq);
                        res.Add(kq);
                        color[kq] = 1;
                    }
                }
            }
            return res;
        }




        // 
        int[] color;
        //
        int[] back;
        private void InitArray()
        {
            //
            int v = -2;
            color = new int[N];
            back = new int[N];
            for (int i = 0; i < color.Length; i++)
            {
                color[i] = 0;
                back[i] = v;
            }
        }

        private void WriteBridgeBFS(string fname, bool flag)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                string res = "No";
                if (flag)
                    res = "Yes";
                file.WriteLine(res);
                Console.WriteLine(res);

                //if (flag == true)
                //{
                //    Console.WriteLine(String.Format("{0,-3}", flag.ToString()));

                //}
                //else
                //    Console.WriteLine(String.Format("{0,-3}", flag.ToString()));


            }
        }

        //Bai03
        //
        List<List<int>> adjListK;
        internal void CutVertexBFS(string fname)
        {
            ReadAdjListBFS3(fname);
            bool flag = IsCutVertexBFS();
            WriteCutVertexBFS(fname, flag);
        }


        private void WriteCutVertexBFS(string fname, bool flag)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname.Substring(0, fname.Length - 3)))
            {
                file.WriteLine(String.Format("{0,-3}", flag.ToString()));
                System.Console.WriteLine(String.Format("{0,-3}", flag.ToString()));
            }
        }

        private bool IsCutVertexBFS()
        {
            bool res = false;
            int prev = CountCnctComponentsBFS().Count;
            int v = sVertex;
            List<int> list = RemoveVertex(v);
            int currt = CountCnctComponentsBFS().Count;
            AddVertex(v, list);
            if (prev < currt)
                res = true;
            return res;
        }

        private List<int> AddVertex(int v, List<int> list)
        {
            foreach (int vv in list)
            {
                adjListK[vv].Add(v);
            }
            adjListK.Insert(v, list);
            return list;
        }

        private List<int> RemoveVertex(int v)
        {
            List<int> list = new List<int>();
            foreach (int vv in adjList[v])
            {
                list.Add(vv);
                adjList[vv].Remove(v);
            }
            adjListK.RemoveAt(v);   
            return list;
        }

        private void ReadAdjListBFS3(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            sVertex = Int32.Parse(line[1].Trim());
            Console.WriteLine("Number of verties: " + N);
            adjListK = new List<List<int>>();
            for (int i = 0; i < N; i++)
            {
                line = lines[i + 1].Trim().Split(' ');
                List<int> list = new List<int>();
                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j].Trim().Length > 0)
                        list.Add(Int32.Parse(line[j].Trim()) - 1);
                }
                adjListK.Add(list);
            }
            Console.WriteLine("Read compelete(eof)!");
        }


        int nRow;
        int nCol;

        //bai04
        private void InitIntArray(int value = -2)
        {
            color = new int[N];
            back = new int[N];
            for (int i = 0; i < color.Length; i++)
            {
                color[i] = 0;
                back[i] = value;
            }
        }
        private List<List<int>> CountCnctComponentsBFS()
        {
            List<List<int>> list = new List<List<int>>();
            InitIntArray(-2);
            for (int i = 0; i < N; i++)
            {
                if (color[i] == 0)
                {
                    List<int> res = BFSBridge(i);
                    list.Add(res);
                }
            }
            return list;
        }
        //internal void GridPathBFS(string fname)
        //{
        //    ReadGridBFS(fname);
        //    List<string> list = GridBFS(sNode);
        //    WriteGridPathBFS(fname, list);
        //}

        //private void WriteGridPathBFS(string fname, List<string> list)
        //{
        //    using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname.Substring(0, fname.Length - 3)))
        //    {
        //        file.WriteLine(String.Format("{0,-3}", list.Count));
        //        foreach (string v in list)
        //            System.Console.WriteLine(String.Format("{0,-3}", v));
        //    }
        //}
        //int[] sNode;
        //int[] eNode;
        //private bool[] status;
        //private void SetNode(int row, int col, int value)
        //{
        //    arrGraph[row, col] = value;
        //}
        //private void InitBoolArray(bool value)
        //{
        //    status = new bool[nRow * nCol];
        //    for (int i = 0; i < nRow * nCol; i++)
        //    {
        //        status[i] = value;
        //    }
        //}
        //private bool InBoard(int x, int y)
        //{
        //    return x >= 0 && x < nRow && y >= 0 && y < nCol;
        //}

        //private void GridBFS(Tuple<int, int> s, ref List<string> list)
        //{
        //    Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
        //    int node = nRow * s.Item1 + s.Item2;
        //    queue.Enqueue(s);
        //    InitBoolArray(false);
        //    status[node] = true;
        //    list.Add(String.Format("{0,-3}{1,-3}", s.Item1, s.Item2));
        //    int[] aA = { -1, 1 };
        //    while (queue.Count > 0)
        //    {
        //        Tuple<int, int> v = queue.Dequeue();
        //        if ((v.Item1 == eNode.Item1) && (v.Item2 == eNode.Item2))
        //            break;
        //        foreach (int i in aA)
        //            foreach (int j in aA)
        //            {
        //                int x = v.Item1 + i;
        //                int y = v.Item2 + j;
        //                node = nRow * x + y;
        //                if (!InBoard(x, y))
        //                    continue;
        //                if (arrGraph[x, y] == 0)
        //                    continue;
        //                if (status[node])
        //                    continue;
        //                queue.Enqueue(new Tuple<int, int>(x, y));
        //                status[node] = true;
        //                list.Add(String.Format("{0,-3}{1,-3}", x, y));
        //            }
        //    }
        //}
        //int eVertex;
        //private void ReadGridBFS(string fname)
        //{
        //    string[] lines = System.IO.File.ReadAllLines(fname);
        //    string[] line = lines[0].Split(' ');
        //    nRow = Int32.Parse(line[0].Trim());
        //    nCol = Int32.Parse(line[1].Trim());
        //    line = lines[1].Split(' ');
        //    sVertex = nRow * (Int32.Parse(line[0].Trim()) - 1) + Int32.Parse(line[1].Trim()) - 1;
        //    eVertex = nRow * (Int32.Parse(line[2].Trim()) - 1) + Int32.Parse(line[3].Trim()) - 1;
        //    adjListK= new List<List<int>>();
        //    for (int i = 2; i < lines.Length; i++)
        //    {
        //        line = lines[i].Split(' ');
        //        for (int j = 0; j < line.Length; j++)
        //        {
        //            SetNode(i - 1, j, Int32.Parse(line[j].Trim()));
        //        }
        //    }
        //}

    }
}