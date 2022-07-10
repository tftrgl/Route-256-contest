?int t = Int32.Parse(Console.ReadLine());
string[] responces = new string[t];

for (int q = 0; q < t; q++)
{

    int[] data = Console.ReadLine().Split(' ')
        .Select(str => Int32.Parse(str)).ToArray();

    char[][] map = new char[data[0]][];
    for (int i = 0; i < data[0]; i++)
    {
        map[i] = Console.ReadLine().ToArray();
    }

    HashSet<char> usedColors = new HashSet<char>();

    bool correct = true;
    for (int i = 0; i < map.Length; i++)
    {
        for (int j = 0; j < map[i].Length; j++)
        {
            if (map[i][j] == '.' || map[i][j] == '0')
                continue;

            if (!usedColors.Add(map[i][j]))
            {
                correct = false;
                break;
            }
            trace(map, i, j, map[i][j]);
        }
        if (!correct)
            break;
    }

    if (correct)
        responces[q] = "YES";
    else
        responces[q] = "NO";
}

foreach (var responce in responces)
    Console.WriteLine(responce);

void trace(char[][] map, int i, int j, char c)
{
    if (i < 0 || i >= map.Length ||
        j < 0 || j >= map[i].Length ||
        map[i][j] != c)
        return;

    map[i][j] = '0';

    trace(map, i, j + 2, c);
    trace(map, i, j - 2, c);
    trace(map, i - 1, j + 1, c);
    trace(map, i - 1, j - 1, c);
    trace(map, i + 1, j + 1, c);
    trace(map, i + 1, j - 1, c);
}