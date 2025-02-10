

using Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace _08_RotatingCube
{
    public class Window : GameWindow
    {

        float[] cubeVerts =
        {   // rect coords         
            -0.5f, 0.5f, 0.5f,          0f, 1f, 
            0.5f, 0.5f, 0.5f,           1f, 1f,
            0.5f, -0.5f, 0.5f,          1f, 0f,
            -0.5f, -0.5f, 0.5f,         0f, 0f,
            -0.5f, 0.5f, -0.5f,         0f, 1f,
            0.5f, 0.5f, -0.5f,          1f, 1f,
            0.5f, -0.5f, -0.5f,         1f, 0f,
            -0.5f, -0.5f, -0.5f,        0f, 0f
        };  

        uint[] indices =
        {
            0, 1, 2, // front
            0, 2, 3, // front
            1, 5, 6, // right
            1, 6, 2, // right
            0, 4, 7, //
            0, 7, 3,
            3, 2, 6,
            3, 6, 7,
            4, 7, 6, 
            4, 5, 6,
            0, 4, 1,
            4, 5, 1
        };

        private int vertexBufferObject;
        private int vertexArrayObject;
        private int elementBufferObject;

        private Shader shader;
        string shadersPath;

        private Texture tex;
        string texturesPath;

        private Matrix4 viewMatrix; // this is the 'camera'
        private Matrix4 projectionMatrix; // how vertices will be projected
        private Matrix4 modelMatrix;

        private double time;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            shadersPath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Shaders\";
            shadersPath = Path.GetFullPath(shadersPath);

            texturesPath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Textures\";
            texturesPath = Path.GetFullPath(texturesPath);

            tex = Texture.LoadFromFile(texturesPath + @"container.jpg");
            tex.Use(TextureUnit.Texture0);
        }


        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Enable(EnableCap.DepthTest);

            GL.ClearColor(34/255f, 57/255f, 84/255f, 1f);


            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, cubeVerts.Length * sizeof(float), cubeVerts, BufferUsageHint.StaticDraw);

            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(float), indices, BufferUsageHint.StaticDraw);

            viewMatrix = Matrix4.CreateTranslation(0f, 0f, -3f);
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Size.X / Size.Y, 0.1f, 100f);

            shader = new Shader(shadersPath + @"shader.vert", shadersPath + @"shader.frag");
            shader.Use();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            double rotationSpeed = 20d;

            time += rotationSpeed * args.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            modelMatrix = Matrix4.Identity * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(time))
                * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(20));

            shader.SetMatrix4("view", viewMatrix);
            shader.SetMatrix4("projection", projectionMatrix);
            shader.SetMatrix4("model", modelMatrix);

            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }


    }
}
