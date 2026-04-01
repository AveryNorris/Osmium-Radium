using System.Numerics;
using ImGuiNET;


namespace DearImGUI;


public abstract class RadiumWindow : IRadiumGUI
{
    
    
    
    public Vector2 Pos;


    public Vector2 Size;
    
    
    public string NameID = "";

    
    
    public ImGuiWindowFlags WindowParameters = ImGuiWindowFlags.None;
    
    
    
    public virtual void Begin() => ImGui.Begin(NameID, WindowParameters);
    public virtual void End() => ImGui.End();

    public abstract void OnWindowGUI();
    
    public virtual void OnPos() => ImGui.SetWindowPos(Radium.ScreenPercentToRealPos(Pos.X, Pos.Y));

    public virtual void OnSize() => ImGui.SetWindowSize(Radium.ScreenPercentToRealPos(Size.X, Size.Y));
    


    public void OnGUI() {
        Begin();
        OnPos();
        OnSize();
        OnWindowGUI();
        End();
    }
    
    
    
}