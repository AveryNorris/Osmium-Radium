namespace DearImGUI;


public interface IGUIElement
{
    public string Name { get; protected internal set; }

    protected internal void Define();
}