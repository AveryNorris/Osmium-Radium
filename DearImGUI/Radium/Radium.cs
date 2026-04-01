using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Runtime.Loader;
using DearImGUI.Compiler;
using OsmiumNucleus;
using Debug = OsmiumNucleus.Debug;


namespace DearImGUI;


public static partial class Radium
{
    public static string Version = "1.0 WIP";
    
    public static int ScreenWidth => Osmium.Context.Size.X;
    public static int ScreenHeight => Osmium.Context.Size.Y;

    
    //takes a percentage from 0-100 and resolves it to coordinates imgui will accept
    public static int ScreenPercentToRealX(float percentage) => (int) (percentage / 100 * ScreenWidth);
    
    //takes a percentage from 0-100 and resolves it
    public static int ScreenPercentToRealY(float percentage) => (int) (percentage / 100 * ScreenHeight);
    
    public static Vector2 ScreenPercentToRealPos(float percentageX, float percentageY) => new Vector2(ScreenPercentToRealX(percentageX), ScreenPercentToRealY(percentageY));

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


    public static OsmiumProgram? LoadedProgram;
    
    public static void ReloadContext() {
        Stopwatch timer = Stopwatch.StartNew();
        
        Debug.LogAction("Reloading Osmium Context!");

        Hierarchy.selectedComponent = null;
        SceneView.selectedScene = null;
        
        if(File.Exists("/home/avery/Programming/DearImGUI/DearImGUI/Build/" + ScriptCompiler.ProgramName + ".dll"))
            File.Delete("/home/avery/Programming/DearImGUI/DearImGUI/Build/" + ScriptCompiler.ProgramName + ".dll");
        
        //if game is started, halt until the end or add like a dialogue box

        if (LoadedProgram != null) {
            
            foreach (Scene scene in Osmium.Scenes) {
                Osmium.RemoveScene(scene);
            }
            
            //todo: forgotten method parameter, fixed in osmium but must be pushed to nuget and changed here, it literally does nothing so i made an empty list
            Osmium.VirtualClose();
            
            
            
            LoadedProgram.Unload();
        }
        
        WeakReference oldAssembly = new(LoadedProgram);

        LoadedProgram = null;

        GC.Collect();
        GC.WaitForPendingFinalizers();
        
        //unload component map and all other context related things

        MemoryStream assemblyStream = ScriptCompiler.CompileScripts();
        
        //CREATE ASSEMBLY LOAD CONTEXT FROM ALL MODULES AND THE CURRENT PROGRAM GIVEN
        
        
        
        //todo: rel path
        LoadedProgram = new OsmiumProgram();

        try {
            LoadedProgram.LoadFromStream(assemblyStream);
        } catch(Exception e) {
            Debug.LogError(e.Message);
        }
        
        assemblyStream.Dispose();
        
        ModuleManager.AppendModules();
        
        //todo rel path and make these dll contents
        //foreach (string libraryPath in Directory.GetFiles("/home/avery/Programming/DearImGUI/DearImGUI/Modules", "*.dll")) LoadedProgram.LoadFromAssemblyPath(libraryPath);
        
        Debug.LogAction("Finished Reloading Context!");

        foreach (Assembly a in LoadedProgram.Assemblies) {
            Debug.LogAction(a.FullName);
        }
        
        Osmium.VirtualInitialize(LoadedProgram.Assemblies);

        foreach (DefaultComponent editorComponent in ComponentMap) {
            editorComponent.Construct();
        }
        
        timer.Stop();
        
        Debug.LogAction("Compiled and Mapped Program in " + timer.ElapsedMilliseconds + "ms");
    }
    
    
    
}