using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using System.Linq;

namespace OpenGLGameCS.Game
{
    public class GameEngine : GameWindow
    {
        public bool IsRunning { get; private set; }
        public GameEngine(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        { 
        }
        public bool Initialise() 
        {
            IsRunning = false;
            if (InitialiseOpenGL())
            {
                Console.WriteLine("Successfully initialised game Engine");
                return true;
            }
            else {
                Console.WriteLine("Failed to initialise game engine");
                return false;
            }
        }
        public bool InitialiseOpenGL()
        {
            GLFWBindingsContext binding = new GLFWBindingsContext();
            GL.LoadBindings(binding);

            if (GLFW.Init()) {
                Console.WriteLine("Successfully initialised GLFW and OpenGL");
                return true;
            }
            else
            {
                Console.WriteLine("Failed to initialise GLFW and OpenGL");
                return false;
            }
            
        }
        public void RunGameLoop()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                Console.WriteLine("Starting GAme Loop");
                base.Run();
            }
        }
        float[] vertices = new float[]
        {
             -0.4f, 0.2f, 0.0f,
              0.0f, 0.0f, 0.0f,

              0.8f, 0.0f, 0.0f,
              0.4f, 0.2f, 0.0f,

              -0.4f, 0.2f, 0.0f,
              0.4f, 0.2f, 0.0f,

              0.0f, 0.0f, 0.0f,
              0.8f, 0.0f, 0.0f,

              -0.4f,-0.4f,0.0f,
              -0.4f, 0.8f,0.0f,

              0.4f,-0.4f,0.0f,
              0.4f,0.8f,0.0f,

              0.0f,0.0f,0.0f,
              0.0f,-0.6f,0.0f,

              0.8f,0.0f,0.0f,
              0.8f,-0.6f,0.0f,

              -0.4f,0.8f,0.0f,
              0.4f, 0.8f,0.0f,

              -0.4f,0.6f,0.0f,
              0.4f,0.6f,0.0f

               
        };

        int vao;
        int vbo;

        protected override void OnLoad()
        {
            base.OnLoad();

            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            vbo = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                vertices.Length * sizeof(float),
                vertices.ToArray(),
                BufferUsageHint.StaticDraw
                );
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, true, 0, 0);
        }
        protected override void OnUnload()
        {
            base.OnUnload();
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            GameTime.Delta = Single.Parse(args.Time.ToString());
        }
        
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.BindVertexArray(vao);
            GL.DrawArrays(BeginMode.Lines, 0, vertices.Length);
            Context.SwapBuffers();
        }
    }
}
