using System;
namespace _22DH110521_Lab09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            //graph.SpanningTree("CayKhung.INP");//Bai01
            //graph.Kruskal("Kruskal.INP");//Bai02
            //graph.Prim_MST("Prim.INP");//Bai03
            graph.SpanningTreeX("CayKhungX.INP");//Bai04
            //graph.VillageRoads("Roads.INP");//Bai05
        }

    }
}