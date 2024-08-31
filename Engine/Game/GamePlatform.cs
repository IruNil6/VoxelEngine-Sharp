using EngineLibrary.DI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTK.Graphics.ES11;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Engine.Game
{
    public class GamePlatform : GameWindow
    {
        private float[] _vertices = [
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
            0.5f, -0.5f, 0.0f, //Bottom-right vertex
            0.0f,  0.5f, 0.0f  //Top vertex
        ];

        private readonly ILogger<GamePlatform> _logger;

        public GamePlatform(int width, int height, string title) : base(GameWindowSettings.Default,
            new NativeWindowSettings { ClientSize = (width, height), Title = title })
        {
            var scope = GlobalServiceProvider.ServiceProvider.CreateScope();

            _logger = scope.ServiceProvider.GetRequiredService<ILogger<GamePlatform>>();
        }

        public override void Run()
        {
            base.Run();

        }

        protected override void OnLoad()
        {
            base.OnLoad();

            _logger.LogInformation("Engine is started");

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyDown(Keys.Escape))
                Close();
        }
    }
}
