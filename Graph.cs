using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _22DH110521_Lab09
{
    internal class Graph
    {
        //Properties:số đỉnh
        public int N { get; set; }
        //Properties:số cạnh
        public int E { get; set; }

        //Danh sách cạnh
        LinkedList<Tuple<int, int, int>> listWEdges;
        //Cây khung
        //LinkedList<Tuple<int, int, int>> spanningTree;
        //Danh sách kề
        List<List<int>> adjList;
        //
        List<Tuple<int, int, int>> MST;

        //Attributes:Mang kt duoc vieng tham hay chua
        bool[] visited;
        //Attributes:danh lien truoc cua dinh ()dinh cha
        int[] pre;
        //
        public const int Inf = 1000;


        List<List<Tuple<int, int>>> adjWList;


        int[] color;
        int[] back;


        double[] distance;

        //public void SpanningTree(string fname)
        //{
        //    //Doc ds ke tu tap tin fname
        //    ReadEdgeList(fname);

        //    //Duyet do thi de tim cac dinh lien thong voi dinh s
        //    SpanningTree();

        //    //Viet dinh lien thong vao tap tin
        //    WriteSpanningTree(fname.Substring(0, fname.Length - 3) + "OUT");
        //}

        //private void ReadEdgeList(string fname)
        //{
        //    string[] lines = System.IO.File.ReadAllLines(fname);
        //    string[] line = lines[0].Split(' ');
        //    N = Int32.Parse(line[0].Trim()); //7 
        //    E = Int32.Parse(line[1].Trim());

        //    Console.WriteLine("Number of vertices: " + N);
        //    wEList = new LinkedList<Tuple<int, int, int>>();//5

        //    for (int i = 0; i < E; i++)
        //    {
        //        line = lines[i + 1].Split(' ');
        //        wEList.AddLast(new Tuple<int, int, int>(
        //            Int32.Parse(line[0].Trim()), Int32.Parse(line[1].Trim()), Int32.Parse(line[2].Trim())));
        //    }
        //}

        //private void SpanningTree()
        //{
        //    //Khoi tao visited va pre 

        //    visited = new bool[N + 1];
        //    pre = new int[N + 1];
        //    spanningTree = new LinkedList<Tuple<int, int, int>>();
        //    //Chuyển ds cạnh thành ds kề
        //    ConvertEL2AL();
        //    SpanningTreeDFS(1);
        //}

        //private void ConvertEL2AL()
        //{

        //    //Khoi tao danh sach ke
        //    adjList = new LinkedList<Tuple<int, int>>[N];

        //    for (int i = 0; i < N; i++)
        //    {
        //        adjList[i] = new LinkedList<Tuple<int, int>>();
        //    }
        //    foreach (Tuple<int, int, int> e in wEList)
        //    {
        //        adjList[e.Item1 - 1].AddLast(
        //            new Tuple<int, int>(e.Item2, e.Item3));
        //        adjList[e.Item2 - 1].AddLast(
        //            new Tuple<int, int>(e.Item1, e.Item3));
        //    }
        //}

        //private void SpanningTreeDFS(int s)
        //{
        //    Stack<int> stack = new Stack<int>();

        //    visited[s] = true;

        //    pre[s] = -1;

        //    stack.Push(s);//Đưa vào hàng đợi


        //    while (stack.Count != 0)
        //    {
        //        int u = stack.Pop();//Lấy ra

        //        foreach (Tuple<int, int> v in adjList[u - 1])
        //        {
        //            if (visited[v.Item1])//=true
        //                continue;

        //            visited[v.Item1] = true;
        //            pre[v.Item1] = u;
        //            stack.Push(u);
        //            stack.Push(v.Item1);
        //            //spanningTree.AddLast(
        //            //    new Tuple<int,int,int>(u,v.Item1,v.Item2));
        //            spanningTree.AddLast(
        //                new Tuple<int, int, int>(u, v.Item1, v.Item2));
        //            break;
        //        }
        //    }
        //}

        //private void PrintSpanningTree()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    foreach (var tuple in spanningTree)
        //    {
        //        sb.AppendLine($"({tuple.Item1}, {tuple.Item2}, {tuple.Item3})");
        //    }
        //    Console.WriteLine(sb.ToString());
        //}


        //private void WriteSpanningTree(string v)
        //{

        //}

        //public void Prim_MST(string fname)
        //{

        //    ReadPrimMST(fname); // Task 3.1
        //    Prim(0); // Task 3.2
        //    WriteList(fname.Substring(0, fname.Length - 3) + "OUT"); // Task 3.3
        //}

        //// Task 1.2.1
        //private void InitIntArray(int value = -2, int vDistance = Inf)
        //{
        //    color = new int[N + 1];
        //    back = new int[N + 1];
        //    for (int i = 1; i < color.Length; i++)
        //    {
        //        color[i] = 0;
        //        back[i] = value;
        //    }
        //    distance = new double[N + 2];
        //    for (int i = 1; i < distance.Length; i++)
        //        distance[i] = vDistance;
        //    distance[N]++;

        //}



        //private void ReadPrimMST(string fname)
        //{
        //    string[] lines = System.IO.File.ReadAllLines(fname);
        //    string[] line = lines[0].Split(' ');
        //    N = Int32.Parse(line[0].Trim());
        //    E = Int32.Parse(line[1].Trim());

        //    //Graph: Tuple<Vertex, Weight> 1: <2,3>
        //    adjWList = new List<List<Tuple<int, int, int>>>();
        //    for (int i = 0; i < N; i++)
        //        adjWList.Add(new List<Tuple<int, int, int>>());

        //    Console.WriteLine(adjWList.Count);
        //    for (int i = 0; i < E; i++)
        //    {
        //        line = lines[i + 1].Split(' ');
        //        Console.WriteLine(lines[i + 1]);
        //        int v1 = Int32.Parse(line[0].Trim()) - 1;
        //        int v2 = Int32.Parse(line[1].Trim()) - 1;
        //        int w = Int32.Parse(line[2].Trim());

        //        adjWList[v1].Add(new Tuple<int, int, int>(v2,v1, w));
        //        adjWList[v2].Add(new Tuple<int, int, int>(v1,v2, w));
        //    }

        //}

        //private void Prim(int v)
        //{
        //    InitIntArray(); // Task 1.2.1
        //    back[v] = -1;
        //    distance[v] = 0;
        //    distance[N + 1] = 0;

        //    for (int i = 0; i < N; i++)
        //    {
        //        int u = MinKey(); // Task 3.2.1
        //        color[u] = 1;
        //        distance[N + 1] += distance[u];

        //        foreach (Tuple<int, int, int> vv in adjWList[u])
        //        {

        //            int vx = vv.Item1;
        //            int w = vv.Item2;
        //            if (color[vx] == 0)
        //                if (w < distance[vx])
        //                {
        //                    back[vx] = u;
        //                    distance[vx] = w;
        //                }

        //        }
        //    }
        //}

        //private int MinKey()
        //{
        //    int index = N;
        //    for (int i = 0; i < N; i++)
        //        if (color[i] == 0)
        //            if (distance[index] > distance[i])
        //                index = i;
        //    return index;
        //}

        //private void WriteList(string fname)
        //{
        //    {

        //        using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
        //        {
        //            file.WriteLine("{0} {1}", N - 1, distance[N + 1]);//n-1
        //            for (int i = 0; i < N; i++)
        //                if (back[i] != -1)
        //                    file.WriteLine(String.Format("{0,-3} {1, -3}",back[i] + 1, i + 1));
        //        }

        //    }
        //}
        //Bai01
        internal void SpanningTree(string fname)
        {
            ReadE2AdjList(fname);
            List<int> list = SpanningTree(1);
            WriteStringList(fname.Substring(0, fname.Length - 3) + "OUT", list);
        }

        private void ReadE2AdjList(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            E = Int32.Parse(line[1].Trim());
            Console.WriteLine("\t Number of vertices: " + N);
            adjList = new List<List<int>>();
            for (int i = 0; i < N; i++)
            {
                adjList.Add(new List<int>());
            }
            for (int i = 0; i < E; i++)
            {
                line = lines[i + 1].Split(' ');
                int v1 = Int32.Parse(line[0].Trim());
                int v2 = Int32.Parse(line[1].Trim());
                adjList[v1 - 1].Add(v2);
                adjList[v2 - 1].Add(v1);
            }
        }
        private List<int> SpanningTree(int s)
        {
            List<int> list = new List<int>();
            Stack<int> stack = new Stack<int>();
            InitIntArray();
            stack.Push(s);
            color[s] = 1;
            while (stack.Count > 0)
            {
                int v = stack.Pop();
                list.Add(v);
                if (list.Count == N)
                    break;
                adjList[v - 1].Reverse();
                foreach (int vv in adjList[v - 1])
                {
                    if (color[vv] == 0)
                    {
                        stack.Push(vv);
                        color[vv] = 1;
                    }
                }
                adjList[v - 1].Reverse();
            }
            return list;
        }

        private void InitIntArray(int value = -2, int vDistance = Inf)
        {
            color = new int[N + 1];
            back = new int[N + 1];
            for (int i = 1; i < color.Length; i++)
            {
                color[i] = 0;
                back[i] = value;
            }
            distance = new double[N + 2];
            for (int i = 1; i < distance.Length; i++)
            {
                distance[i] = vDistance;
            }
            distance[N]++;
        }

        private void WriteStringList(string fname, List<int> list)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                int count = list.Count - 1;
                file.WriteLine(count);
                for (int i = 0; i < count; i++)
                {
                    file.WriteLine(String.Format($"{list[i]} {list[i + 1]}"));
                }
            }
        }

        //Bai02

        internal void Kruskal(string fname)
        {
            ReadEdges(fname);
            Kruskal();
            WriteEdges(fname.Substring(0, fname.Length - 3) + "OUT");
        }
        private void ReadEdges(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            E = Int32.Parse(line[1].Trim());
            Console.WriteLine("\t Number of vertices: " + N);
            listWEdges = new LinkedList<Tuple<int, int, int>>();
            for (int i = 0; i < E; i++)
            {
                line = lines[i + 1].Split(' ');
                listWEdges.AddLast(new Tuple<int, int, int>(
                    Int32.Parse(line[0].Trim()),
                    Int32.Parse(line[1].Trim()),
                    Int32.Parse(line[2].Trim())
                    ));
            }
        }
        private void Kruskal()
        {
            var sortedEdges = listWEdges.OrderBy(edge => edge.Item3).ToList();
            MST = new List<Tuple<int, int, int>>();
            int m = 0;
            UnionFind uf = new UnionFind(N);
            foreach (var edge in sortedEdges)
            {
                int u = edge.Item1;
                int v = edge.Item2;
                int weight = edge.Item3;
                if (!uf.Connected(u, v))
                {
                    MST.Add(new Tuple<int, int, int>(u, v, weight));
                    m++;
                    uf.Union(u, v);
                }
                if (m == N - 1)
                    break;
            }
        }
        public class UnionFind
        {
            private int[] parent;
            private int[] rank;

            public UnionFind(int n)
            {
                parent = new int[n];
                rank = new int[n];
                for (int i = 0; i < n; i++)
                {
                    parent[i] = i;
                    rank[i] = 0;
                }
            }

            public int Find(int x)
            {
                if (x < 0 || x >= parent.Length)
                    return -1;
                if (parent[x] != x)
                    parent[x] = Find(parent[x]);

                return parent[x];
            }

            public bool Connected(int x, int y)
            {
                return Find(x) == Find(y);
            }

            public void Union(int x, int y)
            {
                int rootX = Find(x);
                int rootY = Find(y);
                if (rootX == -1 || rootY == -1 || rootX == rootY) return;

                if (rank[rootX] < rank[rootY])
                    parent[rootX] = rootY;
                else if (rank[rootX] > rank[rootY])
                    parent[rootY] = rootX;
                else
                {
                    parent[rootY] = rootX;
                    rank[rootX]++;
                }
            }
        }
        private void WriteEdges(string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                int totalWeight = 0;
                foreach (var edge in MST)
                {
                    totalWeight += edge.Item3;
                }
                file.WriteLine($"{MST.Count} {totalWeight}");
                foreach (var edge in MST)
                {
                    file.WriteLine($"{edge.Item1} {edge.Item2} {edge.Item3}");
                }
            }
        }
        //Bai03
        internal void Prim_MST(string fname)
        {
            ReadPrimMST(fname);
            Prim(0);
            WriteStringList(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void ReadPrimMST(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            E = Int32.Parse(line[1].Trim());
            Console.WriteLine("\t Number of vertices: " + N);
            adjWList = new List<List<Tuple<int, int>>>();
            for (int i = 0; i < N; i++)
            {
                adjWList.Add(new List<Tuple<int, int>>());
            }
            for (int i = 0; i < E; i++)
            {
                line = lines[i + 1].Split(' ');
                int v1 = Int32.Parse(line[0].Trim()) - 1;
                int v2 = Int32.Parse(line[1].Trim()) - 1;
                int w = Int32.Parse(line[2].Trim());
                adjWList[v1].Add(new Tuple<int, int>(v2, w));
                adjWList[v2].Add(new Tuple<int, int>(v1, w));
            }
        }

        private void Prim(int v)
        {
            InitIntArray();
            back[v] = -1;
            distance[v] = 0;
            distance[N + 1] = 0;
            for (int i = 0; i < N; i++)
            {
                int u = MinKey();
                color[u] = 1;
                distance[N + 1] += distance[u];
                foreach (Tuple<int, int> vv in adjWList[u])
                {
                    int vx = vv.Item1;
                    int w = vv.Item2;
                    if (color[vx] == 0)
                        if (w < distance[vx])
                        {
                            back[vx] = u;
                            distance[vx] = w;
                        }
                }
            }
        }

        private int MinKey()
        {
            int index = N;
            for (int i = 0; i < N; i++)
            {
                if (color[i] == 0)
                    if (distance[index] > distance[i])
                        index = i;
            }
            return index;
        }

        private void WriteStringList(string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.WriteLine($" {N - 1} {distance[N + 1]} ");
                for (int i = 0; i < N; i++)
                {
                    if (back[i] != -1)
                        file.WriteLine(String.Format($" {back[i] + 1} {i + 1}  {distance[i]}"));
                }
            }
        }
    




    //Bai4,5
    public int wX;
        public int Source { get; set; }
        public int Destination { get; set; }
        public int Weight { get; set; }
        public int[,] arrGraph;
        internal class Edge
        {
            public int Source { get; set; }
            public int Destination { get; set; }
            public int Weight { get; set; }

            public Edge(int source, int destination, int weight)
            {
                Source = source;
                Destination = destination;
                Weight = weight;
            }
        }
        internal class SubSet
        {
            public int Parent { get; set; }
            public int Rank { get; set; }

            public SubSet(int parent, int rank)
            {
                Parent = parent;
                Rank = rank;
            }
        }
        public Tuple<List<Edge>, int[]> spanningTree = new Tuple<List<Edge>, int[]>(new List<Edge>(), new int[1]);
        public List<SubSet> subSets;
        public List<Edge> ListWEdges;
        //Bai4
        public void SpanningTreeX(string fname)
        {
            ReadLE2AdjList(fname);
            int edges = SpanningTreeX();
            WriteXTree(fname.Substring(0, fname.Length - 3) + "OUT", edges);
        }
        public void ReadLE2AdjList(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            E = Int32.Parse(line[1].Trim());
            wX = Int32.Parse(line[2].Trim());
            Console.WriteLine("\t Number of vertices:" + N);
            ListWEdges = new List<Edge>();

            for (int i = 0; i < E; i++)
            {
                line = lines[i + 1].Split(' ');
                int v1 = Int32.Parse(line[0].Trim()) - 1;
                int v2 = Int32.Parse(line[1].Trim()) - 1;

                if (v1 > v2)
                {
                    int temp = v1;
                    v1 = v2;
                    v2 = temp;
                }
                int w = Int32.Parse(line[2].Trim());

                ListWEdges.Add(new Edge(v1, v2, w));
            }
            ListWEdges.Sort(new EdgeComparer());
        }



        //Bai5
        public void VillageRoads(string fname)
        {
            ReadVL(fname);
            int dist = VillageRoads();
            WriteXTree(fname.Substring(0, fname.Length - 3) + "out", dist);
        }

        private int VillageRoads()
        {
            InitSubSets();
            int eth = 0;
            int edges = Kruskal(eth);
            if (edges < 0)
                edges *= -1;

            eth = ListWEdges.Count;
            ConvertAM2AL();
            ListWEdges.Sort(eth, ListWEdges.Count - eth, new EdgeComparer());

            edges = Kruskal(eth);
            Console.WriteLine(spanningTree.Item1.Count);
            return edges;
        }

        private int Kruskal(int eth)
        {
            
                int dist = 0;
                int target = N - 1;
                int i = eth; int e = 0;
                while (i < E)
                {
                    Edge nextEdge = ListWEdges[i++];
                    int x = Find(nextEdge.Source);
                    int y = Find(nextEdge.Destination);
                    if (x != y)
                    {
                        spanningTree.Item1.Add(nextEdge);
                        dist += nextEdge.Weight;
                        Union(x, y);
                    }
                    if (e == target)
                    {
                        spanningTree.Item2[0] = dist;
                        return dist;

                    }
                }
                spanningTree.Item2[0] = dist;
                if (spanningTree.Item1.Count == (N - 1))

                    return spanningTree.Item2[0];
                return -1;
            
        }
        public void InitSubSets()
        {
            subSets = new List<SubSet>();
            for (int i = 0; i < N; i++)
                subSets.Add(new SubSet(i, 0));
        }
        public void Union(int x, int y)
        {
            int xroot = Find(x);
            int yroot = Find(y);

            if (subSets[xroot].Rank < subSets[yroot].Rank)
                subSets[xroot].Parent = yroot;
            else if (subSets[xroot].Rank > subSets[yroot].Rank)
                subSets[yroot].Parent = xroot;
            else
            {
                subSets[yroot].Parent = xroot;
                ++subSets[xroot].Rank;
            }
        }
        public int Find(int i)
        {
            if (subSets[i].Parent != i)
            {
                subSets[i].Parent = Find(subSets[i].Parent);
            }
            return subSets[i].Parent;
        }

        public void ConvertAM2AL()
        {
            for (int i = 0; i < N; i++)
                for (int j = i + 1; j < N; j++)
                {
                    if (arrGraph[i, j] > 0)
                    {
                        ListWEdges.Add(new Edge(i, j, arrGraph[i, j]));
                        E++;
                    }
                }
        }

        private void ReadVL(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            N = Int32.Parse(lines[0].Trim());
            Console.WriteLine("\t Number of Vertices:" + N);
            ListWEdges = new List<Edge>();
            string[] line;
            arrGraph = new int[N, N];
            int i = 1;
            for (int k = 0; k < N; k++)
            {
                line = lines[k + 1].Split(' ');
                Console.WriteLine(lines[k + i]);
                int col = 0;
                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j].Trim().Length == 0)
                        continue;
                    arrGraph[k, col++] = Int32.Parse(line[j].Trim());
                }
            }
            i += N;
            E = Int32.Parse(lines[i].Trim());
            for (int k = 0; k < E; k++)
            {
                line = lines[i + k + 1].Split(' ');
                Console.WriteLine(lines[i + k + 1].Split(' '));
                int v1 = Int32.Parse(line[0].Trim()) - 1;
                int v2 = Int32.Parse(line[1].Trim()) - 1;

                if (v1 > v2)
                {
                    int temp = v1;
                    v1 = v2;
                    v2 = temp;
                }
                ListWEdges.Add(new Edge(v1, v2, arrGraph[v1, v2]));
                arrGraph[v1, v2] = 0;
                arrGraph[v2, v1] = 0;
            }
            ListWEdges.Sort();
        }
        public class EdgeComparer : IComparer<Edge>
        {
            public int Compare(Edge edge1, Edge edge2)
            {
                return edge1.Weight.CompareTo(edge2.Weight);
            }
        }
        public int SpanningTreeX()
        {
            int edge = ListWEdges.FindIndex(x => x.Weight == wX);
            InitSubSets();
            int edges = Kruskal(edge);
            if (edges < 0)
                edges = -1;
            return edges;
        }

        private void WriteXTree(string fname, int edges)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                if (edges != -1)
                {
                    file.WriteLine("{0}", edges);
                    //file.WriteLine("{0} {1}", N - 1, edges);
                    //foreach (var edge in spanningTree.Item1)
                    //{
                    //    file.WriteLine($"{edge.Source + 1} {edge.Destination + 1} {edge.Weight}");
                    //}
                }
                else
                {
                    file.WriteLine("-1");
                }
            }
        }
    }

}

