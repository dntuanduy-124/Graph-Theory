using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _22DH110521_Lab06
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
        //List<int> visitedVertices;


        //Attributes:danh sách kề
        LinkedList<int>[] adjList;
        //List<List<int>> adjList;
        //Attributes:Mang kt duoc vieng tham hay chua
        bool[] visited;
        //Attributes:danh lien truoc cua dinh ()dinh cha
        int[] pre;
        //Bài 1
        public int v { get; set; }
         int[] color;
        //private int currentColor;

        ////đỉnh
        //public int v { get; set; }

        //Bai1
        internal void BipartiteGraph(string fname)
        {
            ReadAdjListDFS(fname);
            bool flag = IsBipartite();
            WriteBipartice(fname.Substring(0, fname.Length - 3) + "OUT",flag);
        }

        private void ReadAdjListDFS(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            color = new int[N + 1];
            //S = Int32.Parse(line[1].Trim());

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

        // Phương thức kiểm tra  phân đôi của đồ thị
        private bool IsBipartite()
            
        {
            Array.Fill(color, -1); // Khởi tạo mảng : -1 chưa được tô màu, 0 màu 1, 1 màu 2

            // Duyệt qua tất cả các đỉnh của đồ thị
            for (int i = 1; i <= N; i++)
            {
                if (color[i] == -1) // Nếu đỉnh chưa được tô màu
                {
                    if (!DFSColoring(i)) // Nếu không thể tô màu cho đồ thị
                        return false;
                }
            }
            return true; // Đồ thị là đồ thị phân đôi
        }

        // Thuật toán DFS để tô màu cho đồ thị
        private bool DFSColoring(int vertex)
        {
            Stack<int> stack = new Stack<int>(); // Stack để thực hiện DFS
            stack.Push(vertex); // Đưa đỉnh bắt đầu vào stack
            color[vertex] = 0; // Tô màu cho đỉnh bắt đầu

            while (stack.Count > 0)
            {
                int currentVertex = stack.Pop(); // Lấy đỉnh hiện tại từ stack

                // Duyệt qua các đỉnh kề của đỉnh hiện tại
                foreach (int neighbor in adjList[currentVertex])
                {
                    if (color[neighbor] == -1) // Nếu đỉnh kề chưa được tô màu
                    {
                        stack.Push(neighbor); // Đưa đỉnh kề vào stack
                        color[neighbor] = 1 - color[currentVertex]; // Tô màu cho đỉnh kề
                    }
                    else if (color[neighbor] == color[currentVertex]) // Nếu 2 đỉnh kề cùng màu
                    {
                        return false; // Đồ thị không phân đôi
                    }
                }
            }
            return true; // Đồ thị phân đôi
        }
        //public bool Ispartte()
        //{
        //    ////mảng tô màu
        //    //int[] color = new int[N + 1];
        //    ////-1 chưa tô màu
        //    //Array.Fill(color, -1);
        //    //for (int i = 0; i <= N; i++)
        //    //{
        //    //    if (color[i] == -1)
        //    //    {
        //    //        //bắt đầu tìm DFS CHO ĐỈNH I với màu =0
        //    //        if (!DFSBai1(i, color, 0))
        //    //        {
        //    //            return false;
        //    //        }
        //    //    }
        //    //}
        //    //return true;
        //    bool flag = false;
        //    visited = new bool[N + 1];
        //    pre = new int[N + 1];
        //    int s = 1;
        //    Stack<int> StThuTuDuyet = new Stack<int>();

        //    visited[s] = true;

        //    pre[s] = -1;

        //    StThuTuDuyet.Push(s);//Đưa vào hàng đợi


        //    while (StThuTuDuyet.Count != 0)
        //    {
        //        int u = StThuTuDuyet.Pop();//Lấy ra

        //        foreach (int v in adjList[u - 1])
        //        {
        //            if (visited[v])//=true
        //                continue;

        //            visited[v] = true;
        //            pre[v] = u;
        //            StThuTuDuyet.Push(u);
        //            StThuTuDuyet.Push(v);

        //        }
        //    }
        //    return flag;    

        //}
        ////private bool DFSBai1(int v, int[] color, int currentColor)
        ////{
        ////    color = new int[N];

        ////    color[v] = currentColor;
        ////    //Xét các đỉnh kề với v
        ////    foreach (int dinhKe in adjList[v])
        ////    {
        ////        if (color[dinhKe] == -1)
        ////        {
        ////            //bắt đầu tìm DFS CHO ĐỈNH I với màu đối lập với màu đang xét
        ////            //kiem tra neu false thi khong la do thi bipartitle
        ////            if (!DFSBai1(dinhKe, color, 1 - currentColor))
        ////            {
        ////                return false;
        ////            }

        ////        }
        ////        //nếu đỉnh kề đã tô màu cùng màu với đỉnh đang xét thì không là phân đôi
        ////        else if (color[dinhKe] == color[v])
        ////        {
        ////            return false;
        ////        }
        ////    }
        ////    return true;//đồ thị 2 phía
        ////}

        public void WriteBipartice(string fname, bool isBipartite)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                if (isBipartite)
                {
                    file.WriteLine("\nYes");
                }
                else
                {
                    file.WriteLine("\nNo");

                }
                

            }
        }
        //Bai02
        internal void Cycle(string fname)
        {
            ReadAdjListDFS2(fname);
            bool flag = IsCycle();
            WriteCycleResult(fname.Substring(0, fname.Length - 3) + "OUT", flag);
        }
        private void ReadAdjListDFS2(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            color = new int[N + 1];
            //S = Int32.Parse(line[1].Trim());

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

        private bool IsCycle()
        {
            // Mảng visited dùng để theo dõi các đỉnh đã được duyệt
            bool[] visited = new bool[N + 1];

            // Duyệt qua tất cả các đỉnh của đồ thị
            for (int i = 1; i <= N; i++)
            {
                // Nếu đỉnh chưa được duyệt
                if (!visited[i])
                {
                    
                    if (IsCycle(i, -1, visited))
                        return true;          
                }
            }
            return false;      
        }

        private bool IsCycle(int current, int parent, bool[] visited)
        {
            visited[current] = true;       

            // Duyệt qua tất cả các đỉnh kề của đỉnh hiện tại
            foreach (int neighbor in adjList[current])
            {
                // Nếu đỉnh kề chưa được duyệt
                if (!visited[neighbor])
                {
                    
                    if (IsCycle(neighbor, current, visited))
                        return true;
                }
                // Nếu đỉnh kề đã được duyệt và không phải là đỉnh cha của đỉnh hiện tại
                else if (neighbor != parent)
                {
                    return true; // Trả về true
                }
            }
            return false;       
        }

        private void WriteCycleResult(string fname, bool flag)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                if (flag)
                {
                    file.WriteLine("\nYes");
                }
                else
                {
                    file.WriteLine("\nNo");

                }


            }
        }
        // Bài 03 
        internal void TopologicalSorting(string fname)
        {
            ReadAdjListDFS3(fname);
            List<int> list = TopoSorting();
            WriteTopologicalSorting(fname.Substring(0, fname.Length - 3) + "OUT", list);
        }

       
        private void ReadAdjListDFS3(string fname)
        {
            using (StreamReader reader = new StreamReader(fname))
            {
                N = int.Parse(reader.ReadLine());
                adjList = new LinkedList<int>[N + 1];
                color = new int[N + 1];

                for (int i = 1; i <= N; i++)
                {
                    adjList[i] = new LinkedList<int>();
                    string[] tokens = reader.ReadLine().Split(' ');
                    foreach (var token in tokens)
                    {
                        int neighbor;
                        if (int.TryParse(token, out neighbor))
                        {
                            adjList[i].AddLast(neighbor);
                        }
                        else
                        {
                            // Xử lý trường hợp chuỗi không đúng định dạng
                            Console.WriteLine($"Error: Invalid format at line {i}");
                        }
                    }
                }
            }
        }

        // Sắp xếp topo
        private List<int> TopoSorting()
        {
            List<int> result = new List<int>();
            bool[] visited = new bool[N + 1];
            Stack<int> stack = new Stack<int>();

            // DFS và đẩy đỉnh vào stack sau khi duyệt xong các đỉnh kề
            for (int i = 1; i <= N; i++)
            {
                if (!visited[i])
                {
                    DFS(i, visited, stack);
                }
            }

            // Lấy kết quả từ stack
            while (stack.Count > 0)
            {
                result.Add(stack.Pop());
            }

            return result;
        }

        
        private void DFS(int current, bool[] visited, Stack<int> stack)
        {
            visited[current] = true;

            foreach (int neighbor in adjList[current])
            {
                if (!visited[neighbor])
                {
                    DFS(neighbor, visited, stack);
                }
            }

            stack.Push(current);
        }

        //Xét trong danh sách
        private void WriteTopologicalSorting(string fname, List<int> list)
        {
            using (StreamWriter writer = new StreamWriter(fname))
            {
                foreach (int vertex in list)
                {
                    writer.Write(vertex + " ");
                }
            }
            foreach (int vertex in list)
            {
                Console.Write(vertex + " ");
            }
        }
    }
}



