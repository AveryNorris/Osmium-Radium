using System.Reflection;
using ImGuiNET;
using OpenTK.Platform;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Osmium2DRenderer;
using OsmiumNucleus;


namespace DearImGUI;


public class SceneAdditionScreen : IFreeElement
{

    public bool showModal = true;
    
    public string input = string.Empty;
    
    public string textinput = string.Empty;

    private List<Type> types = [];
    
    private Type selectedType;
    
    
    public void Define() {
        
        types.Clear();
        ImGui.OpenPopup("AddScene");
        
        if (ImGui.BeginPopupModal("AddScene", ref showModal, ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove)) {
            
            
            ImGui.SetWindowPos(Radium.ScreenPercentToRealPos(35, 35));
            ImGui.SetWindowSize(Radium.ScreenPercentToRealPos(15, 15));

            
            ImGui.SetCursorPosX(Radium.ScreenPercentToRealX(1));
            ImGui.SetNextItemWidth(Radium.ScreenPercentToRealX(13));
            ImGui.InputText("##AddSceneInput", ref input, int.MaxValue);
            //ImGui.SetKeyboardFocusHere();

            input += InputBackend.input;
            if (Osmium.Context.KeyboardState.IsKeyDown(Keys.Backspace)) {
                if(input.Length > 0)
                    input = input.Substring(0, input.Length - 1);
            }

            if (Osmium.Context.KeyboardState.IsKeyDown(Keys.Enter) | Osmium.Context.KeyboardState.IsKeyDown(Keys.KeyPadEnter)) {
                GUIManager.FreeElements.Remove(this);

                Osmium.AddScene(input);
            }
            
            
            ImGui.EndPopup();
            
        } else {
            GUIManager.FreeElements.Remove(this);
        }
    }
}