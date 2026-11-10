using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DSA.Lab05
{
    /// <summary>
    /// Iterative graph traversals.
    ///
    /// Complete these methods after the adjacency representation is working.
    /// Think in terms of:
    /// frontier + visited + neighbors.
    /// </summary>
    public static class GraphTraversal {

        /**
         * Complexity:
         * With an adjacency representation, both BFS and DFS visit each reachable node at most once 
         * and inspect each reachable outgoing edge at most once. Both will have the same 
         * time complexity for full traversal.
         * 
         * 
         * O(V + E), the algorithm visits every single vertex and checks every single edge in the graph.
         * Execution time grows linearly as the number of nodes and connections increases.
         * 
         * V = Vertices (nodes or points), for example rooms in a dungeon, or cities on a map
         * E = Edges (the connections between those vertices), for example roads or hallways
         * 
         * visited                          O(V)
         * frontier (queue and stack)       O(V) worst case
         * returnValue                      O(V) when returning traversal order
         */
        public static IReadOnlyList<string> BreadthFirstOrder(AdjacencyGraph graph, string start) {
            List<string> returnValue = new List<string>();

            if (graph == null) {
                throw new NullReferenceException("The graph is null");
            }

            if (graph.NodeCount <= 0) {
                throw new InvalidOperationException("The graph contains no nodes.");
            }

            if (!graph.ContainsNode(start)) {
                //return default!;
                return returnValue;
            }

            var visited = new HashSet<string>();
            var queue = new Queue<string>();
            queue.Enqueue(start);
            visited.Add(start);
            while (queue.Count > 0) {
                string currentNode = queue.Dequeue();
                returnValue.Add(currentNode);

                foreach (string neighbor in graph.GetNeighbors(currentNode)) {
                    if (visited.Add(neighbor)) {
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return returnValue;
        }

        public static IReadOnlyList<string> DepthFirstOrder(AdjacencyGraph graph, string start) {
            List<string> returnValue = new List<string>();

            if(graph == null) {
                throw new NullReferenceException("The graph is null");
            }

            if (graph.NodeCount <= 0) {
                throw new InvalidOperationException("The graph contains no nodes.");
            }

            if (!graph.ContainsNode(start)) {
                //return default!;
                return returnValue;
            }

            // TODO:
            // Optional: Implement a recursive solution
            // Optional: reconstruct an unweighted path

            /**
             * Pop-Time Checking
             * Simpler and more intuitive to write (process and mark when popped), but may temporarily let 
             * duplicate references sit on the stack.
             */
            /*var visited = new HashSet<string>();
            var stack = new Stack<string>();
            stack.Push(start);

            while (stack.Count > 0) {
                string currentNode = stack.Pop();
                if (visited.Add(currentNode)) {
                    returnValue.Add(currentNode);
                    var neighbors = graph.GetNeighbors(currentNode);
                    for (int i = neighbors.Count - 1; i >= 0; i--) {
                        string neighbor = neighbors[i];
                        if (!visited.Contains(neighbor)) {
                            stack.Push(neighbor);
                        }
                    }
                }
            }*/

            /**
             * Push-Time Checking 
             * Better for massive graphs because it prevents duplicate nodes from ever entering the stack 
             * (saving memory), but requires pre-adding start and looping neighbors backwards.
             */
            var visited = new HashSet<string>();
            var stack = new Stack<string>();
            stack.Push(start);
            visited.Add(start);

            while (stack.Count > 0) {
                string currentNode = stack.Pop();
                returnValue.Add(currentNode);
                var neighbors = graph.GetNeighbors(currentNode);
                for (int i = neighbors.Count - 1; i >= 0; i--) {
                    string neighbor = neighbors[i];
                    if (!visited.Contains(neighbor)) {
                        visited.Add(neighbor);
                        stack.Push(neighbor);
                    }
                }
            }

            return returnValue;
        }

        public static bool IsReachableBfs(AdjacencyGraph graph, string start, string target) {
            if(graph == null) {
                throw new NullReferenceException("The graph is null");
            }

            if (graph.NodeCount <= 0) {
                throw new InvalidOperationException("The graph contains no nodes.");
            }

            if (!graph.ContainsNode(start) || !graph.ContainsNode(target)) {
                //return default!;
                return false;
            }

            var visited = new HashSet<string>();
            var queue = new Queue<string>();
            queue.Enqueue(start);
            visited.Add(start);
            while(queue.Count > 0) {
                string currentNode = queue.Dequeue();
                if (currentNode == target) {
                    return true;
                }

                foreach (string neighbor in graph.GetNeighbors(currentNode)) {
                    if (visited.Add(neighbor)) {
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return false;
        }

        public static bool IsReachableDfs(AdjacencyGraph graph, string start, string target) {
            if(graph == null) {
                throw new NullReferenceException("The graph is null");
            }

            if (graph.NodeCount <= 0) {
                throw new InvalidOperationException("The graph contains no nodes.");
            }

            if (!graph.ContainsNode(start) || !graph.ContainsNode(target)) {
                //return default!;
                return false;
            }

            var visited = new HashSet<string>();
            var stack = new Stack<string>();
            stack.Push(start);

            while(stack.Count > 0) {
                string currentNode = stack.Pop();
                if (currentNode == target) {
                    return true;
                }

                if (visited.Add(currentNode)) {
                    var neighbors = graph.GetNeighbors(currentNode);
                    for (int i = neighbors.Count - 1; i >= 0; i--) {
                        string neighbor = neighbors[i];
                        if (!visited.Contains(neighbor)) {
                            stack.Push(neighbor);
                        }
                    }
                }
            }

            return false;
        }
    }
}
