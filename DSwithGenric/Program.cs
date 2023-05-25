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

class BinaryTree<T>
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

    public int Size()
    {
        return GetSize(Root);
    }

    private int GetSize(Node<T> currentNode)
    {
        if (currentNode == null)
        {
            return 0;
        }

        int leftSize = GetSize(currentNode.Left);
        int rightSize = GetSize(currentNode.Right);

        return 1 + leftSize + rightSize;
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
        binaryTree.Add(10);

        int size = binaryTree.Size();
        Console.WriteLine("Size of the binary tree: " + size);

        // Output: Size of the binary tree: 7
    }
}
