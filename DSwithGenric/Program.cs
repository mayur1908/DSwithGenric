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

    public void Add(TKey key, TValue value)
    {
        var newNode = new MyMapNode<TKey, TValue>(key, value);

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            var current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
    }

    public TValue GetValue(TKey key)
    {
        var current = head;
        while (current != null)
        {
            if (current.Key.Equals(key))
            {
                return current.Value;
            }
            current = current.Next;
        }
        return default(TValue);
    }

    public void Remove(TKey key)
    {
        if (head == null)
            return;

        if (head.Key.Equals(key))
        {
            head = head.Next;
            return;
        }

        var current = head;
        while (current.Next != null)
        {
            if (current.Next.Key.Equals(key))
            {
                current.Next = current.Next.Next;
                return;
            }
            current = current.Next;
        }
    }

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

        var wordFrequencyMap = new LinkedList<int, LinkedList<string, int>>();

        string[] words = paragraph.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            int index = words[i].GetHashCode();  // Get the hash code of the word as the index

            var linkedList = wordFrequencyMap.GetValue(index);
            if (linkedList == null)
            {
                linkedList = new LinkedList<string, int>();
                wordFrequencyMap.Add(index, linkedList);
            }

            int frequency = linkedList.GetValue(words[i]);
            if (frequency != 0)
            {
                linkedList.Add(words[i], frequency + 1);
            }
            else
            {
                linkedList.Add(words[i], 1);
            }
        }

        // Remove the word "avoidable" from the word frequency map
        int avoidableIndex = "avoidable".GetHashCode();
        var avoidableList = wordFrequencyMap.GetValue(avoidableIndex);
        if (avoidableList != null)
        {
            avoidableList.Remove("avoidable");
        }

        // Print the updated word frequency map
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
