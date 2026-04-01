using System.Numerics;
using DearImGUI.Tools;
using ImGuiNET;
using OsmiumNucleus;


namespace DearImGUI;


public class SceneView : RadiumWindow
{

    public static Scene? selectedScene = null;
    
    public SceneView() {
        NameID = "Scenes";
        
        WindowParameters = ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove;

        Size = new Vector2(15, 29);
        Pos = new Vector2(65, 71.5f);
    }
    
    public override void OnWindowGUI() {
        
        foreach (Scene scene in Osmium.Scenes) {
            ImGui.Text("\n\n"); if (ImGui.Selectable(" " + scene.Name, selectedScene == scene)) selectedScene = scene;
        }

        ImGui.PushStyleColor(ImGuiCol.Button, ColorSelector.Select);
        ImGui.SetCursorPos(Radium.ScreenPercentToRealPos(0, 22.5f));
        
        if (ImGui.Button("+", Radium.ScreenPercentToRealPos(15, 2))) {
            GUIManager.FreeElements.Add(new SceneAdditionScreen());
        }
        ImGui.PopStyleColor();
    }
}