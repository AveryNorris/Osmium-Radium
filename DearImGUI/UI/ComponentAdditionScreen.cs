using System.Reflection;
using ImGuiNET;
using OpenTK.Platform;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Osmium2DRenderer;
using OsmiumNucleus;


namespace DearImGUI;


public class ComponentAdditionScreen : IFreeElement
{

    public bool showModal = true;
    
    public string input = string.Empty;
    
    public string textinput = string.Empty;

    private List<Type> types = [];
    
    private Type selectedType;
    
    
    public void Define() {
        
        types.Clear();
        ImGui.OpenPopup("AddComponent");
        
        if (ImGui.BeginPopupModal("AddComponent", ref showModal, ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove)) {
            
            
            ImGui.SetWindowPos(Radium.ScreenPercentToRealPos(35, 35));
            ImGui.SetWindowSize(Radium.ScreenPercentToRealPos(15, 15));

            
            ImGui.SetCursorPosX(Radium.ScreenPercentToRealX(1));
            ImGui.SetNextItemWidth(Radium.ScreenPercentToRealX(13));
            ImGui.InputText("##AddComponentInput", ref input, int.MaxValue);
            //ImGui.SetKeyboardFocusHere();

            input += InputBackend.input;
            if (Osmium.Context.KeyboardState.IsKeyDown(Keys.Backspace)) {
                if(input.Length > 0)
                    input = input.Substring(0, input.Length - 1);
            }

            ImGui.SetCursorPosX(Radium.ScreenPercentToRealX(1));
            ImGui.SetNextItemWidth(Radium.ScreenPercentToRealX(13));
            if (ImGui.BeginListBox("##ValidTypes")) {

                foreach (Assembly assembly in Radium.LoadedProgram.Assemblies) {
                    foreach (Type type in assembly.GetTypes()) {
                        if (type.IsSubclassOf(typeof(Component)) && type.Name.Contains(input)) {
                            types.Add(type);
                            if (ImGui.Selectable(type.Name, selectedType == type)) {
                                selectedType = type;
                            }
                        }
                    }
                }

                ImGui.EndListBox();
            }

            if (Osmium.Context.KeyboardState.IsKeyDown(Keys.Enter) | Osmium.Context.KeyboardState.IsKeyDown(Keys.KeyPadEnter)) {
                GUIManager.FreeElements.Remove(this);

                if (Hierarchy.selectedComponent != null) {

                    if (selectedType != null) {

                        foreach (Assembly assembly in Radium.LoadedProgram.Assemblies) {
                            foreach (Type type in assembly.GetTypes()) {
                                if (type.IsSubclassOf(typeof(Component)) && type.Name.Contains(input)) {
                                    Component newComponent =  (Component)Activator.CreateInstance(type)!;
                                    newComponent.Name = type.Name;
                                    
                                    Hierarchy.selectedComponent?.Add(newComponent);
                                    Radium.ComponentMap.Add(new Radium.DefaultComponent(type.Name, new Radium.ReferencedType(type), [], SceneView.selectedScene!.Name));
                                }
                            }
                        }
                    }
                } else if(SceneView.selectedScene != null) {
                    if (selectedType != null) {

                        foreach (Assembly assembly in Radium.LoadedProgram.Assemblies) {
                            foreach (Type type in assembly.GetTypes()) {
                                if (type.IsSubclassOf(typeof(Component)) && type.Name.Contains(input)) {
                                    Component newComponent =  (Component)Activator.CreateInstance(type)!;
                                    newComponent.Name = type.Name;
                                    
                                    SceneView.selectedScene?.Add(newComponent);
                                    Radium.ComponentMap.Add(new Radium.DefaultComponent(type.Name, new Radium.ReferencedType(type), [], SceneView.selectedScene!.Name));
                                }
                            }
                        }
                    }
                }
            }
            
            
            ImGui.EndPopup();
            
        } else {
            GUIManager.FreeElements.Remove(this);
        }
    }
}