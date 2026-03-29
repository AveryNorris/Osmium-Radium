using Microsoft.CodeAnalysis;
using OsmiumNucleus;


namespace DearImGUI.Compiler;


public static class DependencyResolver
{

    //todo: make change to match currently opened project
    public const string PluginPath = "/home/avery/Programming/DearImGUI/DearImGUI/Modules";
    
    
    public static MetadataReference[] GetDependencies() {

        List<string> targetPaths = [];
        targetPaths.AddRange(GetMicrosoftReferenceAssembliesPaths());
        targetPaths.AddRange(GetExternalModulePaths());
        
        //todo: clean up, foreach loop is asking for it
        List<MetadataReference> codeReferences = [];
        foreach (string libraryPath in targetPaths) {
            MetadataReference? libraryReference = ResolveReferenceFromPath(libraryPath);
            if (libraryReference == null) continue;
            
            codeReferences.Add(libraryReference);
        }
        
        return codeReferences.ToArray();
    }



    public static string[] GetMicrosoftReferenceAssembliesPaths() {
        return Directory.GetFiles("/usr/lib/dotnet/packs/Microsoft.NETCore.App.Ref/9.0.14/ref/net9.0/", "*.dll");
    }
    /// todo: add error documentation like C# exceptions? or attributes for that
    /// <summary> Finds all the trusted platform assembly paths, that give essential C# types</summary>
    //public static string[] GetTrustedPlatformLibraryPaths() => AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!.ToString()!.Split(Path.PathSeparator);
    /// <summary> Finds all the external modules that are present in the current plugin directory</summary>
    public static string[] GetExternalModulePaths() => Directory.GetFiles(PluginPath, "*.dll");

    //todo: add extension checks to prevent compiling txt lol
    


    public static MetadataReference? ResolveReferenceFromPath(string path) {
        try {
            return MetadataReference.CreateFromFile(path);
        } catch (ArgumentException) {
            Debug.LogError("The system cannot resolve a given trusted library's path! ", ["Path"], [path]);
        } catch (IOException) {
            Debug.LogError("An error occured while reading a trusted library!", ["Path"], [path]);
        }

        return null;
    }
    
    
    
}