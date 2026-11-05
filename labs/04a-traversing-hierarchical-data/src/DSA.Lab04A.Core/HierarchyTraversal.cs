using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA.Lab04A
{
    /// <summary>
    /// Recursive algorithms over a rooted hierarchy.
    ///
    /// The key idea is structural recursion:
    /// solve the current node, then apply the same operation to each child subtree.
    /// </summary>
    public static class HierarchyTraversal
    {

        /**
         * What work happens at the current node?
         * Initiliazes count = 1 to account for the current node itself.
         * 
         * What smaller version of the same problem is delegated to each child?
         * Counting the total number of nodes in each child's respective subtree (CountNodes<T>(node)).
         * 
         * What stops the recursion? 
         * Reaching a leaf node, which skips the foreach loop over children.
         * 
         * What result is returned or accumulated?
         * An integer representing the total node count (1 plus the sum of all child subtree counts).
         */
        public static int CountNodes<T>(HierarchyNode<T> root)
        {
            if (root == null) {
                throw new ArgumentNullException(nameof(root));
            }
            // TODO: count this node, then count every child subtree.

            //return 1 + root.Children.Sum(child => CountNodesLinq(child));
            int count = 1;
            foreach (HierarchyNode<T> node in root.Children) {
                count += CountNodes<T>(node);
            }
            return count;
        }

        /**
         * What work happens at the current node?
         * Checks if the current node's value equals the target value.
         * 
         * What smaller version of the same problem is delegated to each child?
         * Searching for the target value within each child's subtree (Contains<T>(node, target)).
         * 
         * What stops the recursion? 
         * There's two conditions: 
         * Either finding a match in current node (return true and skip the code afterwards) or 
         * reaching a leaf node with no children left (return false).
         * 
         * What result is returned or accumulated?
         * A boolean (true if the target value exists anywhere in the subtree, else false).
         */
        public static bool Contains<T>(HierarchyNode<T> root, T target) {
            if (root == null) {
                throw new ArgumentNullException(nameof(root));
            }

            // TODO: compare the current node, then search child subtrees.

            if (EqualityComparer<T>.Default.Equals(root.Value, target)) { 
                return true;
            }
            foreach (HierarchyNode<T> node in root.Children) {
                if(Contains<T>(node, target)) { return true; }
            }
            return false;
        }

        /**
         * What work happens at the current node?
         * Creates a list, adds the value of the current node (root.Value first, parent before children) 
         * and then process all the children if they exist. 
         * 
         * What smaller version of the same problem is delegated to each child?
         * Generating the pre-order traversal list for each child's subtree (DepthFirstPreOrder<T>(node)).
         * 
         * What stops the recursion? 
         * Reaching a leaf node, which has no children to iterate over, leaving it to just return its list 
         * with just one element.
         * 
         * What result is returned or accumulated?
         * An IReadOnlyList<T> (which is a List<T>) containing all values in preorder (parent first, followed 
         * by each child subtree left-to-right).
         */
        public static IReadOnlyList<T> DepthFirstPreOrder<T>(HierarchyNode<T> root)
        {
            if (root == null) {
                throw new ArgumentNullException(nameof(root));
            }

            // TODO: return values in pre-order:
            // current node first, then each child subtree from left to right.

            List<T> preorder = new List<T>();
            preorder.Add(root.Value);

            foreach (HierarchyNode<T> node in root.Children) {
                List<T> childNodes = (List<T>)(DepthFirstPreOrder<T>(node));
                /*foreach (T value in childNodes) {
                    preorder.Add(value);
                }*/
                preorder.AddRange(childNodes);
            }

            return preorder;
        }

        /**
         * What work happens at the current node?
         * Checks if the current node is a leaf (returning 0), otherwise it tries to find the max child depth 
         * (including subtrees) and adds 1 for the connecting edge.
         * 
         * What smaller version of the same problem is delegated to each child?
         * Finding the maximum depth of each child's subtree (MaxDepth<T>(node)).
         * 
         * What stops the recursion? 
         * Reaching a leaf node (root.IsLeaf), hitting the base case of 0 edges.
         * 
         * What result is returned or accumulated?
         * An integer representing the maximum number of edges from the current node to its deepest descendant leaf.
         */
        public static int MaxDepth<T>(HierarchyNode<T> root)
        {
            if (root == null) {
                throw new ArgumentNullException(nameof(root));
            }

            // Depth is measured in edges from the root.
            // A single-node tree therefore has max depth 0.

            if (root.IsLeaf) {
                return 0;
            }

            int maxChildDepth = 0;

            foreach (HierarchyNode<T> node in root.Children) {
                int depth = MaxDepth<T>(node);

                if(depth > maxChildDepth) {
                    maxChildDepth = depth;
                }
            }

            return 1 + maxChildDepth;
        }
    }
}
