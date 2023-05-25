using System;
using System.Collections;
using System.Collections.Generic;

class MyMapNode<TKey, TValue>
{
    public TKey Key { get; }
    public TValue Value { get; set; }
    public MyMapNode<TKey, TValue> Next { get; set; }

    public MyMapNode(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}

class LinkedList<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    private MyMapNode<TKey, TValue> head;

    // Add a new key-value pair to the linked list
    public void Add(TKey key, TValue value)
    {
        var newNode = new MyMapNode<TKey, TValue>(key, value);

        // If the head is null, set the new node as the head
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            // Traverse to the end of the list and add the new node
            var current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
    }

    // Get the value associated with the given key
    public TValue GetValue(TKey key)
    {
        var current = head;
        while (current != null)
        {
            // Check if the current node's key matches the given key
            if (current.Key.Equals(key))
            {
                return current.Value; // Return the value if a match is found
            }
            current = current.Next;
        }
        return default(TValue); // Return the default value if no match is found
    }

    // Implement the IEnumerable interface to enable iteration over the key-value pairs
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        var current = head;
        while (current != null)
        {
            yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Program
{
    static void Main()
    {
        string paragraph = "Paranoids are not paranoid because they are paranoid but because they keep putting themselves deliberately into paranoid avoidable situations";

        // Create a linked list of linked lists to store the word frequency map
        var wordFrequencyMap = new LinkedList<int, LinkedList<string, int>>();

        string[] words = paragraph.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            int index = words[i].GetHashCode();  // Get the hash code of the word as the index

            // Retrieve the linked list corresponding to the index
            var linkedList = wordFrequencyMap.GetValue(index);
            if (linkedList == null)
            {
                // If the linked list does not exist, create a new one and add it to the outer linked list
                linkedList = new LinkedList<string, int>();
                wordFrequencyMap.Add(index, linkedList);
            }

            // Get the frequency of the current word in the inner linked list
            int frequency = linkedList.GetValue(words[i]);
            if (frequency != 0)
            {
                // If the word already exists, increment its frequency
                linkedList.Add(words[i], frequency + 1);
            }
            else
            {
                // If the word is encountered for the first time, add it with a frequency of 1
                linkedList.Add(words[i], 1);
            }
        }

        // Iterate over the word frequency map and print the index and word frequencies
        foreach (var kvp in wordFrequencyMap)
        {
            int index = kvp.Key;
            var linkedList = kvp.Value;

            Console.WriteLine("Index: " + index);

            foreach (var node in linkedList)
            {
                Console.WriteLine("Word: " + node.Key + ", Frequency: " + node.Value);
            }
        }
    }
}
