using System.Reflection;
using OsmiumNucleus;


namespace DearImGUI;


public static partial class Radium
{

    public static List<DefaultComponent> ComponentMap = [];


    public static void ResolveNullComponentTypeReference() {
        
    }
    
    
    
    
    
    //represents a single component in the map of the editor
    public readonly struct DefaultComponent(string Name, ReferencedType Type, List<DefaultVariable> Variables, string SceneName)
    {
        //returns a new Component

        public void Construct() {

            Scene? scene = Osmium.GetScene(SceneName);
            scene ??= Osmium.AddScene(SceneName);
            
            Type? type = Type.GetFromLoadedAssembly();
            if(type == null)
            {
                //todo: add screen to resolve this!
                //todo: add debug info to make it clear what type is failing
                Debug.LogError("A referenced type has returned null! Would you like to re-map these references to another Component type? " + Type.FullName); //add to this! make it remap all types of Default component! And maybe make referenced type a class
                return; 
            }
      
            //make component and maybe support constructor?
            object? newObject = Activator.CreateInstance(type);
            if (newObject == null)
            {
                //todo: give the option to clear, or manipulate the data in an editor and fetch all of the problem components, and add do for all option
                Console.WriteLine("Radium has somehow initialized a null Component! Chooose an affirmative action");
                return;
            }
        
        
            if (newObject is not Component NewComponent)
            {
                Console.WriteLine("A mapped object is not a Component!");
                return;
            }
            
            //todo: add component check here
        
        
            //sets variables to default values
            foreach (DefaultVariable variable in Variables) {
                variable.MapVariable(NewComponent, type);
            }
        
        
            NewComponent.Name = Name;
            //todo tags
            
            scene.Add(NewComponent);
        }
    }


    //represents any manually set variables
    public struct DefaultVariable
    {
       public string Name;
       public ReferencedType Type;
    
    
       //must be of primitive types!
       public object?[] Values;
    
    
       //send parent type for extra speed
       public void MapVariable(Component __parent, Type __parentType)
       {
           //dont worry about error handling here, since GetVariableInfo will catch and refactor it!
           Type? variableType = Type.GetFromLoadedAssembly();
          
           //todo: allow user to refactor all types of these variables to another one
           if (variableType == null) { Console.WriteLine("Radium cannot resolve a given type listed in the Component map! " + Type.FullName); return; }
    
    
           //create a new instance from given constructor values
           //todo: add a way to map value constructors for the editor to see
           object? variableValue = Activator.CreateInstance(variableType, Values);
          
           FieldInfo? field = __parentType.GetField(Name);
           if (field != null) {
               field.SetValue(__parent, variableValue);
               return;
           }
    
    
           PropertyInfo? property = __parentType.GetProperty(Name);
           if (property != null) {
               property.SetValue(__parent, variableValue);
               return;
           }
    
    
           Console.WriteLine("A referenced variable has been found as null! Would you like to re-map these references, or discard the mapped field? " + Name);
       }
    }
    
    
    
    
    public struct ReferencedType(Type __type)
    {
       public string? FullName = __type.FullName;
    
    
       public Type? GetFromLoadedAssembly()
       {
           //load from radium loaded assembly
           //todo: clean up
           
           if(FullName == null) { Debug.LogError("Radium loaded a null variable! "); return null; }

           foreach (Assembly assembly in Radium.LoadedProgram.Assemblies) {
               Type? type = assembly.GetType(FullName);
               
               if(type != null) return type;
           }
           
           return null;
       }
    }



}