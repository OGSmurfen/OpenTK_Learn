using OpenTK.Windowing.Desktop;

namespace _07_2Dplane_in_3Dspace
{
    public class Program
    {
        static void Main(string[] args)
        {
            NativeWindowSettings nws = new NativeWindowSettings()
            {
                ClientSize = (1920, 1080),
                Title = "Plane"
            };

            using (Window window = new Window(GameWindowSettings.Default, nws))
            {
                window.Run();
            }


        }
    }
}
