

using OsmiumNucleus;


namespace DearImGUI.Compiler;


public static class ModuleManager
{
    public static void AppendModules() {

        foreach (string __modulePath in Directory.GetFiles(ChangeDetection.ModulesPath, "*.dll", SearchOption.TopDirectoryOnly)) {
            Debug.LogAction("Found and appending module type! " + __modulePath);
            
            Radium.LoadedProgram!.LoadFromAssemblyPath(__modulePath);
        }
    }
}