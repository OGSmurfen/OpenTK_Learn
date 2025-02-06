using Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;


namespace TexturedRectangle
{
    public class Window : GameWindow
    {


        float[] rectVertices =
        {   // rect coords      // texture coords
            -0.5f, 0.5f, 0f,    0, 1f,
            0.5f, 0.5f, 0f,     1f, 1f,
            0.5f, -0.5f, 0f,    1f, 0f,
            -0.5f, -0.5f, 0f,   0f, 0f
        };

        uint[] indices =
        {
            0, 1, 2,
            0, 2, 3
        };



        private int vertexBufferObj;
        private int vertexArrayObj;
        private int elementBufferObj;

        private Texture texture;
        private Shader shader;

        string texturePath;
        string shaderPath;


        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            shaderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Shaders\");
            texturePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Textures\");

            shaderPath = Path.GetFullPath(shaderPath);
            texturePath = Path.GetFullPath(texturePath);


        }


        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(245/255f, 78/255f, 66/255f, 1f);

            vertexBufferObj = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObj);
            GL.BufferData(BufferTarget.ArrayBuffer, rectVertices.Length * sizeof(float), rectVertices, BufferUsageHint.StaticDraw);

            vertexArrayObj = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObj);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            elementBufferObj = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObj);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);



            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            shader = new Shader(shaderPath + @"shader.vert", shaderPath + @"shader.frag");
            shader.Use();

            texture = Texture.LoadFromFile(texturePath + @"container.jpg");
            texture.Use(TextureUnit.Texture0);

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.BindVertexArray(vertexArrayObj);

            texture.Use(TextureUnit.Texture0);
            shader.Use();

            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }


    }
}
