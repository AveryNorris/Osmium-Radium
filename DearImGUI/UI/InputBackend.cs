using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OsmiumNucleus;


namespace DearImGUI;


public static class InputBackend
{

    public static string input = "";

    static InputBackend() {
        Osmium.Context.TextInput += OnTextInput;
        Osmium.Context.UpdateFrame += ClearInput;
    }

    public static void ClearInput(FrameEventArgs e) {
        input = string.Empty;

        if (Osmium.Context.KeyboardState.IsKeyDown(Keys.Backspace)) {
            if(input.Length > 0)
                input = input.Substring(0, input.Length - 1);
        }
    }

    public static void OnTextInput(TextInputEventArgs e) {
        input += e.AsString;
    }
}