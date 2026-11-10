using System;
using System.Collections.Generic;
using DSA.Lab05;

Console.WriteLine("DSA Lab 5 - Graph Representation and Traversal");
Console.WriteLine();

string[] nodes =
{
    "Entrance",
    "Hall",
    "StoreRoom",
    "Armory",
    "Courtyard",
    "BossRoom",
    "SecretRoom"
};

/**
 * Where is the cycle?
 * The cycle is between Hall and Courtyard (can loop indefinitely, 
 * due to directed edges between those two)
 * 
 * Can Entrance reach BossRoom?
 * Yes, there are 2 ways:
 * Entrance -> Hall -> Armory -> BossRoom
 * Entrance -> Hall -> Courtyard -> BossRoom
 * 
 * Can Entrance reach SecretRoom?
 * No (completely disconnected from the graph)
 * 
 * Can BossRoom reach Entrance?
 * No (no directed edges that leads from BossRoom to Entrance, only one way from Entrance to BossRoom)
 * 
 * Which nodes can I go to directly from Hall?
 * Armory
 * Courtyard
 * 
 * Does the representation match the workload?
 * No. The worload involves frequent traversals (repeatedly asking for neighbors), meaning the EdgeListGraph 
 * needs to scan through every single edge in the entire graph (that's why it's O(E) time complexity). 
 * 
 * An Adjacency List, where each node directly stores its own specific list of outgoing neighbors, is more 
 * preferable here because it makes neighbor lookups significantly faster.
 * 
 */
DirectedEdge[] edges =
{
    new DirectedEdge("Entrance", "Hall"),
    new DirectedEdge("Entrance", "StoreRoom"),
    new DirectedEdge("Hall", "Armory"),
    new DirectedEdge("Hall", "Courtyard"),
    new DirectedEdge("StoreRoom", "Courtyard"),
    new DirectedEdge("Armory", "BossRoom"),
    new DirectedEdge("Courtyard", "Hall"),
    new DirectedEdge("Courtyard", "BossRoom")
};

Console.WriteLine("Dungeon graph:");
Console.WriteLine("Entrance -> Hall, StoreRoom");
Console.WriteLine("Hall -> Armory, Courtyard");
Console.WriteLine("StoreRoom -> Courtyard");
Console.WriteLine("Armory -> BossRoom");
Console.WriteLine("Courtyard -> Hall, BossRoom");
Console.WriteLine("BossRoom ->");
Console.WriteLine("SecretRoom -> (disconnected)");
Console.WriteLine();

var edgeList = new EdgeListGraph(nodes, edges);
Console.WriteLine(
    "Edge-list neighbors of Hall: " +
    string.Join(", ", edgeList.GetNeighbors("Hall")));

var adjacency = new AdjacencyGraph(nodes, edges);
Console.WriteLine(
    "Adjacency neighbors of Hall: " +
    string.Join(", ", adjacency.GetNeighbors("Hall")));

try
{
    IReadOnlyList<string> bfs =
        GraphTraversal.BreadthFirstOrder(adjacency, "Entrance");

    IReadOnlyList<string> dfs =
        GraphTraversal.DepthFirstOrder(adjacency, "Entrance");

    Console.WriteLine("BFS: " + string.Join(" -> ", bfs));
    Console.WriteLine("DFS: " + string.Join(" -> ", dfs));
    Console.WriteLine(
        "Can Entrance reach BossRoom? " +
        GraphTraversal.IsReachableBfs(adjacency, "Entrance", "BossRoom"));
    Console.WriteLine(
        "Can Entrance reach SecretRoom? " +
        GraphTraversal.IsReachableBfs(adjacency, "Entrance", "SecretRoom"));
}
catch (NotImplementedException)
{
    Console.WriteLine();
    Console.WriteLine("Traversal methods are intentionally incomplete in the starter.");
    Console.WriteLine("Complete the adjacency grouping and traversal tasks, then run again.");
}
