

using Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace _07_2Dplane_in_3Dspace
{
    public class Window : GameWindow
    {
        // the projection going back and forth has to do with the Z coords of the rect.
        // when Z is set to 1 the pivot points are off making it not spin through its center.
        // to make it spin in place make the Zs 0
        float[] rectVerts =
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
        string texturesPath;

        private double time;
        private Matrix4 view;
        private Matrix4 projection;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            shadersPath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Shaders\";
            shadersPath = Path.GetFullPath(shadersPath);
            shader = new Shader(shadersPath + @"shader.vert", shadersPath + @"shader.frag");

            texturesPath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Textures\";
            texturesPath = Path.GetFullPath(texturesPath);

            texture1 = Texture.LoadFromFile(texturesPath + @"container.jpg");
            texture2 = Texture.LoadFromFile(texturesPath + @"awesomeface.png");
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Enable(EnableCap.DepthTest);

            GL.ClearColor(81/255f, 160/255f, 194/255f, 1f);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, rectVerts.Length * sizeof(float), rectVerts, BufferUsageHint.StaticDraw);

            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);
            int apos = shader.GetAttribLocation("aPos");
            GL.VertexAttribPointer(apos, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(apos);

            int texpos = shader.GetAttribLocation("texPos");
            GL.VertexAttribPointer(texpos, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(texpos);

            ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(float), indices, BufferUsageHint.StaticDraw);


            texture1.Use(TextureUnit.Texture0);
            texture2.Use(TextureUnit.Texture1);
            shader.Use();

            shader.SetInt("tex0", 0);
            shader.SetInt("tex1", 1);

            view = Matrix4.CreateTranslation(0f, 0f, -3.0f);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Size.X / (float)Size.Y, 0.1f, 100f);

            
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            float animationSpeed = 20.0f;
            time += animationSpeed * args.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 model = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(time));

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("projection", projection);
            // the plane also appears to go back-and-forth. this has to do with the rectVerts! go up top to see

            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            

            SwapBuffers();
        }



    }
}
