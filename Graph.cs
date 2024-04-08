using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab10_2
{
    internal class Graph
    {
        public int N { get; set; }
        public int X { get; set; }
        public int E { get; set; }
        bool[] visited;
        int[] pre;
        LinkedList<int>[] adjList;
        public int[,] adjMatrix;
        LinkedList<Tuple<int, int>> eList;

        //Bai01
        internal void CheckEuler01(string fname)
        {
            ReadAdjMatrix(fname);
            int k = CheckEuler01();
            WriteCheckEuler(k, fname.Substring(0, fname.Length - 3) + "OUT");
        }
        private void ReadAdjMatrix(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            N = Int32.Parse(lines[0].Trim());
            Console.WriteLine("\t Number of vertices: " + N);
            adjMatrix = new int[N, N];
            for (int i = 0; i < N && i + 1 < lines.Length; i++)
            {
                string[] line = lines[i + 1].Split(' ');
                for (int j = 0; j < N && j < line.Length; j++)
                {
                    if (!int.TryParse(line[j].Trim(), out int value))
                    {
                        continue;
                    }
                    adjMatrix[i, j] = value;
                }
            }
        }
        private int CheckEuler01()
        {
            ConvertAM2AL();
            return EulerGraph();
        }
        private void ConvertAM2AL()
        {
            adjList = new LinkedList<int>[N];
            for (int i = 0; i < N; i++)
            {
                adjList[i] = new LinkedList<int>();
            }
            for (int i = 0; i < N; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    if (adjMatrix[i, j] == 1)
                    {
                        adjList[i].AddLast(j + 1);
                        adjList[j].AddLast(i + 1);
                    }
                }
            }
        }

        private int EulerGraph()
        {
            if (!IsConnectedGraph())
                return 0;
            int numOddvertex = 0;
            int numEvenvertex = 0;
            for (int i = 0; i < N; i++)
            {
                int deg = adjList[i].Count;
                numEvenvertex += 1 - deg % 2;
                numOddvertex += deg % 2;
            }
            if (numOddvertex == 2)
                return 2;
            if (numEvenvertex == N)
                return 1;
            return 0;
        }
        private bool IsConnectedGraph()
        {
            int s = 1;
            visited = new bool[N + 1];
            pre = new int[N + 1];
            DFS(s);
            for (int i = 2; i < pre.Length; i++)
            {
                if (pre[i] < 1)
                    return false;
            }
            return true;
        }

        private void DFS(int s)
        {
            if (s < 0 || s >= visited.Length)
            {
                return;
            }
            Stack<int> stack = new Stack<int>();
            visited[s] = true;
            pre[s] = -1;
            stack.Push(s);
            while (stack.Count != 0)
            {
                int u = stack.Pop();
                foreach (int v in adjList[u - 1])
                {
                    if (!visited[v])
                    {
                        visited[v] = true;
                        pre[v] = u;
                        stack.Push(u);
                        stack.Push(v);
                    }
                }
            }
        }

        private void WriteCheckEuler(int k, string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.WriteLine(String.Format($"{k}"));
            }
        }

        //Bai02
        internal void CheckEuler02(string fname)
        {
            ReadAdjMatrix(fname);
            int k = CheckEuler02();
            WriteCheckEuler(k, fname.Substring(0, fname.Length - 3) + "OUT");
        }
        private int CheckEuler02()
        {
            ConvertAM2AL();
            if (!IsConnectedGraph())
                return 0;
            int numOddvertex = 0;
            int numEvenvertex = 0;
            int[] deg = CalcDegree02();
            for (int i = 1; i < deg.Length; i++)
            {
                numEvenvertex += 1 - deg[i] % 2;
                numOddvertex += deg[i] % 2;
            }
            if (numOddvertex == 2)
                return 2;
            if (numEvenvertex == N)
                return 1;
            return 0;
        }
        private int[] CalcDegree02()
        {
            int[] deg = new int[N + 1];
            for (int i = 0; i < N; i++)
            {
                deg[i + 1] += adjList[i].Count;
                foreach (int v in adjList[i])
                {
                    deg[v]++;
                }
            }
            return deg;
        }

        //Bai03
        internal void EulerianCycle(string fname)
        {
            ReadAdjMatrix03(fname);
            List<int> cycle = EulerianCycle();
            WriteEulerianCycle(cycle, fname.Substring(0, fname.Length - 3) + "OUT");
        }
        private void ReadAdjMatrix03(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            X = Int32.Parse(line[1].Trim());
            Console.WriteLine("\t Number of vertices: " + N);
            adjMatrix = new int[N, N];
            for (int i = 0; i < N; i++)
            {
                line = lines[i + 1].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int value = Int32.Parse(line[j].Trim());
                    adjMatrix[i, j] = value;
                }
            }
        }
        private List<int> EulerianCycle()
        {
            if (CheckEuler01() != 1)
            {
                return null;
            }
            Stack<int> stack = new Stack<int>();
            List<int> cycle = new List<int>();
            int current = X - 1;
            while (true)
            {
                while (adjList[current].Count > 0)
                {
                    stack.Push(current);
                    int pre = current;
                    current = adjList[current].Last.Value - 1;
                    adjList[pre].RemoveLast();
                    adjList[current].Remove(adjList[current].Find(pre + 1));
                }
                if (stack.Count == 0)
                {
                    break;
                }
                current = stack.Pop();
                cycle.Add(current + 1);
            }
            cycle.Reverse();
            cycle.Add(X);
            return cycle;
        }
        private void WriteEulerianCycle(List<int> cycle, string fname)
        {
            using (StreamWriter file = new StreamWriter(fname))
            {
                foreach (int vertex in cycle)
                {
                    file.Write("{0, -3}", vertex);
                }
            }
        }

        //Bai04
        internal void DrawAllEdges(string fname)
        {
            ReadAdjMatrix(fname);
            List<List<int>> kList = DrawAllEdges();
            WriteDrawAllEdges(kList, fname.Substring(0, fname.Length - 3) + "OUT");
        }
        private List<List<int>> DrawAllEdges()
        {
            ConvertAM2AL();
            List<int> spanningTree = FindSpanningTree();
            int eulerianEdgesCount = GetEulerianEdgesCount();
            List<List<int>> list = new List<List<int>>();
            if (eulerianEdgesCount == 0)
            {
                list.Add(spanningTree);
                return list;
            }
            List<int> eulerianCycle = EulerianCycle();
            list.Add(eulerianCycle);
            for (int i = 1; i < adjList.Length; i++)
            {
                if (adjList[i].Count % 2 == 1)
                {
                    List<int> l = new List<int>();
                    l.Add(i);
                    l.Add(adjList[i].First.Value);
                    list.Add(l);
                }
            }
            return list;
        }
        private List<int> FindSpanningTree()
        {
            List<int> parent = new List<int>();
            int[] key = new int[N];
            bool[] mstSet = new bool[N];
            for (int i = 0; i < N; i++)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }

            key[0] = 0;

            for (int count = 0; count < N - 1; count++)
            {
                int u = MinKey(key, mstSet);
                mstSet[u] = true;

                for (int v = 0; v < N; v++)
                {
                    if (adjMatrix[u, v] != 0 && !mstSet[v] && adjMatrix[u, v] < key[v])
                    {
                        parent.Add(u);
                        key[v] = adjMatrix[u, v];
                    }
                }
            }

            return parent;
        }
        private int MinKey(int[] key, bool[] mstSet)
        {
            int min = int.MaxValue;
            int minIndex = -1;

            for (int v = 0; v < N; v++)
            {
                if (!mstSet[v] && key[v] < min)
                {
                    min = key[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }
        private int GetEulerianEdgesCount()
        {
            ConvertAM2AL();
            int totalDegree = 0;
            int oddDegreeVertices = 0;

            foreach (var list in adjList)
            {
                totalDegree += list.Count;
                if (list.Count % 2 != 0)
                {
                    oddDegreeVertices++;
                }
            }

            return totalDegree / 2;
        }

        private void WriteDrawAllEdges(List<List<int>> kList, string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.WriteLine(kList.Count);
                foreach (List<int> list in kList)
                {
                    if (list == null)
                    {
                        continue;
                    }
                    foreach (int v in list)
                    {
                        file.Write($"{v,-3}");
                    }
                    file.WriteLine();
                }
            }
        }

    }
}
