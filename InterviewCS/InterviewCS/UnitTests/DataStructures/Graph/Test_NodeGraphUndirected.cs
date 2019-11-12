using System;
using System.Collections.Generic;
using System.Text;

using InterviewCS.DataStructures.Graph;

namespace InterviewCS.UnitTests.DataStructures.Graph
{
    // interface for unit tests
    public class Test_NodeGraphUndirected
    {
        private NodeGraphUndirected graph;

        private List<Tuple<int, int>> graphList;

        #region BFS test values

        private int bfsSource = 0;

        private int pathSource = 0;

        private int pathDestination = 0;

        private List<int> correctPathBack = new List<int>();

        private int nrOfConnectedComponents = 0;

        private string bfsTestString;

        #endregion


        public Test_NodeGraphUndirected(string graphStr, string bfsTest)
        {
            var tuples = graphStr.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            graphList = new List<Tuple<int, int>>();

            foreach (var tuple in tuples)
            {
                var ids = tuple.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (ids.Length > 2)
                {
                    Console.WriteLine("ERROR in Input: too many Ids in one line");
                    continue;
                }

                graphList.Add(new Tuple<int, int>(int.Parse(ids[0]), int.Parse(ids[1])));
            }

            bfsTestString = bfsTest;
        }

        private void PopulateTestValuesBFS()
        {
            var pieces = bfsTestString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (pieces.Length == 0)
            {
                return;
            }
            
            bfsSource = int.Parse(pieces[0]);

            pathSource = int.Parse(pieces[1]);

            pathDestination = int.Parse(pieces[2]);

            nrOfConnectedComponents = int.Parse(pieces[3]);

            correctPathBack = new List<int>();

            for (int i = 4; i < pieces.Length; ++i)
            {
                correctPathBack.Add(int.Parse(pieces[i]));
            }
        }

        public bool Execute()
        {
            graph = new NodeGraphUndirected();

            Test_BulkAdd();

            Test_Print();

            PopulateTestValuesBFS();

            Test_BFS(bfsSource);

            Test_Path(pathSource, pathDestination, correctPathBack);

            Test_NumberOfConnectedComponents(nrOfConnectedComponents);

            return true;
        }


        private bool Test_BulkAdd()
        {
            Console.WriteLine("Testing Bulk Add");
            graph.BulkAddEdges(graphList);

            return true;
        }

        private bool Test_Print()
        {
            Console.WriteLine("PRINTING------------------");

            Console.WriteLine(graph.PrintGraph());

            Console.WriteLine("END PRINTING------------------\n\n\n\n");

            return true;
        }

        private bool Test_BFS(int v)
        {
            Console.WriteLine("BFS on " + v + "------------------");

            Action<Vertex> early = vert => { Console.WriteLine("Early Processing Vertex " + vert.Id); };

            Action<Vertex,Vertex> process = (v1,v2) => { Console.WriteLine("Processing Edge " + v1.Id + " " + v2.Id); };

            Action<Vertex> late = vert => { Console.WriteLine("Late Processing Vertex " + vert.Id); };

            NodeGraphUndirected.BFS bfs = new NodeGraphUndirected.BFS(v, graph, early, process, late);

            Console.WriteLine("END BFS------------------\n\n\n\n");

            return true;
        }

        private bool Test_Path(int src, int dest, List<int> pathBack)
        {
            Console.WriteLine("Path from " + src + "to " + dest + "------------------");

            NodeGraphUndirected.BFS bfs = new NodeGraphUndirected.BFS(src, graph);

            var path = bfs.FindPath(dest);

            if (path.Count != pathBack.Count)
            {
                Console.WriteLine("Path back failed ---------------\n\n\n\n");
                return false;
            }

            for (int i = 0; i < path.Count; ++i)
            {
                if (path[i].Id != pathBack[i])
                {
                    Console.WriteLine("Path back failed ---------------\n\n\n\n");
                    return false;
                }
            }

            Console.WriteLine("Path back success ---------------\n\n\n\n");
            return true;
        }

        private bool Test_NumberOfConnectedComponents(int count)
        {
            int total = graph.NumberOfConnectedComponents();

            Console.WriteLine("No. Of components: Real: " + total + " Expected: " + count + "\n\n\n\n");

            return total == count;
        }

        public static void ExecuteUnitTest()
        {
            //TEST 1
            Console.WriteLine("START TEST1");
            string graph1 = string.Empty;

            string bfsTest1 = string.Empty;

            Test_NodeGraphUndirected test1 = new Test_NodeGraphUndirected(graph1,bfsTest1);

            test1.Execute();

            Console.WriteLine("END TEST1 \n\n\n");

            //TEST 2
            Console.WriteLine("START TEST2");
            string graph2 = "1 2\n2 3\n3 4\n5 6\n1 7\n7 4\n";

            string bfsTest2 = "1 1 4 2 4 7 1";

            Test_NodeGraphUndirected test2 = new Test_NodeGraphUndirected(graph2, bfsTest2);

            test2.Execute();
            Console.WriteLine("END TEST2 \n\n\n");

            //TEST 3
            Console.WriteLine("START TEST3");
            string graph3 = "1 2\n";

            string bfsTest3 = "2 2 1 1 1 2";

            Test_NodeGraphUndirected test3 = new Test_NodeGraphUndirected(graph3, bfsTest3);

            test3.Execute();
            Console.WriteLine("END TEST3 \n\n\n");

            //TEST 4
            Console.WriteLine("START TEST4");
            string graph4 = "1 2\n3 4\n";

            string bfsTest4 = "3 3 4 2 4 3";

            Test_NodeGraphUndirected test4 = new Test_NodeGraphUndirected(graph4, bfsTest4);

            test4.Execute();
            Console.WriteLine("END TEST4 \n\n\n");

            //TEST 5
            Console.WriteLine("START TEST5");
            string graph5 = "1 2\n1 3\n2 4\n2 5\n3 6\n3 7\n5 8\n6 8\n";

            string bfsTest5 = "1 8 4 1 4 2 5 8";

            Test_NodeGraphUndirected test5 = new Test_NodeGraphUndirected(graph5, bfsTest5);

            test5.Execute();
            Console.WriteLine("END TEST5 \n\n\n");

            //TEST 6
            Console.WriteLine("START TEST6");
            string graph6 = "1 2\n1 3\n2 4\n2 5\n3 6\n3 7\n4 8\n4 9\n5 10\n5 11\n6 12\n6 13\n7 14\n7 15\n";

            string bfsTest6 = "1 15 8 1 8 4 2 1 3 7 15";

            Test_NodeGraphUndirected test6 = new Test_NodeGraphUndirected(graph6, bfsTest6);

            test6.Execute();
            Console.WriteLine("END TEST6 \n\n\n");
        }
    }
}
