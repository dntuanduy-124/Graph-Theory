r﻿using _22DH110521_Lab2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _22DH110521_Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            //graph.ConvertEL2AL(" Canh2Ke.INP");//Bai01
            graph.ConvertAL2EL("DSKe2Canh.INP");//Bai02
            //graph.StorageTank("BonChua.INP");//Bai03
            //graph.TransposeGraph("ChuyenVi.INP");//Bai04
            //graph.AverageEdge("TrungBinhCanh.INP");//Bai05
            graph.AdjacencyMatrix2AdjacencyList("MaTran2DSKe.INP");//BT 1 extra
            Console.ReadLine();
            
        }
    }
}
