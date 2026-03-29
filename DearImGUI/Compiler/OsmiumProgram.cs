using System.Runtime.Loader;


namespace DearImGUI.Compiler;


public class OsmiumProgram() : AssemblyLoadContext(isCollectible:true) { }