using System.Numerics;
using ImGuiNET;


namespace DearImGUI;


public class PlayButton : IFreeElement
{

    public void Define() {
        
        ImGui.Begin("PlayMenu", ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove);
        
        ImGui.SetWindowPos(Radium.ScreenPercentToRealPos(0, 3));
        ImGui.SetWindowSize(Radium.ScreenPercentToRealPos(65, 2f));
        
        ImGui.SetCursorPosX(Radium.ScreenPercentToRealX(29.7f));
        ImGui.SetCursorPosY(Radium.ScreenPercentToRealY(.5f));
        ImGui.Button("Play", Radium.ScreenPercentToRealPos(5, 2));
        
        ImGui.End();
    }
}