using Lab03_22DH110521;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_22DH110521
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            //graph.ConnectedVertex("BFS.INP");//Bai01
            graph.PathBFS("TimDuong.INP");//Bai02
            //graph.BFSConnected("LienThong.INP");//Bai03
            //graph.ConnectedComponents("DemLienThong.INP");//Bai04
            //graph.AdjacencyMatrix2AdjacencyList("MaTran2DSKe.INP");//BT1 làm thêm
        }
    }

  
}