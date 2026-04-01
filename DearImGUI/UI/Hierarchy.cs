using ImGuiNET;
using OsmiumNucleus;


namespace DearImGUI;


public class Hierarchy : IFreeElement
{

    public static Component? selectedComponent { get; set; } = null;
    
    public string Name { get; set; } = "Hierarchy";

    public int count = 0;

    public void Define() {
        
        ImGui.Begin("Hierarchy", ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.AlwaysHorizontalScrollbar | ImGuiWindowFlags.AlwaysVerticalScrollbar);

        count = 0;
        
        ImGui.SetWindowSize(Radium.ScreenPercentToRealPos(15, 68.5f));
        ImGui.SetWindowPos(Radium.ScreenPercentToRealPos(65, 3));

        string textValue = "";

        if (SceneView.selectedScene == null) {
            ImGui.End();
            return;
        }

        foreach (Component component in SceneView.selectedScene.Children) {
            MapComponent(component, 0);
        }
        
        if (ImGui.Button("+")) {
            GUIManager.FreeElements.Add(new ComponentAdditionScreen());
        }
        
        ImGui.End();

    }

    public void MapComponent(Component component, uint depth) {
        count++;
        
        string indents = " ";
        for (int i = 0; i < depth; i++) {
            indents += "    ";
        }
        
        
        ImGui.Text("\n\n");
        
        if (ImGui.Selectable(indents + component.Name + "##ComponentView" + count, selectedComponent == component)) {
            selectedComponent = component;
        }
        
        foreach(Component child in component.Children) {
            MapComponent(child, depth + 1);
        }
    }
}