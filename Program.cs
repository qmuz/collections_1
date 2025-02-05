using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Employee(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}";
    }
}

public class Department : Employee
{
    public int EmployeeId { get; set; }
    public Department(int id, string name, int employeeId) : base(id, name)
    {
        EmployeeId = employeeId;
    }
    public override string ToString()
    {
        return $"{base.ToString()}, EmployeeId: {EmployeeId}";
    }
}

public class MyDictionary<K, V>
{
    private List<Pair> Pairs;

    public MyDictionary()
    {
        Pairs = new List<Pair>();
    }
    public void Add(K key, V value)
    {
        foreach (var pair in Pairs)
        {
            if (pair.Key.Equals(key))
            {
                pair.Key = key;
                pair.Value = value;
                return;
            }
        }
        Pairs.Add(new Pair(key, value));
    }
    public void Remove(K key)
    {
        foreach (var pair in Pairs)
        {
            if (pair.Key.Equals(key))
            {
                Pairs.Remove(pair);
                return;
            }
        }
    }
    public V GetValue(K key)
    {
        foreach (var pair in Pairs)
        {
            if (pair.Key.Equals(key))
            {
                return pair.Value;
            }
        }
        throw new EntryPointNotFoundException("Not found");
    }
    public bool ContainsKey(K key)
    {
        foreach (var pair in Pairs)
        {
            if (pair.Key.Equals(key))
            {
                return true;
            }
        }
        return false;
    }
    public List<K> GetKeys()
    {
        List<K> keys = new List<K>();
        foreach (var pair in Pairs)
        {
            keys.Add(pair.Key);
        }
        return keys;
    }
    public override string ToString()
    {
        string result = "";
        foreach (var pair in Pairs)
        {
            result += $"{pair.ToString()}\n";
        }
        return result;
    }
    public class Pair
    {
        public K Key;
        public V Value;
        public Pair(K key, V value)
        {
            this.Key = key;
            this.Value = value;
        }
        public override string ToString()
        {
            return $"{Key} = {Value}";
        }
    }
}

public class Graph<T>
{
    public MyDictionary<T, List<Edge>> AdjacencyList { get; set; }
    public class Edge
    {
        public T Target { get; set; }
        public int Weight { get; set; }
        public Edge(T target, int weight)
        {
            Target = target;
            Weight = weight;
        }
        public override string ToString()
        {
            return $"{Target} (weight: {Weight})";
        }
    }
    public Graph()
    {
        AdjacencyList = new MyDictionary<T, List<Edge>>();
    }
    private void AddVertex(T vertex)
    {
        if (!AdjacencyList.ContainsKey(vertex))
        {
            AdjacencyList.Add(vertex, new List<Edge>());
        }
    }
    public void AddEdge(T source, T target, int weight)
    {
        AddVertex(source);
        AddVertex(target);
        AdjacencyList.GetValue(source).Add(new Edge(target, weight));
        AdjacencyList.GetValue(target).Add(new Edge(source, weight));
    }
    public void PrintGraph()
    {
        foreach (var vertex in AdjacencyList.GetKeys())
        {
            foreach (var edge in AdjacencyList.GetValue(vertex))
            {
                Console.WriteLine($"{vertex} -> {edge.Target} => weight: {edge.Weight}");
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Graph<Employee> graph = new Graph<Employee>();
        Department department1 = new Department(1, "Alex", 2);
        Department department2 = new Department(2, "Bob", 1);

        graph.AddEdge(department1, department2, 10);
        graph.PrintGraph();
    }
}
