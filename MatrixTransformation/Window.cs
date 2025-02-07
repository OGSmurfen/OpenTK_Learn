using Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;


namespace MatrixTransformation
{
    public class Window : GameWindow
    {


        float[] rectVertices =
        {   // rect coords          // tex coords
            -0.5f, 0.5f, 1f,        0f, 1f,
            0.5f, 0.5f, 1f,         1f, 1f,
            0.5f, -0.5f, 1f,        1f, 0f,
            -0.5f, -0.5f, 1f,       0f, 0f
        };

        uint[] indices =
        {
            0, 1, 2,
            0, 2, 3
        };

        private int vbo;
        private int vao;
        private int ebo;

        private Shader shader;
        private Texture texture1;
        private Texture texture2;

        string shadersPath;
        string texturePath;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            shadersPath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Shaders\";
            shadersPath = Path.GetFullPath(shadersPath);

            shader = new Shader(shadersPath + @"shader.vert", shadersPath + @"shader.frag");


            texturePath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Textures\";
            texturePath = Path.GetFullPath(texturePath);

            texture1 = Texture.LoadFromFile(texturePath + @"container.jpg");
            texture2 = Texture.LoadFromFile(texturePath + @"awesomeface.png");
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(138/255f, 56/255f, 102/255f, 1f);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, rectVertices.Length * sizeof(float), rectVertices, BufferUsageHint.StaticDraw);

            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            // specify texture coords:
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
            


            shader.Use();
            texture1.Use(TextureUnit.Texture0);

            shader.SetInt("tex0", 1);

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }
    }
}
