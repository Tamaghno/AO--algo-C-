using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace and_or_graph
{
    class Node : IComparable
    {
        private int     id;
        private double  cost;
        private double  heuristic;
        private bool    terminal;
        private HashSet<Operator> operators;
        private Node parent;

        public Node (int id, double cost, double heuristic, bool terminal, Node parent, HashSet<Operator> operators = null)
        {
            this.id         = id;
            this.cost       = cost;
            this.heuristic  = heuristic;
            this.terminal   = terminal;

            if(parent != null)
                this.parent = parent;

            if (operators != null)
                this.operators = operators;
            else
                this.operators = new HashSet<Operator>();
        }

        public void AddOperator(Operator item)
        {
            this.operators.Add(item);
        }

        public double FullCost()
        {
            double mCost = double.MaxValue;
            foreach (Operator Op in Operators)
            {
                mCost = Math.Min(mCost, Op.Cost + Op.HeuristicCost());
            }
            return mCost + this.Cost;
        }

        public int CompareTo(object obj)
        {
            if(obj is Node)
            {
                Node o = (Node)obj;
                double object_f = o.cost + o.heuristic, this_f = this.cost + this.heuristic;

                if (this_f == object_f)
                    return this.id.CompareTo(o.id);
                else
                    return this_f.CompareTo(object_f);
            }
            else
            {
                return int.MinValue;
            }
        }

        public int Id { get => id; set => id = value; }
        public double Heuristic { get => heuristic; set => heuristic = value; }
        public double Cost { get => cost; set => cost = value; }
        public bool IsTerminal { get => terminal; set => terminal = value; }
        internal HashSet<Operator> Operators { get => operators; set => operators = value; }
        internal Node Parent { get => parent; set => parent = value; }
    }
}
