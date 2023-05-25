using System;

class Node<T>
{
    public T Data { get; set; }
    public Node<T> Left { get; set; }
    public Node<T> Right { get; set; }

    public Node(T data)
    {
        Data = data;
        Left = null;
        Right = null;
    }
}

class BinaryTree<T> where T : IComparable<T>
{
    public Node<T> Root { get; private set; }

    public BinaryTree()
    {
        Root = null;
    }

    public void Add(T data)
    {
        Root = AddNode(Root, data);
    }

    private Node<T> AddNode(Node<T> currentNode, T data)
    {
        if (currentNode == null)
        {
            return new Node<T>(data);
        }

        if (currentNode.Left == null)
        {
            currentNode.Left = AddNode(currentNode.Left, data);
        }
        else if (currentNode.Right == null)
        {
            currentNode.Right = AddNode(currentNode.Right, data);
        }
        else
        {
            // Both left and right children are filled, so we recursively traverse down the tree
            // using a level-order (breadth-first) traversal until we find a spot to insert the node
            if (currentNode.Left != null)
            {
                currentNode.Left = AddNode(currentNode.Left, data);
            }
            else
            {
                currentNode.Right = AddNode(currentNode.Right, data);
            }
        }

        return currentNode;
    }

    public bool Search(T value)
    {
        return SearchNode(Root, value);
    }

    private bool SearchNode(Node<T> currentNode, T value)
    {
        if (currentNode == null)
        {
            return false;
        }

        if (currentNode.Data.Equals(value))
        {
            return true;
        }

        bool leftSearch = SearchNode(currentNode.Left, value);
        if (leftSearch)
        {
            return true;
        }

        bool rightSearch = SearchNode(currentNode.Right, value);
        if (rightSearch)
        {
            return true;
        }

        return false;
    }
}

class Program
{
    static void Main()
    {
        BinaryTree<int> binaryTree = new BinaryTree<int>();

        // Adding nodes to the binary tree
        binaryTree.Add(1);
        binaryTree.Add(2);
        binaryTree.Add(3);
        binaryTree.Add(4);
        binaryTree.Add(5);
        binaryTree.Add(6);
        binaryTree.Add(7);

        int valueToSearch = 3;
        bool found = binaryTree.Search(valueToSearch);
        Console.WriteLine("Search for " + valueToSearch + ": " + found);

        // Output: Search for 63: False
    }
}
