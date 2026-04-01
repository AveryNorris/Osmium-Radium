namespace DearImGUI.Compiler;


public static class ChangeDetection
{
    public static Dictionary<string, DateTime> FileEditTimes = new Dictionary<string, DateTime>();

    public const string SourcePath = "/home/avery/Programming/DearImGUI/DearImGUI/Source";
    //todo make these load project paths at the start
    public const string ModulesPath = "/home/avery/Programming/DearImGUI/DearImGUI/Modules";

    
    
    /// <summary> Searches for all changes within the modules and compiler folders, TODO: maybe add a larger way to do this? we'll need to see texture file changes as well so it might make sense to
    /// </summary>
    public static void QueryChanges() {

        bool reload = false;
        
        foreach (string file in FileEditTimes.Keys) {
            if (!File.Exists(file)) {
                FileEditTimes.Remove(file);
                reload = true;
            }
        }

        List<string> targetFiles = [];
        targetFiles.AddRange(Directory.GetFiles(SourcePath, "*.cs"));
        targetFiles.AddRange(Directory.GetFiles(ModulesPath, "*.dll"));
        
        foreach (string file in targetFiles) {
            
            //todo: redundant?
            if (!File.Exists(file)) {
                FileEditTimes.Remove(file);
                continue;
            }
            
            if (!FileEditTimes.ContainsKey(file)) {
                FileEditTimes.Add(file, File.GetLastWriteTime(file));
            } else {
                DateTime lastWriteTime = File.GetLastWriteTime(file);
                if (FileEditTimes[file] == lastWriteTime) continue;
                
                FileEditTimes[file] = lastWriteTime;
            }

            reload = true;
        }
        
        if(reload) Radium.ReloadContext();
    }
}