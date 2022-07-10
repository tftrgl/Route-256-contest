?Trie dictionary = new Trie();
int n = Int32.Parse(Console.ReadLine());
for (int i = 0; i < n; i++)
{
    dictionary.insert(Console.ReadLine());
}

int q = Int32.Parse(Console.ReadLine());
string[] responces = new string[q];
for (int i = 0; i < q; i++)
{
    string request = Console.ReadLine();
    responces[i] = dictionary.searchRhyme(request);
}

foreach (string responce in responces)
    Console.WriteLine(responce);

class Trie
{
    const int letterCount = 26;
    class LetterNode
    {
        public LetterNode[] children = new LetterNode[letterCount];
        public bool terminalNode;

        public LetterNode()
        {
            terminalNode = false;
            for (int i = 0; i < letterCount; i++)
                children[i] = null;
        }
    }

    LetterNode root = new LetterNode();

    public void insert(String key)
    {
        int level;
        int length = key.Length;
        int index;


        LetterNode pCrawl = root;

        for (level = length - 1; level >= 0; level--)
        {
            index = key[level] - 'a';
            if (pCrawl.children[index] == null)
                pCrawl.children[index] = new LetterNode();

            pCrawl = pCrawl.children[index];
        }

        // mark last node as leaf
        pCrawl.terminalNode = true;
    }

    public string searchRhyme(String key)
    {
        int level;
        int length = key.Length;
        int index;


        LetterNode pCrawl = root;
        List<LetterNode> previousNodes = new List<LetterNode>();
        List<char> letterList = new List<char>();

        for (level = length-1; level >= 0; level--)
        {
            index = key[level] - 'a';
            if (pCrawl.children[index] == null)
            {
                if (pCrawl.terminalNode)
                {
                    char[] res = letterList.ToArray();
                    Array.Reverse(res);
                    return new string(res);
                }
                break;
            }
            else
            {
                letterList.Add(key[level]);
            }

            previousNodes.Add(pCrawl);
            pCrawl = pCrawl.children[index];
        }

        while (true)
        {
            for (int i = 0; i < letterCount; i++)
            {
                if (pCrawl.children[i] != null)
                {
                    letterList.Add((char)(i + 'a'));
                    previousNodes.Add(pCrawl);
                    pCrawl = pCrawl.children[i];
                    break;
                }
            }
            if (pCrawl.terminalNode)
                break;
        }



        char[] result = letterList.ToArray();
        Array.Reverse(result);
        string rhymedWord = new string(result);

        if (rhymedWord == key)
        {
            Array.Reverse(result);
            remove(root, new string(result), 0);
            rhymedWord = searchRhyme(key);
            insert(key);
        }

        return rhymedWord;
    }

    bool isEmpty(LetterNode root)
    {
        for (int i = 0; i < letterCount; i++)
            if (root.children[i] != null)
                return false;
        return true;
    }

    LetterNode remove(LetterNode root, String key, int depth)
    {
        // If tree is empty
        if (root == null)
            return null;

        // If last character of key is being processed
        if (depth == key.Length)
        {

            // This node is no more end of word after
            // removal of given key
            if (root.terminalNode)
                root.terminalNode = false;

            // If given is not prefix of any other word
            if (isEmpty(root))
            {
                root = null;
            }

            return root;
        }

        // If not last character, recur for the child
        // obtained using ASCII value
        int index = key[depth] - 'a';
        root.children[index] =
            remove(root.children[index], key, depth + 1);

        // If root does not have any child (its only child got
        // deleted), and it is not end of another word.
        if (isEmpty(root) && root.terminalNode == false)
        {
            root = null;
        }

        return root;
    }
}