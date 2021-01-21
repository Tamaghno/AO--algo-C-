using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace and_or_graph
{
    class Program
    {
        static void Main(string[] args)
        {
            AOGraph graph = new AOGraph();
            graph.CreateNode(0, 0,  50, false);
            graph.CreateNode(1, double.MaxValue, 30, false);
            graph.CreateNode(2, double.MaxValue, 20, false);
            graph.CreateNode(3, double.MaxValue, 20, false);
            graph.CreateNode(4, double.MaxValue, 20, false);
            graph.CreateNode(5, double.MaxValue, 20, false);
            graph.CreateNode(6, double.MaxValue, 15, true);
            graph.CreateNode(7, double.MaxValue, 15, true);
            graph.CreateNode(8, double.MaxValue, 15, true);
            graph.CreateNode(9, double.MaxValue, 15, true);

            graph.CreateOperator(0, new int[] { 1 }, 0);
            graph.CreateOperator(0, new int[] { 2, 3 }, 38);
            graph.CreateOperator(1, new int[] { 4, 5 }, 17);
            graph.CreateOperator(2, new int[] { 6, 7 },  9);
            graph.CreateOperator(3, new int[] { 8, 9 }, 27);

/*
            HashSet<Operator> ops = graph.Nodes.ElementAt(0).Operators;
            foreach(Operator op in ops)
                Console.WriteLine(op.Ends.Count);
*/

            AOStar.Find(graph, 0);
            Console.ReadKey();
        }
    }
}
