using System.Numerics;
using System.Reflection;
using System.Runtime.Loader;
using DearImGUI.Compiler;
using OsmiumNucleus;


namespace DearImGUI;


public static class Radium
{
    public static int ScreenWidth => Osmium.Context.Size.X;
    public static int ScreenHeight => Osmium.Context.Size.Y;

    
    //takes a percentage from 0-100 and resolves it to coordinates imgui will accept
    public static int ScreenPercentToRealX(float percentage) {
        return (int) (percentage / 100 * ScreenWidth);
    }
    
    //takes a percentage from 0-100 and resolves it
    public static int ScreenPercentToRealY(float percentage) {
        return (int) (percentage / 100 * ScreenHeight);
    }
    
    public static Vector2 ScreenPercentToRealPos(float percentageX, float percentageY) {
        return new Vector2(ScreenPercentToRealX(percentageX), ScreenPercentToRealY(percentageY));
    }

    //takes in 255, 255, 255
    public static Vector4 FromHex(float r, float g, float b) {
        return new Vector4(r / 255f, g / 255f, b / 255f, 1);
    }

    public static Vector4 FromHex(string hex) {
        if(hex.Length != 6) { return Vector4.One; }
        
        string r = hex.Substring(0, 2);
        string g = hex.Substring(2, 2);
        string b = hex.Substring(4, 2);

        return new Vector4(
            int.Parse(r, System.Globalization.NumberStyles.HexNumber) / 255f,
            int.Parse(g, System.Globalization.NumberStyles.HexNumber) / 255f,
            int.Parse(b, System.Globalization.NumberStyles.HexNumber) / 255f,
            1
        );
    }
    
    
    public static List<DefaultComponent> ComponentMap = [];
    
    public struct DefaultComponent
    {
        public string Name;
        public HashSet<string> Tags;
        public string Type;

        public DefaultVariable[] Variables;
    }


    public struct DefaultVariable
    {
        public string Name;
        public string Type;

        public object[] Values;
    }


    public static OsmiumProgram LoadedProgram;
    
    public static void ReloadContext() {
        Debug.LogAction("Reloading Osmium Context!");
        
        //if game is started, halt until the end or add like a dialogue box

        if (LoadedProgram != null) {
            //todo: forgotten method parameter, fixed in osmium but must be pushed to nuget and changed here, it literally does nothing so i made an empty list
            Osmium.VirtualClose([]);
            
            
            
            LoadedProgram.Unload();
        }
        
        //unload component map and all other context related things

        ScriptCompiler.CompileScripts();
        
        //CREATE ASSEMBLY LOAD CONTEXT FROM ALL MODULES AND THE CURRENT PROGRAM GIVEN
        
        //todo: rel path
        LoadedProgram = new OsmiumProgram();
        LoadedProgram.LoadFromAssemblyPath("/home/avery/Programming/DearImGUI/DearImGUI/Build/OsmiumProgram.dll");
        //todo rel path and make these dll contents
        //foreach (string libraryPath in Directory.GetFiles("/home/avery/Programming/DearImGUI/DearImGUI/Modules", "*.dll")) LoadedProgram.LoadFromAssemblyPath(libraryPath);
        
        Debug.LogAction("Finished Reloading Context!");
        
        Osmium.VirtualInitialize(LoadedProgram.Assemblies);
    }
    
}