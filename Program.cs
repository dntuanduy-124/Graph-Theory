using System;

namespace lab08ltdt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Graph graph = new Graph();
            graph.GoMargin("RaBien.INP");
            //graph.ChooseCity("ChonThanhPho.INP");
            //graph.MoveCircle("DuongTron.INP");
            //graph.Go2School("SCHOOL.INP");
        }
    }
}
