using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace and_or_graph
{
    class AOGraph
    {
        HashSet<Node> nodes;
        HashSet<Operator> operators;

        public AOGraph()
        {
            this.nodes = new HashSet<Node>();
            this.operators = new HashSet<Operator>();
        }

        internal HashSet<Node> Nodes { get => nodes; }
        internal HashSet<Operator> Operators { get => operators; }

        public void CreateNode(int id, double cost, double heuristic, bool isTerminal, HashSet<Operator> operators = null)
        {
            this.nodes.Add(new Node(id, cost, heuristic, isTerminal, null, operators));
        }
        
        public void CreateOperator(int start, int[] ends, double cost)
        {
            Operator mOperator = new Operator(cost, this.nodes.ElementAt(start));
            foreach (int end in ends)
            {
                mOperator.AddEndNode(this.nodes.ElementAt(end));
            }

            this.operators.Add(mOperator);
        }

        public void Print()
        {
            Stack<int> q = new Stack<int>();

            q.Push(0);
            while(q.Count > 0)
            {
                Node top = nodes.ElementAt(q.Pop());

                Console.Write(" -> {0}", top.Id);

                foreach(Operator op in top.Operators)
                {
                    foreach (Node end in op.Ends)
                    {
                        q.Push(end.Id);
                    }
                }
            }
        }
    }
}
