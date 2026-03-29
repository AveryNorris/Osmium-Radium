using System.Numerics;
using ImGuiNET;
using OsmiumNucleus;


namespace DearImGUI;


public class ConsoleFiles : IFreeElement
{

    
    
    public void Define() {
        ImGui.Begin("ConsoleFiles", ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoScrollbar);
        
        ImGui.SetWindowSize(Radium.ScreenPercentToRealPos(65, 29));
        ImGui.SetWindowPos(Radium.ScreenPercentToRealPos(0, 71.5f));
        
        ImGui.BeginTabBar("##ConsoleFilesTabBar");

        if (ImGui.BeginTabItem("Console")) {

            ImGui.BeginChild("ScrollableRegion");
            
            ResolveDebugText(File.ReadAllText(Debug.LogFilePath));

            
            ImGui.EndChild();

            ImGui.EndTabItem();
        }
        
        if (ImGui.BeginTabItem("Files")) {


            string Path = "/home/avery/Programming/DearImGUI/DearImGUI";
            
            ImGui.TextWrapped(Path);

            string folderLayout = "";

            foreach (string folder in Directory.GetDirectories(Path)) {
                folderLayout += "   " + folder.Split('/')[^1];
            }

            foreach (string file in Directory.GetFiles(Path)) {
                folderLayout += "   " + file.Split('/')[^1];
            }
            
            ImGui.TextWrapped(folderLayout);
            
            
            
            ImGui.EndTabItem();
        }
        
        ImGui.EndTabBar();

        ImGui.End();
    }

    public string[] textTypes = ["ERR", "WRN", "ACT", "STK"];
    
    public Vector4[] textColors = [Radium.FromHex("c3381d"), Radium.FromHex("e26d15"), Radium.FromHex("cd387f"), Radium.FromHex("ffd600")];

    public void ResolveDebugText(string text) {
        ImGui.PushTextWrapPos();

        string[] splitText = text.Split('\n');

        foreach (string line in splitText) {

            int firstIndex = 0;
            for (int i = 0; i < line.Length; i++) {
                if (line[i] == ' ') {
                    firstIndex++;
                } else {
                    break;
                }
            }
            
            
            if (firstIndex + 3 >= line.Length) {
                ImGui.Text(line);
                continue;
            }
            
            
            string prefix = line.Substring(firstIndex, 3);
            if (textTypes.Contains(prefix)) {
                
                ImGui.PushStyleColor(ImGuiCol.Text, textColors[Array.IndexOf(textTypes, prefix)]);
                ImGui.Text(prefix);
                ImGui.PopStyleColor();
                
                ImGui.SameLine(0);
                ImGui.Text(line.Substring(firstIndex + 3, line.Length - firstIndex - 3));
            } else {
                ImGui.Text(line);
            }
        }
    }
}