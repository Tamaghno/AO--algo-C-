using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace and_or_graph
{
    class AOStar
    {
        /// <summary>
        /// Find all available paths from start node in the defined graph
        /// </summary>
        /// <param name="graph">The data set where graph is exist</param>
        /// <param name="start">Id of node which will consider as root</param>
        public static void Find(AOGraph graph, int start)
        {
            new AOStar().Run(graph, start);
        }

        private HashSet<Node> opened;
        private HashSet<Node> closed;
        /// <summary>
        /// key: int is index of node like is stored at the graph object
        /// value: double value is cost to get this node from the spiecified root node 
        /// </summary>
        private Dictionary<int, double> costs;

        private HashSet<Node> terminals;

        private AOStar()
        {
            this.opened = new HashSet<Node>();
            this.closed = new HashSet<Node>();
            this.costs  = new Dictionary<int, double>();

            this.terminals = new HashSet<Node>();
        }

        private void Run(AOGraph mGraph, int start)
        {
            foreach(Node node in mGraph.Nodes)
            {
                this.costs.Add(node.Id, double.MaxValue);
            }

            this.opened.Add(mGraph.Nodes.ElementAt(start));
            this.costs[start] = 0;

            while(opened.Count > 0)
            {
                /// current => is the best node to continue search from it's sub graph
                Node current = opened.Min();
                opened.Remove(current);
                closed.Add(current);

                if(current.IsTerminal)
                {
                    this.terminals.Add(current);
                    Refresh(current.Parent);
                    continue;
                }
                else
                {
                    foreach(Operator op in current.Operators)
                    {
                        bool solveable = true;

                        foreach (Node successor in op.Ends)
                        {
                            if (!successor.IsTerminal && (successor.Operators == null || successor.Operators.Count == 0))
                            {
                                solveable = false;
                                break;
                            }
                        }

                        if(solveable)
                        {
                            foreach (Node successor in op.Ends)
                            {
                                successor.Parent = current;
                                successor.Cost = current.Cost + G(current, successor);

                                ///Here we must to override the comparator.
                                if (!opened.Contains(successor) && !closed.Contains(successor))
                                {
                                    costs[successor.Id] = successor.Cost;
                                    opened.Add(successor);
                                }
                            }
                        }
                        else
                        {
                            Refresh(current);
                        }
                    }
                }
            }

            if(costs[start] >= double.MaxValue)
            {
                Console.WriteLine("No available paths from start = [{0:D}]", start);
                return;
            }

            Console.WriteLine("Best solution = " + this.costs[start]);
            foreach (Node node in mGraph.Nodes)
            {
                Console.WriteLine(node.Cost + ", " + costs[node.Id]);
            }
        }

        private void Refresh(Node node)
        {
            if (node == null)
                return;

            double bestCost = double.MaxValue;
            foreach (Operator op in node.Operators)
            {
                double opCost = 0;
                foreach(Node n in op.Ends)
                {
                    opCost += costs[n.Id];
                }

                bestCost = Math.Min(bestCost, opCost + op.Cost);
            }
            
            if(node.Parent != null)
                costs[node.Id] = costs[node.Parent.Id] + 1 + bestCost;
            else
                costs[node.Id] =  1 + bestCost;

            Refresh(node.Parent);
        }

        private double G(Node current, Node successor)
        {
            return 1;
        }
    }
}