using OsmiumNucleus;


namespace DearImGUI;


public class DrawManager : Component
{
    public void Draw() {
        OsmiumContext.SwapBuffers();
    }
}