using System.Numerics;
using ImGuiNET;


namespace DearImGUI;


public class ColorSelector : IGUIElement
{

    public static Vector4 Primary = Radium.FromHex("b75b48");

    public static ImGuiCol[] PrimaryColorObjects = [
        ImGuiCol.Button, //possibly not title bg active? maybe primary selected
        ImGuiCol.TitleBg,
        ImGuiCol.TitleBgCollapsed,
        ImGuiCol.Tab,
        ImGuiCol.TabDimmed,
        ImGuiCol.CheckMark,
        //ImGuiCol.TabSelected
        ImGuiCol.HeaderActive,
        ImGuiCol.TitleBgActive,
        ImGuiCol.FrameBg,
    ];

    public static Vector4 Hover = Radium.FromHex("a94337");

    public static ImGuiCol[] HoverColorObjects = [
        ImGuiCol.TabHovered,
        ImGuiCol.ButtonHovered,
        ImGuiCol.HeaderHovered,
        ImGuiCol.FrameBgHovered,
    ];
    
    public static Vector4 Select = Radium.FromHex("a93634");

    public static ImGuiCol[] SelectColorObjects = [
        ImGuiCol.TextSelectedBg,
        ImGuiCol.TabSelected,
        ImGuiCol.ButtonActive,
        //ImGuiCol.FrameBg,
        ImGuiCol.Header,
    ];

    public static Vector4 Background = Radium.FromHex("0b0b0b");
    
    public static ImGuiCol[] BackgroundColorObjects = [
        ImGuiCol.WindowBg,
    ];

    public static Vector4 Background2 = Radium.FromHex("181818");
    
    public static ImGuiCol[] Background2ColorObjects = [
        ImGuiCol.Border,
        ImGuiCol.BorderShadow,
        ImGuiCol.ScrollbarBg
    ];
    
    public string Name { get; set; } = "ColorSelector";

    public Vector4 color;
    
    public void Define() {
        ImGui.ColorEdit4("Primary", ref color);
        
        ImGuiStylePtr style = ImGui.GetStyle();
        Primary = color;
        //fix alpha 160 76 76
        Primary.W = 1;
        
        foreach (ImGuiCol color in PrimaryColorObjects) {
            style.Colors[(int)color] = Primary;   
        }
    }
}