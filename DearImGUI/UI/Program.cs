using System.Numerics;
using DearImGUI;
using ImGuiNET;
using OsmiumNucleus;
using OpenTK.Graphics.OpenGL;
using Osmium2DRenderer;





//todo: editor initalize
Osmium.EditorInitialize();

GUIManager.Clear = true;
GUIManager.FreeElements.Add(new Hierarchy());
GUIManager.GUIElements.Add(new Inspector());
GUIManager.FreeElements.Add(new ConsoleFiles());
GUIManager.FreeElements.Add(new PlayButton());
GUIManager.FreeElements.Add(new TopMenu());

GUIManager.RadiumElements.Add(new SceneView());

ImGuiStylePtr style = ImGui.GetStyle();
style.TabRounding = 0;

//todo: GUIManager.Clear = false;


style.Colors[(int)ImGuiCol.ModalWindowDimBg] = Vector4.Zero;

foreach (ImGuiCol color in ColorSelector.PrimaryColorObjects) {
    style.Colors[(int)color] = ColorSelector.Primary;   
}

foreach (ImGuiCol color in ColorSelector.SelectColorObjects) {
    style.Colors[(int)color] = ColorSelector.Select;
}

foreach (ImGuiCol color in ColorSelector.HoverColorObjects) {
    style.Colors[(int)color] = ColorSelector.Hover;
}

foreach (ImGuiCol color in ColorSelector.BackgroundColorObjects) {
    style.Colors[(int)color] = ColorSelector.Background;
}

foreach (ImGuiCol color in ColorSelector.Background2ColorObjects) {
    style.Colors[(int)color] = ColorSelector.Background2;
}

//style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(1, 0, 0, 1);

//todo: camera
//Scene main = Osmium.AddScene("Main");
//Camera2D cam = main.Add<Camera2D>();
//cam.Swap = false;
//
//
//Sprite2D sprite = main.Add<Sprite2D>();
//sprite.source = new Texture2D("/home/avery/Programming/RendererTest/RendererTest/wireframe.png");
//sprite.Width = .30f;
//sprite.Height = .30f;
//sprite.X = -.5f;
//sprite.Y = .3f;
//
//Scene others = Osmium.AddScene("Level 1");
//others.Add<Sprite2D>();
//others.Add<Sprite2D>();
//others.Add<Sprite2D>();


//main.Add<TestComponent>();

Debug.WriteToConsole = true;

GUIManager.Clear = true;
GUIManager.Swap = true;

Osmium.EditorRun();
