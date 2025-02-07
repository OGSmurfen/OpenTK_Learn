using OpenTK.Windowing.Desktop;

namespace MultipleTextures
{
    public class Program
    {
        static void Main(string[] args)
        {

            NativeWindowSettings nws = new NativeWindowSettings()
            {
                ClientSize = (1920, 1080),
                Title = "Multiple Textures"
            };


            using (Window gameWindow = new Window(GameWindowSettings.Default, nws))
            {
                gameWindow.Run();
            }



        }
    }
}
