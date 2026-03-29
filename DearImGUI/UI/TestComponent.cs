using OsmiumNucleus;


namespace DearImGUI;


public class TestComponent : Component
{
    [DisplayVariable] public bool valueA = false;
    [DisplayVariable] public bool valueB = false;
    
    [DisplayVariable] public string valueC = "hello world!";
    public bool valueD = false;
    public string valueE = "aaa";

    [DisplayVariable] private string hidden = "poopy";

    public void Update() {
        //Debug.LogAction(valueA.ToString());
    }
}