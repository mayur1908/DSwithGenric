using System;

interface INode<T> where T : IComparable<T>
{
    T Key { get; set; }
    INode<T> Left { get; set; }
    INode<T> Right { get; set; }
}

class MyBinaryNode<T> : INode<T> where T : IComparable<T>
{
    public T Key { get; set; }
    public INode<T> Left { get; set; }
    public INode<T> Right { get; set; }

    public MyBinaryNode(T key)
    {
        Key = key;
        Left = null;
        Right = null;
    }
}

class BinarySearchTree<T> where T : IComparable<T>
{
    private INode<T> root;

    public void Add(T key)
    {
        root = AddNode(root, key);
    }

    private INode<T> AddNode(INode<T> currentNode, T key)
    {
        if (currentNode == null)
        {
            return new MyBinaryNode<T>(key);
        }

        if (key.CompareTo(currentNode.Key) < 0)
        {
            currentNode.Left = AddNode(currentNode.Left, key);
        }
        else if (key.CompareTo(currentNode.Key) > 0)
        {
            currentNode.Right = AddNode(currentNode.Right, key);
        }

        return currentNode;
    }

    public void InOrderTraversal()
    {
        InOrderTraversal(root);
    }

    private void InOrderTraversal(INode<T> currentNode)
    {
        if (currentNode != null)
        {
            InOrderTraversal(currentNode.Left);
            Console.Write(currentNode.Key + " ");
            InOrderTraversal(currentNode.Right);
        }
    }
}

class Program
{
    static void Main()
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        bst.Add(56); // Adding root node with key 56
        bst.Add(30); // Adding node with key 30
        bst.Add(70); // Adding node with key 70

        bst.InOrderTraversal(); // Printing the BST in-order (sorted)

        // Output: 30 56 70
    }
}
