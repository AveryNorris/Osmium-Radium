using ImGuiNET;
using OsmiumNucleus;


namespace DearImGUI;


public class SceneView : IGUIElement
{

    public static Scene? selectedScene = null;

    public int count = 0;
    
    public string Name { get; set; } = "Scenes";
    
    public void Define() {
        count = 0;
        
        ImGui.SetWindowSize(Radium.ScreenPercentToRealPos(15, 29));
        ImGui.SetWindowPos(Radium.ScreenPercentToRealPos(65, 71.5f));
        
        foreach (Scene scene in Osmium.Scenes) {
            count++;
            ImGui.Text("\n\n");
            if (ImGui.Selectable(" " + scene.Name + "##" + count, selectedScene == scene)) {
                selectedScene = scene;
            }
        }
    }
}