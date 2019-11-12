using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewCS.DataStructures.Graph
{
    public class Vertex
    {
        public int Id { get; }

        public int Data => 0;

        public List<Vertex> AdjacencyList { get; }

        public Vertex(int id)
        {
            Id = id;
            AdjacencyList = new List<Vertex>();
        }

        public void AddEdge(Vertex edge)
        {
            //check for self cycle
            if (!AdjacencyList.Contains(edge))
            {
                AdjacencyList.Add(edge);
            }
        }

        public override string ToString()
        {
            var printedVertex = "[ " + Id + " {";

            foreach (var adj in AdjacencyList)
            {
                printedVertex += adj.Id + " ";
            }

            printedVertex += "} ]";

            return printedVertex;
        }
    }
}
