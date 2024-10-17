namespace MSO_P2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TXTStrategy txt = new TXTStrategy("C:\\Users\\Gebruiker\\OneDrive\\reposSchool\\year2\\MSO-P2-v2\\testv1MSO.txt");
            txt.ReadFile();
            Console.WriteLine("Hello, World!");
        }
    }
}
