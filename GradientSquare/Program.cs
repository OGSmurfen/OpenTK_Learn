using OpenTK.Windowing.Desktop;

namespace GradientSquare
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new OpenTK.Mathematics.Vector2i(1920, 1090),
                Title = "Gradient Rectangle",
                Flags = OpenTK.Windowing.Common.ContextFlags.ForwardCompatible // for macOS
            };


            using (Window window = new Window(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.Run();
            }


        }
    }
}
