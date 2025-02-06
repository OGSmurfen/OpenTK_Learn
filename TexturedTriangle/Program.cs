using OpenTK.Windowing.Desktop;

namespace TexturedTriangle
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings() { ClientSize = (1920, 1080), Title = "Textured Triangle"};

            using (Window gameWindow = new Window(GameWindowSettings.Default, nativeWindowSettings))
            {
                gameWindow.Run();
            }

        }
    }
}
