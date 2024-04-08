using _22DH110521_Lab05_DFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _22DH110521_Lab05_DFS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            //graph.ListCntVertices("LienThongDFS.INP");//Bai01
            graph.PathDFS("TimDuongDFS.INP");//Bai02

        }
    }


}