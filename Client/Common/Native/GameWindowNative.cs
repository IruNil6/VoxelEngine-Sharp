using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Client.Common.Native
{
    public class GameWindowNative(int width, int height, WindowState windowState)
                : GameWindow(GameWindowSettings.Default, new NativeWindowSettings
                {
                    ClientSize = (width, height),
                    Title = "VoxelEngine-Sharp",
                    WindowState = windowState
                })
    {
    }
}
