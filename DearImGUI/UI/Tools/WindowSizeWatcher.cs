using System.Numerics;
using ImGuiNET;


namespace DearImGUI.Tools;


public class WindowSizeWatcher(Vector2 __size)
{
    public Vector2 Size = __size;

    public bool SizeChanged() {
        Vector2 NewSize = ImGui.GetWindowSize();

        if (NewSize == Size) return false;
        
        Size = NewSize;
        return true;
    }
}