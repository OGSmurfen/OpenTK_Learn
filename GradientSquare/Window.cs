using Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradientSquare
{
    public class Window : GameWindow
    {

        private readonly float[] vertices =
        {   // positions        // colors
             -0.5f, 0.5f, 0f,   1.0f, 0.0f, 0.0f,
             0.5f, 0.5f, 0f,    0.0f, 1.0f, 0.0f,
             0.5f, -0.5f, 0f,   0.0f, 0.0f, 1.0f,
             -0.5f, -0.5f, 0f,  1.0f, 1.0f, 0.0f
        };


        uint[] indices =
        {
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
        };

        private int _vertexBufferObject;

        private int _vertexArrayObject;

        private int _elementBufferObject;

        private Shader _shader;

        private string shadersPath;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            shadersPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Shaders\");
            shadersPath = Path.GetFullPath(shadersPath);
            _shader = new Shader(shadersPath + @"\shader.vert", shadersPath + @"\shader.frag");
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(96 / 255f, 94 / 255f, 125 / 255f, 1f);


            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(float), indices, BufferUsageHint.StaticDraw);


            _shader.Use();

        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.BindVertexArray(_vertexArrayObject);

            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }




    }
}
