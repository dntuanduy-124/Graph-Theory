using System;
namespace Lab01_22DH110521
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            //graph.VertexDegreeAM("BacDoThiVoHuong.INP");//Bai01
            //graph.Bai_2();//Bai02

            /*graph.VertexDegreeAL("DanhSachKe.INP");*/

            graph.AdjacencyList("DanhSachKe.INP");//
            //graph.Bai4();//Bai04
            


        }
    }
}