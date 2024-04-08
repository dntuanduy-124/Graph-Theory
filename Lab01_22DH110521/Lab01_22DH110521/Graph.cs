using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab01_22DH110521
{
    internal class Graph
    {
        //Properties:So dinh cua do thi 
        public int N { get; set; }
        internal void VertexDegreeAM(string fname)
        {
            //Doc do thi tu fname
            ReadAdjMatrix(fname);
            //Tinh bac cua do thi va ghi ket qua vao fname
            WriteVertexDegreeAM(fname.Substring(0, fname.Length - 3) + "OUT");

        }

        //ma tran ke 
        int[,] adjMatrix;


        //Constructor khoi tao
        public Graph()
        {
            N = 0;
        }
        //Bai 01
        public void ReadAdjMatrix(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            N = int.Parse(lines[0].Trim());

            Console.WriteLine("\tNumber of vertices: " + N);//6
            adjMatrix = new int[N, N];//6x6
            for (int i = 1; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(' ');  //array 1D,6  
                for (int j = 0; j < line.Length; j++)
                {
                    int value = int.Parse(line[j].Trim());
                    adjMatrix[i - 1, j] = value;
                    Console.Write(String.Format("{0,-3}", adjMatrix[i - 1, j]));
                }
                Console.WriteLine();
            }

        }

        public void WriteVertexDegreeAM(string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.WriteLine(String.Format("{0,-3}", N));
                for (int i = 0; i < N; i++)
                {
                    int degree = 0;
                    for (int j = 0; j < N; j++)
                    {
                        degree += adjMatrix[i, j];
                    }
                    file.Write(String.Format("{0,-3}", degree));
                }
                file.WriteLine();
            }
        }
        //Bai02
        internal void Bai_2()
        {
            //so dinh
            int N;
            //ma tran ke
            int[,] maTran;

            //Doc du lieu ma tran ke tu file
            try
            {
                using (StreamReader sr = new StreamReader("BacVaoRa.INP"))
                {
                    N = int.Parse(sr.ReadLine().Trim());
                    maTran = new int[N, N];
                    for (int i = 0; i < N; i++)
                    {
                        string line = sr.ReadLine();
                        string[] mang = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < N; j++)
                        {
                            maTran[i, j] = int.Parse(mang[j]);
                        }
                    }
                    sr.Close();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Ko tim thay file");
                return;
            }

            int[] In = new int[N];//bac vao
            int[] Out = new int[N];//bac ra
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (maTran[i, j] != 0)
                        Out[i]++;
                }
            }
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (maTran[j, i] != 0)
                        In[i]++;
                }
            }
            //In ket qua ra man hinh va ra file
            using (StreamWriter sw = new StreamWriter("BacVaoRa.OUT"))
            {
                Console.WriteLine(N);
                sw.WriteLine(N);
                for (int i = 0; i < N; i++)
                {
                    Console.WriteLine(In[i] + " " + Out[i]);
                    sw.WriteLine(In[i] + " " + Out[i]);
                }
            }
        }
        //Bai03
        List<List<int>> adjList;
        public void AdjacencyList(string fname)
        {
            ReadAdjList(fname);
            WriteVertexDegreeAL(fname.Substring(0, fname.Length - 3) + "OUT");
        }



        private void ReadAdjList(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            N = int.Parse(lines[0].Trim());
            Console.WriteLine("So dinh: " + N);
            adjList = new List<List<int>>();

            for (int i = 0; i < N; i++)
            {
                adjList.Add(new List<int>());
                string[] line = lines[i + 1].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {

                    int v = Int32.Parse(line[j].Trim(' ')) ;
                    adjList[i].Add(v);
                }

            }
        }
        private void WriteVertexDegreeAL(string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.WriteLine(String.Format("{0,-3}", N));
                foreach (List<int> list in adjList)
                    file.Write(String.Format("{0,-3}", list.Count));
            }
            
        }

    


        //Bai03
        internal void Bai_3()
            {
                //so dinh
                int Numcertices;
                //ma tran ke
                List<List<int>> adjMatrix = new List<List<int>>();

                //Doc du lieu danh sach ke tu file
                try
                {
                    using (StreamReader r = new StreamReader("DanhSachKe.INP"))
                    {
                        Numcertices = int.Parse(r.ReadLine().Trim());
                        for (int i = 0; i < Numcertices; i++)
                        {
                            string line = r.ReadLine();
                            string[] str = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            //Add phan tu vao hang
                            List<int> hang = new List<int>();
                            foreach (string s in str)
                            {
                                hang.Add(int.Parse(s));
                            }
                            adjMatrix.Add(hang);
                        }
                        r.Close();
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Khong tim thay file!!! ");
                    return;
                }


                //In ket qua ra man hinh va ra file
                using (StreamWriter sw = new StreamWriter("DanhSachKe.OUT"))
                {
                    Console.WriteLine(Numcertices);
                    sw.WriteLine(Numcertices);
                    for (int i = 0; i < Numcertices; i++)
                    {
                        Console.Write(adjMatrix[i].Count + " ");
                        sw.Write(adjMatrix[i].Count + " ");
                    }
                }
                Console.WriteLine();
            }
        struct Canh
        {
            public int dinh_a;
            public int dinh_b;
            public Canh(int dinh_a, int dinh_b)
            {
                this.dinh_a = dinh_a;
                this.dinh_b = dinh_b;
            }
        }
        internal void Bai4()
        {
            //so dinh
            int numVertices;
            //so canh
            int numEdges;
            List<Canh> Matrix = new List<Canh>();

            //Doc du lieu danh sach canh tu file
            try
            {
                using (StreamReader s = new StreamReader("DanhSachCanh.INP"))
                {
                    string line = s.ReadLine();
                    string[] st = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    numVertices = int.Parse(st[0]);
                    numEdges = int.Parse(st[1]);
                    for (int i = 0; i < numEdges; i++)
                    {
                        line = s.ReadLine();
                        st = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        Canh c = new Canh(int.Parse(st[0]), int.Parse(st[1]));
                        Matrix.Add(c);
                    }

                    s.Close();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Khong tim thay file!!! ");
                return;
            }

            int[] bac = new int[numVertices];
            for (int i = 0; i < numEdges; i++)
            {
                bac[Matrix[i].dinh_a - 1]++;
                bac[Matrix[i].dinh_b - 1]++;
            }
            //In ket qua ra man hinh va ra file
            using (StreamWriter sw = new StreamWriter("DanhSachCanh.OUT"))
            {
                Console.WriteLine(numVertices + " " + numEdges);
                sw.WriteLine(numVertices + " " + numEdges);
                for (int i = 0; i < numVertices; i++)
                {
                    Console.Write(bac[i] + " ");
                    sw.Write(bac[i] + " ");
                }
            }
            Console.WriteLine();
        }


    }
}






