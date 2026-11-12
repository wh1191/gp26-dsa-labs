using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DSA.Lab06
{
    public sealed class DijkstraRouteFinder
    {
        public RouteResult FindRoute(WeightedGraph graph, string start, string goal)
        {
            if (graph == null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            /**
             * If either the 'start' or 'goal' node doesn't exist in the graph.
             */
            if (!graph.ContainsNode(start) || !graph.ContainsNode(goal))
            {
                return RouteResult.NotFound(0);
            }

            /**
             * If already at the destination, immediately return a success with a path containing just that single node.
             */
            if (start == goal)
            {
                return RouteResult.Success(new[] { start }, 0, 0);
            }

            var bestCost = new Dictionary<string, int>(); //Remembers the cheapest known cost to reach any node from the start
            var cameFrom = new Dictionary<string, string>(); //A trail that leads to the start node from the goal node
            //Holds nodes you need to explore next, automatically sorting them so the cheapest node is always at the front
            //Dequeue() pulls the lowest priority first 
            var frontier = new MinFrontier<string>();

            bestCost[start] = 0; //0 cost since it's the starting point
            frontier.Enqueue(start, 0);

            int expandedNodes = 0;

            // TODO Lab 6A:
            // 1. Repeatedly remove the smallest-priority frontier candidate.
            // 2. Ignore stale entries when a cheaper cost is already known.
            // 3. Finalize/expand the current node.
            // 4. If it is the goal, reconstruct the route using cameFrom.
            // 5. Relax each outgoing edge:
            //      candidate = bestCost[current] + edge.Cost
            //    If candidate improves the known cost, update both bestCost and cameFrom,
            //    then enqueue the neighbor with the improved total cost.
            //
            // Implement one small piece at a time and run the relevant tests.

            //Keep exploring as long as there are still candidate nodes left in the queue
            while(frontier.Count > 0) {
                var currentEntry = frontier.Dequeue(); //Pulls out the node with the lowest current cost
                string currentValue = currentEntry.Value;
                int currentPriority = currentEntry.Priority;

                //Stale check, if a cheaper path to this node was discovered after this specific entry was added to the queue,
                //this entry is considered as stale and will be skipped entirely and move on to the next entry
                if (currentPriority > bestCost[currentValue]) {
                    continue;
                }

                expandedNodes++; //If it's a valid, fresh entry, that node is counted as an expanded one

                //Reaching the Goal & Path Reconstruction
                //If the node matches the goal, the shortest path has been found
                if (currentValue == goal) {
                    List<string> path = new List<string>();
                    string currentNode = goal; //used for backtracking

                    while (currentNode != start) {
                        //use the cameFrom trail to hop between nodes backward
                        //then add those nodes to the path list that's gonna be used as return value
                        path.Add(currentNode);
                        currentNode = cameFrom[currentNode]; 
                    }
                    path.Add(currentNode);
                    path.Reverse(); //so the list reads correctly each node from start to goal
                    //bundle the path, the final cost and the count into a successful result and exit the function
                    return RouteResult.Success(path, currentPriority, expandedNodes); 
                } else {
                    //Edge Relaxation (Exploring Neighbors)
                    //If the goal hasn't been reached yet, go through every possible outgoing connection from the current node
                    foreach (var edge in graph.GetOutgoing(currentValue)) {
                        string neighbor = edge.To;
                        //Calculate how much it would cost to reach the neighboring node by taking this specific edge
                        int candidateCost = bestCost[currentValue] + edge.Cost;
                        //Either if this neighbor hasn't been visited, or if the recently calculated cost for this route
                        //is cheaper than any previously recorded to route to this neighbor
                        if (!bestCost.ContainsKey(neighbor) || bestCost[neighbor] > candidateCost) {
                            bestCost[neighbor] = candidateCost;
                            //building the backward trail, to remember that this neighbor was reached via currentValue
                            cameFrom[neighbor] = currentValue;
                            //push the neighbor into the priority queue so it can be explored later
                            frontier.Enqueue(neighbor, candidateCost);
                        }
                    }
                }
            }
            //Fallback, if the frontier is empty and there's no valid paths leading to the goal
            return RouteResult.NotFound(expandedNodes);
        }
    }
}
