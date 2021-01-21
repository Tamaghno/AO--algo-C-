using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace and_or_graph
{
    class Operator
    {
        private double              cost;
        private Node                start;
        private LinkedList<Node>    ends;

        public double Cost { get => cost; set => cost = value; }
        internal Node Start { get => start; set => start = value; }
        internal LinkedList<Node> Ends { get => ends; set => ends = value; }

        public Operator(double cost, Node start, LinkedList<Node> ends = null)
        {
            this.cost = cost;
            this.start = start;

            if (ends == null)
            {
                this.ends = new LinkedList<Node>();
            }
            else
            {
                this.ends = ends;
            }
            
            start.AddOperator(this);
        }

        public void AddEndNode(Node node)
        {
            this.ends.AddFirst(node);
        }

        public double HeuristicCost()
        {
            double mCost = 0.0f;
            foreach (Node end in ends)
            {
                mCost += end.Heuristic;
            }

            return mCost;
        }
    }
}
