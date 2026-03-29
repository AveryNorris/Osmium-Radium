using ImGuiNET;


namespace DearImGUI;


public class TopMenu : IFreeElement
{

    public string Name { get; set; } = "Top Menu";
    public void Define() {

        ImGui.SetNextItemWidth(Radium.ScreenPercentToRealX(100));

        
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
            
            ImGui.EndMainMenuBar();
        }
    }
}