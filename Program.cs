using Lab04_22DH110521;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04_22DH110521
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            //graph.ConnectedComponents("MienLienThong.INP");//Bai01
            graph.BridgeBFS("CanhCau.INP");//Bai02
            //graph.CutVertexBFS("DinhKhop.INP");//Bai03
            //graph.GridPathBFS("Gird.INP");//Bai04

        }
    }


}