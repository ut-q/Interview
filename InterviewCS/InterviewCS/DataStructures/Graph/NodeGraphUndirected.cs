using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace InterviewCS.DataStructures.Graph
{
    // add an interface
    public class NodeGraphUndirected
    {
        public NodeGraphUndirected()
        {
            Vertices = new Dictionary<int, Vertex>();
        }

        public void BulkAddEdges(List<Tuple<int, int>> graphList)
        {
            foreach (var tuple in graphList)
            {
                InsertEdge(tuple.Item1, tuple.Item2);
            }
        }

        // assumption: undirected
        public void InsertEdge(int node1, int node2)
        {
            Vertex v1 = GetVertex(node1) ?? CreateVertex(node1);

            Vertex v2 = GetVertex(node2) ?? CreateVertex(node2);

            v1.AddEdge(v2);
            v2.AddEdge(v1);
        }

        public Vertex GetVertex(int id)
        {
            Vertices.TryGetValue(id, out var v);

            return v;
        }

        public Vertex CreateVertex(int id)
        {
            var v = GetVertex(id);
            if (v == null)
            {
                v = new Vertex(id);
                Vertices[id] = v;
            }

            return v;
        }

        public bool IsEmpty()
        {
            return Vertices.Count == 0;
        }

        #region BFS

        public class BFS
        {
            public HashSet<int> Discovered { get; }
            private HashSet<int> Processed { get; }

            private Dictionary<int, int> Parent { get; }

            private Queue<Vertex> BFSQueue { get; }

            private Vertex sourceVertex;

            private readonly NodeGraphUndirected Graph;

            private Action<Vertex> earlyProcess;
            private Action<Vertex, Vertex> process;
            private Action<Vertex> lateProcess;

            public BFS(int start, NodeGraphUndirected graph, Action<Vertex> early = null,
                Action<Vertex,Vertex> regular = null, Action<Vertex> late = null)
            {
                Graph = graph;

                if (Graph.IsEmpty())
                {
                    Console.WriteLine("Empty Graph, not executing BFS");
                    return;
                }

                sourceVertex = Graph.GetVertex(start);
                if (sourceVertex == null)
                {
                    Console.WriteLine("Source Node doesn't exist, not executing BFS");
                    return;
                }
                Discovered = new HashSet<int>();
                Processed = new HashSet<int>();
                Parent = new Dictionary<int, int>();
                BFSQueue = new Queue<Vertex>();

                earlyProcess = early;
                process = regular;
                lateProcess = late;

                Execute_BFS();
            }

            public void Execute_BFS()
            {
                BFSQueue.Enqueue(sourceVertex);
                Discovered.Add(sourceVertex.Id);
                Parent.Add(sourceVertex.Id, -1);

                while (BFSQueue.Count != 0)
                {
                    var current = BFSQueue.Dequeue();

                    ProcessVertex_Early(current);

                    Processed.Add(current.Id);

                    foreach (var node in current.AdjacencyList)
                    {
                        if (!Processed.Contains(node.Id))
                        {
                            ProcessEdge(current, node);
                        }

                        if (!Discovered.Contains(node.Id))
                        {
                            Discovered.Add(node.Id);
                            BFSQueue.Enqueue(node);

                            if (!Parent.ContainsKey(node.Id))
                            {
                                Parent[node.Id] = current.Id;
                            }
                            else
                            {
                                Console.WriteLine("We shouldn't be here during BFS");
                            }
                        }
                    }

                    ProcessVertex_Late(current);
                }
            }

            public List<Vertex> FindPath(int destination)
            {
                var path = new List<Vertex>();
                var end = Graph.GetVertex(destination);
                if (end != null)
                {
                    FindPath_Internal(end, path);
                }

                return path;
            }

            // find path between sourceVertex and Vertex end
            // assuming both are in the same connected component
            public void FindPath_Internal(Vertex end, List<Vertex> path)
            {
                path.Add(end);

                if (sourceVertex != end && end != null && HasParent(end.Id))
                {
                    FindPath_Internal(Graph.GetVertex(Parent[end.Id]), path);
                }
            }

            private bool HasParent(int id)
            {
                return Parent != null && Parent.ContainsKey(id);
            }

            private void ProcessVertex_Early(Vertex v)
            {
                earlyProcess?.Invoke(v);
            }

            private void ProcessEdge(Vertex v, Vertex x)
            {
                process?.Invoke(v, x);
            }

            private void ProcessVertex_Late(Vertex v)
            {
                lateProcess?.Invoke(v);
            }
        }

        public int NumberOfConnectedComponents()
        {
            List<BFS> searches = new List<BFS>();
            HashSet<int> discovered = new HashSet<int>();

            foreach (var (id, vertex) in Vertices)
            {
                if (!discovered.Contains(id))
                {
                    var search = new BFS(id, this);
                    searches.Add(search);

                    discovered.UnionWith(search.Discovered);
                }
            }

            return searches.Count;
        }

        #endregion // BFS

        #region DFS

        public class DFS
        {

        }

        #endregion


        public string PrintGraph()
        {
            string graph = "";

            foreach (var v in Vertices)
            {
                graph += v + "\n";
            }

            return graph;
        }

        private Dictionary<int, Vertex> Vertices { get; }
    }
}
