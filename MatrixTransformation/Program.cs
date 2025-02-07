

using OpenTK.Windowing.Desktop;

namespace MatrixTransformation
{
    public class Program
    {
        static void Main(string[] args)
        {

            NativeWindowSettings windowSettings = new NativeWindowSettings()
            {
                ClientSize = (1980, 1080),
                Title = "Matrix Transformations"
            };

            using (Window w = new Window(GameWindowSettings.Default, windowSettings))
            {
                w.Run();
            }
        }
    }
}
