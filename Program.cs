namespace lab07ltdt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            //graph.ShortestPath("Dijkstra.INP");
            graph.InterVertexShortestPath("NganNhatX.INP");
            //graph.ShortestPathFloyd("Floyd.INP");
            //Console.WriteLine("Nhấn phím bất kỳ để kết thúc chương trình!");
            //Console.ReadKey();
            

        }
    }
}
