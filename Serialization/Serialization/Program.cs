using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Serialization
{
    class Program
    {
        static Random rand = new Random();
        static ListNode addNode(ListNode previous)
        {
            ListNode newNode = new ListNode();
            newNode.Previous = previous;
            newNode.Next = null;
            newNode.Data = rand.Next(0, 100).ToString();
            previous.Next = newNode;
            return newNode;
        }

        static ListNode randomNode(ListNode _head, int _length)
        {
            int k = rand.Next(0, _length);
            int i = 0;
            ListNode currentNode = _head;
            while (i < k)
            {
                currentNode = currentNode.Next;
                i++;
            }
            return currentNode;
        }

        static void Main(string[] args)
        {
            int length = 7;

            ListNode head = new ListNode();
            ListNode tail = new ListNode();
            ListNode currentNode = new ListNode();

            head.Data = rand.Next(0, 100).ToString();

            tail = head;

            for (int i = 1; i < length; i++)
                tail = addNode(tail);

            currentNode = head;

            for (int i = 0; i < length; i++)
            {
                currentNode.Random = randomNode(head, length);
                currentNode = currentNode.Next;
            }

            ListRandom first = new ListRandom();
            first.Head = head;
            first.Tail = tail;
            first.Count = length;

            FileStream fs = new FileStream("C:/Users/Диана/Downloads/list.dat", FileMode.OpenOrCreate);
            first.Serialize(fs);

            ListRandom second = new ListRandom();
            try
            {
                fs = new FileStream("C:/Users/Диана/Downloads/list.dat", FileMode.Open);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Enter to exit.");
                Console.Read();
                Environment.Exit(0);
            }
            second.Deserialize(fs);

            if (second.Tail.Data == first.Tail.Data) Console.WriteLine("Success");
            Console.Read();

        }
        class ListNode
        {
            public ListNode Previous;
            public ListNode Next;
            public ListNode Random;
            public string Data;
        }
        class ListRandom
        {
            public ListNode Head;
            public ListNode Tail;
            public int Count;

            private ListNode GetNodeAt(int index)
            {
                int counter = 0;
                for (ListNode currentNode = Head; currentNode.Next != null; currentNode = currentNode.Next)
                {
                    if (counter == index)
                        return currentNode;
                    counter++;
                }
                return new ListNode();
            }


            public void Serialize(Stream s)
            {
                Dictionary<ListNode, int> dictionary = new Dictionary<ListNode, int>();
                int id = 0;
                for (ListNode currentNode = Head; currentNode != null; currentNode = currentNode.Next)
                {
                    dictionary.Add(currentNode, id);
                    id++;
                }
                using (BinaryWriter writer = new BinaryWriter(s))
                {
                    for (ListNode currentNode = Head; currentNode != null; currentNode = currentNode.Next)
                    {
                        writer.Write(currentNode.Data);
                        writer.Write(dictionary[currentNode.Random]);
                    }
                }
                Console.WriteLine("List serialized");

            }

            public void Deserialize(Stream s)
            {
                Dictionary<int, Tuple<String, int>> dictionary = new Dictionary<int, Tuple<String, int>>();
                int counter = 0;
                using (BinaryReader reader = new BinaryReader(s))
                {
                    while (reader.PeekChar() != -1)
                    {
                        String data = reader.ReadString();
                        int randomId = reader.ReadInt32();
                        dictionary.Add(counter, new Tuple<String, int>(data, randomId));
                        counter++;
                    }
                    Console.WriteLine("File readed");
                }
                Count = counter;
                Head = new ListNode();
                ListNode current = Head;
                for (int i = 0; i < Count; i++)
                {
                    current.Data = dictionary.ElementAt(i).Value.Item1;
                    current.Next = new ListNode();
                    if (i != this.Count - 1)
                    {
                        current.Next.Previous = current;
                        current = current.Next;
                    }
                    else
                    {
                        Tail = current;
                    }

                }
                counter = 0;
                for (ListNode currentNode = Head; currentNode.Next != null; currentNode = currentNode.Next)
                {
                    currentNode.Random = GetNodeAt(dictionary.ElementAt(counter).Value.Item2);
                    counter++;
                }
                Console.WriteLine("List deserialized");
            }
        }
    }
}