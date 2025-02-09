using OpenTK.Windowing.Desktop;

namespace _08_RotatingCube
{
    public class Program
    {
        static void Main(string[] args)
        {
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = (1920, 1080),
                Title = "The Allspark"
            };

            using (Window gameWindow = new Window(GameWindowSettings.Default, nativeWindowSettings))
            {
                gameWindow.Run();
            }


        }
    }
}
