namespace Triangle
{
    public class Program
    {


        static void Main(string[] args)
        {

            using (Game game = new Game(1920, 1080, "Triangle"))
            {
                game.Run();
            }


        }
    }
}
