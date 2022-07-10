Dictionary<string, DependencyNode> dependencyMap = 
    new Dictionary<string, DependencyNode>();
string? line = "";
int N = 0;

int requestsCount = Int32.Parse(Console.ReadLine());
string[][] responceList = new string[requestsCount][];
for (int q = 0; q < requestsCount; q++)
{
    Console.ReadLine();
    dependencyMap.Clear();

    N = Int32.Parse(Console.ReadLine());
    for (int i = 0; i < N; i++)
    {
        line = Console.ReadLine();
        if (line != null)
        {
            string[] parts = line.Split(':');
            string name = parts[0];
            string[] dependencies = parts[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            dependencyMap.Add(name, new DependencyNode(dependencies, name));
        }
    }


    N = Int32.Parse(Console.ReadLine());
    string loadingSequence;
    string[] responces = new string[N];
    for (int i = 0; i < N; i++)
    {
        loadingSequence = "";
        line = Console.ReadLine();
        if (line != null)
        {
            dependencyMap.TryGetValue(line, out DependencyNode? dependency);
            if (dependency != null)
            {
                if (!dependency.IsLoaded)
                {
                    loadingSequence = loadDependency(dependency) + " " + loadingSequence;
                    loadingSequence = loadingSequence.Trim();
                }
                int count = loadingSequence.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
                loadingSequence = count + " " + loadingSequence;
                loadingSequence = loadingSequence.Trim();
                responces[i] = loadingSequence;
            }
            else
            {
                Console.WriteLine($"Dependency {line} not found");
            }
        }
    }
    responceList[q] = responces;
}

foreach (var responces in responceList)
{
    foreach (var responce in responces)
    {
        Console.WriteLine(responce);
    }
    Console.WriteLine();
}


string loadDependency(DependencyNode dep)
{
    string loadSequence = "";
    foreach (var dependencyName in dep.Dependencies)
    {
        dependencyMap.TryGetValue(dependencyName, out DependencyNode? dependency);
        if (dependency != null)
        {
            if (!dependency.IsLoaded)
                loadSequence += loadDependency(dependency) + " ";
        }
        else
        {
            return "<ERROR>";
        }
    }
    dep.IsLoaded = true;
    loadSequence += dep.Name;
    return loadSequence;
}


internal class DependencyNode
{
    public string[] Dependencies { get; }
    public string Name { get; set; }
    public bool IsLoaded { get; set; }

    public DependencyNode(string[] dependencies, string name)
    {
        Dependencies = new string[dependencies.Length];
        dependencies.CopyTo(Dependencies, 0);
        Name = name;
        IsLoaded = false;
    }
}