using Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;


namespace MultipleTextures
{
    public class Window : GameWindow
    {

        private int vertexBufferObject;
        private int vertexArrayObject;
        private int elementBufferObject;

        private Shader shader;
        private Texture texture;
        private Texture texture2;

        string shadersPath;
        string texturesPath;

        float[] vertices =
        {   // vertex coords        // texture coords
            -0.5f, 0.5f, 1f,        0f, 1f,         // top left
            0.5f, 0.5f, 1f,         1f, 1f,         // top right
            0.5f, -0.5f, 1f,        1f, 0f,         // bot right
            -0.5f, -0.5f, 1f,       0f, 0f          // bot left
        };

        uint[] indices =
        {
            0, 1, 2,        // triangle 1
            0, 2, 3         // triangle 2
        };


        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            shadersPath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Shaders\";
            shadersPath = Path.GetFullPath(shadersPath);
            Console.WriteLine(shadersPath);

            texturesPath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Textures\";
            texturesPath = Path.GetFullPath(texturesPath);
            Console.WriteLine(texturesPath);
        }


        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(26/255f, 92/255f, 53/255f, 1);

            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);



            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            texture = Texture.LoadFromFile(texturesPath + @"container.jpg");
            texture2 = Texture.LoadFromFile(texturesPath + @"awesomeface.png");

            texture.Use(TextureUnit.Texture0);
            texture2.Use(TextureUnit.Texture1);

           
            shader = new Shader(shadersPath + @"shader.vert", shadersPath + @"shader.frag");
            shader.Use();

            shader.SetInt("tex0", 0);
            shader.SetInt("tex1", 1);


        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.BindVertexArray(vertexArrayObject);

            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();

        }


    }
}
