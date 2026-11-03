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
