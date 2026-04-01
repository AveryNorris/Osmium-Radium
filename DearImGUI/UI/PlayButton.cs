using System.Numerics;
using ImGuiNET;
using OsmiumNucleus;


namespace DearImGUI;


public class PlayButton : IFreeElement
{

    public static bool started = false;

    public void Define() {
        
        ImGui.Begin("PlayMenu", ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove);
        
        ImGui.SetWindowPos(Radium.ScreenPercentToRealPos(0, 3));
        ImGui.SetWindowSize(Radium.ScreenPercentToRealPos(65, 2f));
        
        ImGui.SetCursorPosY(Radium.ScreenPercentToRealY(.5f));
        if (ImGui.Button("Build", Radium.ScreenPercentToRealPos(0, 2))) {
            Radium.ReloadContext();
        }
        
        ImGui.SetCursorPosX(Radium.ScreenPercentToRealX(29.7f));
        ImGui.SetCursorPosY(Radium.ScreenPercentToRealY(.5f));
        
        
        if (!started && ImGui.Button("Run", Radium.ScreenPercentToRealPos(5, 2))) {
            Console.WriteLine(Osmium.Scenes.Count);
            
            started = true;
            Debug.Clear();
            Osmium.VirtualRun();            
        }
        
        if (started && ImGui.Button("End", Radium.ScreenPercentToRealPos(5, 2))) {
            started = false;
            Osmium.VirtualClose();  
            Console.WriteLine("closed");
        }
        
        ImGui.End();
    }
}