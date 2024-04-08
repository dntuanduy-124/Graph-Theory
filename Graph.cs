using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lab07ltdt
{
    internal class Graph
    {
        protected const int INF = 1000;
        protected int maxWeight = 0;
        int[,] arrGraph;
        int[,] floydMatrix;
        List<List<Tuple<int, int>>> adjList;
        int numVertices;
        int numEdges;
        int sVertex;
        int eVertex;
        int iVertex;
        int[] color;
        int[] back;
        int[] dist;
        // Bai 01 
        public void ShortestPath(string fname)
        {
            ReadAdjListSP(fname); // Task 1.1
            Dijkstra(); // Task 1.2 
            List<int> list = TracePath(); // Task 1.3
            WriteShortestPath(fname.Substring(0, fname.Length - 3) + "out", list); // Task 1.4 
        }

        // Task 1.1 
        protected void ReadAdjListSP(string fname) // adjacency List 
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            numVertices = Int32.Parse(line[0].Trim()); //7 
            numEdges = Int32.Parse(line[1].Trim());
            sVertex = Int32.Parse(line[2].Trim()) -1;
            eVertex = Int32.Parse(line[3].Trim())-1 ;
            Console.WriteLine("Number of vertices: " + numVertices);
            adjList = new List<List<Tuple<int, int>>>();
            for (int i = 0; i < numVertices; i++)
            {
                adjList.Add(new List<Tuple<int, int>>());
            }
            for (int i = 0; i < numEdges; i++)
            {
                line = lines[i + 1].Split(' ');
                int v1 = Int32.Parse(line[0].Trim()) -1;
                int v2 = Int32.Parse(line[1].Trim()) -1;
                int w = Int32.Parse(line[2].Trim()); // w 3
                adjList[v1].Add(new Tuple<int, int>(v2, w));
                adjList[v2].Add(new Tuple<int, int>(v1, w));
            }
            Console.WriteLine("Read complete(eof)!");
        }
        protected void Dijkstra()
        {
            InitIntArray(-1); // Task 1.2.1
            int g = sVertex;
            dist[g] = 0;
            back[g] = -1;
            do
            {
                g = eVertex;
                // Tìm đỉnh g có dist[g] nhỏ nhất trong các đỉnh chưa xét
                for (int i = 0; i < numVertices; i++)
                {
                    if ((color[i] == 0) && (dist[g] > dist[i]))
                    {
                        g = i;
                    }
                }
                color[g] = 1;
                if ((dist[g] == INF) || g == eVertex)
                {
                    break;
                }
                foreach (Tuple<int, int> vv in adjList[g])
                {
                    if (color[vv.Item1] == 0)
                    {
                        int d = dist[g] + vv.Item2;
                        if (dist[vv.Item1] > d)
                        {
                            dist[vv.Item1] = d;
                            back[vv.Item1] = g;
                        }
                    }
                }
            } while (true);
        }
        protected void InitIntArray(int value = -2)
        {
            color = new int[numVertices];
            back = new int[numVertices];
            dist = new int[numVertices];
            for (int i = 0; i < numVertices; i++)
            {
                color[i] = 0;
                back[i] = value;
                dist[i] = INF;
            }
        }
        // Task 1.3 
        protected List<int> TracePath()
        {
            int y = eVertex;
            int x = sVertex;
            List<int> res = new List<int>();
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
                    res.Insert(0, v);
            }
            return res;
        }

        // Task 1.4 
        protected void WriteShortestPath(string fname, List<int> res)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                if (res.Count == 3) // SV giải thích tại sao thì không có đường đi ngắn nhất
                {
                    file.WriteLine(String.Format("Không có đường đi ngắn nhất từ {0,-3} đến {1,-3}", sVertex + 1, eVertex + 1));
                    System.Console.WriteLine(String.Format("Không có đường đi ngắn nhất từ {0,-3} đến {1,-3}", sVertex + 1, eVertex + 1));
                }
                else
                {
                    file.WriteLine(String.Format("{0,-3}", dist[eVertex]));
                    System.Console.WriteLine(String.Format("{0,-3}", dist[eVertex]));                   
                    foreach (int v in res)
                    {
                        file.Write(string.Format("{0,-3} ", v+1));
                        System.Console.Write(string.Format("{0,-3} ", v+1));
                    }
                    file.WriteLine();
                    System.Console.WriteLine();
                }
            }
        }



        public void InterVertexShortestPath(string fname)
        {
            ReadAdjListIVSP(fname);
            List<int> list = IVShortestPath();
            WriteInterVertexShortestPath(fname.Substring(0, fname.Length - 3) + "out", list);
        }

        protected void ReadAdjListIVSP(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            numVertices = Int32.Parse(line[0].Trim());
            numEdges = Int32.Parse(line[1].Trim());
            sVertex = Int32.Parse(line[2].Trim()) - 1;
            eVertex = Int32.Parse(line[3].Trim()) - 1;
            iVertex = Int32.Parse(line[4].Trim()) - 1;
            Console.WriteLine("Number of vertices: " + numVertices);
            adjList = new List<List<Tuple<int, int>>>();
            for (int i = 0; i < numVertices; i++)
            {
                adjList.Add(new List<Tuple<int, int>>());
            }
            for (int i = 0; i < numEdges; i++)
            {
                line = lines[i + 1].Trim().Split(' ');
                int v1 = Int32.Parse(line[0].Trim()) - 1;
                int v2 = Int32.Parse(line[1].Trim()) - 1;
                int w = Int32.Parse(line[2].Trim());
                adjList[v1].Add(new Tuple<int, int>(v2, w));
                adjList[v2].Add(new Tuple<int, int>(v1, w));
            }
            Console.WriteLine("Read complete(eof)!");
        }

        // Task 2.2
        protected List<int> IVShortestPath()
        {
            int temp = eVertex; // lưu tạm
            eVertex = iVertex; // 3       
            // Tìm đường đi ngắn nhất từ s đến trung gian (x) 1->3
            Dijkstra();
            int distance = dist[eVertex];
            List<int> res = TracePath();
            // Phục hồi lại trạng thái đường đi
            eVertex = temp;
            temp = sVertex; // 1
            sVertex = iVertex;
            for (int i = 0; i < numVertices; i++)
            {
                if (!res.Contains(i + 1))
                {
                    color[i] = 0;
                    back[i] = -2;
                    dist[i] = INF;
                }
            }
            color[sVertex] = 0;
            SubDijkstra();
            List<int> phase2 = TracePath();
            distance += dist[sVertex];
            iVertex = sVertex;
            sVertex = temp;
            res.RemoveAt(res.Count - 1);
            res.AddRange(phase2);
            res.Add(distance);
            return res;
        }  
        // Task 2.2.1
        private void SubDijkstra()
        {         
            int g = sVertex;
            dist[g] = 0;
            do
            {
                g = eVertex;
                // Tìm đỉnh g có dist[g] nhỏ nhất trong các đỉnh chưa xét
                for (int i = 0; i < numVertices; i++)
                {
                    if ((color[i] == 0) && (dist[g] > dist[i]))
                    {
                        g = i;
                    }
                }
                color[g] = 1;
                if ((dist[g] == INF) || g == eVertex)
                {
                    break;
                }
                foreach (Tuple<int, int> vv in adjList[g])
                {
                    if (color[vv.Item1] == 0)
                    {
                        int d = dist[g] + vv.Item2;
                        if (dist[vv.Item1] > d)
                        {
                            dist[vv.Item1] = d;
                            back[vv.Item1] = g;
                        }
                    }
                }
            } while (true);
        }

        protected void WriteInterVertexShortestPath(string fname, List<int> res)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                if (res.Count == 3) 
                {
                    file.WriteLine(String.Format("Không có đường đi từ {0, -3} đến {1,-3}", sVertex + 1, eVertex + 1));
                    System.Console.WriteLine(String.Format("Không có đường đi từ {0,-3} đến {1,-3}", sVertex + 1, eVertex + 1));
                }
                else
                {
                    file.WriteLine(String.Format("Có đường đi từ {0,-3} ", res[res.Count - 1]));
                    Console.WriteLine(String.Format("Có đường đi từ {0,-3}", res[res.Count - 1]));
                    int index = 0;
                    foreach (int v in res)
                    {
                        if (index == (res.Count - 1)) 
                            break;
                        file.Write(string.Format("{0,-3} ", v + 1));
                        System.Console.Write(string.Format("{0,-3} ", v + 1));
                        index++;
                    }
                    file.WriteLine();
                    System.Console.WriteLine();
                }
            }
        }
        //Bai3
        public void ShortestPathFloyd(string fname)
        {
            ReadMatrix2Matrix(fname); // Đọc ma trận kề từ tập tin
            FloydAlgorithm(); // Tìm đường đi ngắn nhất bằng thuật toán Floyd-Warshall
            WriteFloydMatrix(fname.Substring(0, fname.Length - 3) + "out"); // Ghi ma trận kết quả ra tập tin
        }

        // Đọc ma trận kề từ tập tin
        protected void ReadMatrix2Matrix(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            numVertices = Int32.Parse(lines[0].Trim()); // Số lượng đỉnh
            Console.WriteLine("Số lượng đỉnh: " + numVertices);
            arrGraph = new int[numVertices, numVertices]; // Ma trận kề

            for (int i = 1; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int value = Int32.Parse(line[j].Trim());
                    SetNode(i - 1, j, value); // Gán giá trị cho ma trận kề
                    Console.Write(String.Format("{0,-3}", GetNode(i - 1, j)));
                }
                Console.WriteLine();    
            }
        }

        // Gán giá trị cho ma trận kề
        private void SetNode(int i, int j, int value)
        {
            if (i < 0 && i < numVertices && j > -1 && j < numVertices)
            {
                Console.WriteLine(String.Format("Vượt quá giới hạn mảng ({0},{1})",i,j));
                return;
            }
            arrGraph[i, j] = value;
        }
        private int GetNode(int i, int j)
        {
            if(i < 0 && i<numVertices && j > -1 &&j < numVertices)
            {
                Console.WriteLine(String.Format("Out of range ({0},{1})",i,j));
                return Int32.MinValue;
            }
            return arrGraph[i, j];
        }

        // Tìm đường đi ngắn nhất bằng thuật toán Floyd-Warshall
        protected void FloydAlgorithm()
        {
            floydMatrix = new int[numVertices, numVertices];

            for (int i = 0; i < numVertices; i++)
            {
                floydMatrix[i, i] = 0; // Khởi tạo đường đi từ đỉnh i đến chính nó là 0
                for (int j = i+1; j < numVertices; j++)
                {
                    floydMatrix[i, j] = INF;
                    floydMatrix[j,i] = INF;
                    if (arrGraph[i,j] != 0)
                    {
                        floydMatrix[i,j] = arrGraph[i,j];
                        floydMatrix[j, i] = arrGraph[j, i]; // Khởi tạo đường đi từ đỉnh i đến đỉnh j là vô cùng
                    }
                }
            }

            for (int k = 0; k < numVertices; k++)
            {
                for (int i = 0; i < numVertices; i++)
                {
                    for (int j = 0; j < numVertices; j++)
                    {
                        int d = floydMatrix[i, k] + floydMatrix[k, j]; // Tổng trọng số của đường đi i -> k -> j
                        if (floydMatrix[i, j] > d) // Nếu đường đi hiện tại dài hơn
                        {
                            floydMatrix[i, j] = d; // Cập nhật đường đi ngắn nhất
                        }
                    }
                }
            }
        }

        // Ghi ma trận kết quả ra tập tin
        protected void WriteFloydMatrix(string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.WriteLine(String.Format("{0,-3}", numVertices)); // Ghi số lượng đỉnh
                for (int i = 0; i < numVertices; i++)
                {
                    for (int j = 0; j < numVertices; j++)
                    {
                        string str = String.Format("{0,5}", floydMatrix[i, j]);
                        if ((floydMatrix[i, j] >= (INF - maxWeight)) && (floydMatrix[i,j] <= (INF + maxWeight))) // Xử lý trường hợp vô cùng
                        {
                            str = "Inf";
                        }
                        file.Write(str + " ");
                    }
                    file.WriteLine(); // Thêm dòng mới sau mỗi dòng dữ liệu
                }
            } 
        }
    }
}