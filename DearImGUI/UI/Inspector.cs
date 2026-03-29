using System.Numerics;
using System.Reflection;
using ImGuiNET;


namespace DearImGUI;


public class Inspector : IGUIElement
{

    public bool showModal = true;
    public string Name { get; set; } = "Inspector";

    public void Define() {
        
        ImGui.SetWindowSize(Radium.ScreenPercentToRealPos(20, 97));
        ImGui.SetWindowPos(Radium.ScreenPercentToRealPos(80, 3));
        
        if (Hierarchy.selectedComponent == null) return;
        
        foreach (FieldInfo field in Hierarchy.selectedComponent.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)) {

            if (field.GetCustomAttribute<DisplayVariable>() != null) {

                ImGui.Text("\n " + field.Name + " - " + field.GetValue(Hierarchy.selectedComponent));
                if (field.FieldType == typeof(bool)) {
                    bool fieldValue = (bool) field.GetValue(Hierarchy.selectedComponent);
                    bool checkBoxValue = fieldValue;

                    string ComponentID = "##ValueCheckbox/" + field.Name + '/';
                    ImGui.Checkbox(ComponentID, ref checkBoxValue);

                    if (checkBoxValue != fieldValue) {
                        field.SetValue(Hierarchy.selectedComponent, checkBoxValue);
                    }
                }
            }
        }
        
        foreach (PropertyInfo property in Hierarchy.selectedComponent.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)) {

            if (property.GetCustomAttribute<DisplayVariable>() != null) {

                ImGui.Text("\n " + property.Name + " - " + property.GetValue(Hierarchy.selectedComponent));
            }
        }

        if (ImGui.Button("+")) {
            GUIManager.FreeElements.Add(new ComponentAdditionScreen());
        }
    }
}