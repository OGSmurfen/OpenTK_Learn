using Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexturedTriangle
{
    public class Window : GameWindow
    {

        float[] verices =
        {   // Coordinates      Texture coords
            0f, 0.5f, 0f,       0.5f, 1f,   // top
            0.5f, -0.5f, 0f,    1f, 0f,     // bot right
            -0.5f, -0.5f, 0f,   0f, 0f      // bot left
        };

        string shadersPath;
        string texturesPath;

        private Shader shader;

        private int vbo;
        private int vao;

        private Texture texture;

        

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            shadersPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Shaders\");
            shadersPath = Path.GetFullPath(shadersPath);

            texturesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Textures\");
            texturesPath = Path.GetFullPath(texturesPath);

            shader = new Shader(shadersPath + @"\shader.vert", shadersPath + @"shader.frag");

            
        }



        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(110/255f, 133/255f, 118/255f, 1f);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, verices.Length * sizeof(float), verices, BufferUsageHint.StaticDraw);
            
            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            texture = Texture.LoadFromFile(texturesPath + @"wall.jpg");
            texture.Use(TextureUnit.Texture0);

            shader.Use();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.BindVertexArray(vao);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            SwapBuffers();

        }


    }
}
