using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSwithGenric
{
    class MyMapNode<TKey, TValue>
    {
        public TKey Key { get; }  // Key property to store the key of the key-value pair
        public TValue Value { get; set; }  // Value property to store the value of the key-value pair
        public MyMapNode<TKey, TValue> Next { get; set; }  // Next property to reference the next node in the linked list

        public MyMapNode(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    class LinkedList<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private MyMapNode<TKey, TValue> head;  // The head of the linked list

        public void Add(TKey key, TValue value)
        {
            var newNode = new MyMapNode<TKey, TValue>(key, value);  // Create a new node with the provided key and value

            if (head == null)
            {
                head = newNode;  // If the linked list is empty, assign the new node as the head
            }
            else
            {
                var current = head;
                while (current.Next != null)
                {
                    current = current.Next;  // Traverse the linked list until the last node is reached
                }
                current.Next = newNode;  // Assign the new node as the next node of the last node
            }
        }

        public TValue GetValue(TKey key)
        {
            var current = head;
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    return current.Value;  // Traverse the linked list and return the value associated with the provided key if found
                }
                current = current.Next;  // Move to the next node
            }
            return default(TValue);  // If the key is not found, return the default value for TValue
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            var current = head;
            while (current != null)
            {
                yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);  // Use yield return to iterate over the linked list and return each key-value pair
                current = current.Next;  // Move to the next node
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
            string sentence = "To be or not to be";

            var wordFrequencyMap = new LinkedList<string, int>();

            string[] words = sentence.Split(' ');

            foreach (string word in words)
            {
                int frequency = wordFrequencyMap.GetValue(word);
                if (frequency != 0)
                {
                    wordFrequencyMap.Add(word, frequency + 1);  // Increment the frequency if the word is already present in the linked list
                }
                else
                {
                    wordFrequencyMap.Add(word, 1);  // Add the word with frequency 1 to the linked list if it doesn't exist
                }
            }

            foreach (var node in wordFrequencyMap)
            {
                Console.WriteLine("Word: " + node.Key + ", Frequency: " + node.Value);  // Print each key-value pair in the linked list
            }
        }
    }
}
