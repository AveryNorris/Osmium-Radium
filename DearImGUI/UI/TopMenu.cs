using ImGuiNET;
using OsmiumNucleus;


namespace DearImGUI;


public class TopMenu : IFreeElement
{

    public string Name { get; set; } = "Top Menu";
    public void Define() {

        ImGui.SetNextItemWidth(Radium.ScreenPercentToRealX(100));

        ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, Radium.ScreenPercentToRealPos(0, .95f));

        
        if (ImGui.BeginMainMenuBar()) {
            
    
            if (ImGui.BeginMenu("File")) {
                ImGui.MenuItem("Open");
                ImGui.MenuItem("Save");
                ImGui.EndMenu();
            }
            
            if (ImGui.BeginMenu("Edit")) {
                ImGui.EndMenu();
            }
            
            if (ImGui.BeginMenu("Options")) {
                ImGui.EndMenu();
            }

            string VersionText = "Osmium: V" + Osmium.Version + "   Radium: V" + Radium.Version;
            
            ImGui.SameLine(Radium.ScreenPercentToRealX(99) - ImGui.CalcTextSize(VersionText).X);
            ImGui.Text(VersionText);
            
            ImGui.EndMainMenuBar();
        }
        
        ImGui.PopStyleVar();
    }
}