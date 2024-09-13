using OpenTK.Windowing.Common;

namespace EngineLibrary.Infrastructure.Options
{
    public class ClientOptions
    {
        public int Width { get; set; } = 1280;
        public int Height { get; set; } = 720;

        public WindowState WindowState { get; set; } = WindowState.Normal;
    }
}
