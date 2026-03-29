using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;


namespace DearImGUI.Compiler;


public static class ScriptCompiler
{
    public static void CompileScripts() {

        List<SyntaxTree> scriptTrees = [];
        
        //todo: clean up add docs
        foreach (string file in Directory.GetFiles(ChangeDetection.SourcePath, "*.cs")) {
            
            Console.WriteLine(file);
            
            scriptTrees.Add(CSharpSyntaxTree.ParseText(File.ReadAllText(file)));
        }
        
        //todo: make it easy to create modules add some editor option
        Compilation compiledProgram = CSharpCompilation.Create("OsmiumProgram", scriptTrees, DependencyResolver.GetDependencies(), new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        
        
        //todo: temp
        using FileStream stream = new FileStream("/home/avery/Programming/DearImGUI/DearImGUI/Build/OsmiumProgram.dll", FileMode.Create);
        EmitResult result = compiledProgram.Emit(stream);
        
        if (!result.Success)
        {
            foreach (Diagnostic diag in result.Diagnostics)
            {
                Console.WriteLine(diag.ToString());
            }
        }
        else
        {
            Console.WriteLine("DLL compiled successfully!");
        }
    }
}