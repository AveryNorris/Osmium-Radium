using DearImGUI.Compiler;
using ImGuiNET;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OsmiumNucleus;


namespace DearImGUI;


public static class GUIManager
{
    public static List<IGUIElement> GUIElements = [];
    
    public static List<IFreeElement> FreeElements = [];
    
    public static bool forceConstruct = false;
    
    public static int ShaderProgram;
    
    public static int VertexBuffer;

    public static int IndexBuffer;

    public static int VertexBufferArray;

    public static bool Clear = true;
    public static bool Swap = true;
    
    static GUIManager() {
        
        #region Program
        
        ShaderProgram = GL.CreateProgram();
        GL.UseProgram(ShaderProgram);

        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, File.ReadAllText("/home/avery/Programming/DearImGUI/DearImGUI/Vertex.glsl"));
        GL.CompileShader(vertexShader);
        GL.AttachShader(ShaderProgram, vertexShader);
        
        GL.GetShaderInfoLog(vertexShader, out string vinfo);
        Console.WriteLine(vinfo);
        
        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, File.ReadAllText("/home/avery/Programming/DearImGUI/DearImGUI/Fragment.glsl"));
        GL.CompileShader(fragmentShader);
        GL.AttachShader(ShaderProgram, fragmentShader);
        
        GL.GetShaderInfoLog(vertexShader, out string finfo);
        Console.WriteLine(finfo);
        
        GL.LinkProgram(ShaderProgram);
        
        GL.DetachShader(ShaderProgram, vertexShader);
        GL.DetachShader(ShaderProgram, fragmentShader);
        
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
        
        GL.UseProgram(0);
        
        #endregion
        
        #region Buffers
        
        VertexBuffer = GL.GenBuffer();
        
        IndexBuffer = GL.GenBuffer();



        VertexBufferArray = GL.CreateVertexArray();
        GL.BindVertexArray(VertexBufferArray);
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBuffer);
        
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 20, 0);
        GL.EnableVertexAttribArray(0);
        
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 20, 2 * sizeof(float));
        GL.EnableVertexAttribArray(1);
        
        GL.VertexAttribPointer(2, 4, VertexAttribPointerType.UnsignedByte, true, 20, 4 * sizeof(float));
        GL.EnableVertexAttribArray(2);
        
        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        #endregion


        Osmium.Context.UpdateFrame += Update;
        Osmium.Context.RenderFrame += Draw;
        Osmium.Context.Resize += Resize;
        
        
        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        GL.Disable(EnableCap.DepthTest);
        GL.Disable(EnableCap.CullFace);
        //GL.Enable(EnableCap.ScissorTest);

        ImGui.CreateContext();
        
        #region fonts
        
        var io = ImGui.GetIO();

        io.Fonts.GetTexDataAsRGBA32(out IntPtr pixels, out int width, out int height);

        int fontTexture = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2d, fontTexture);

        GL.TexImage2D(
            TextureTarget.Texture2d,
            0,
            InternalFormat.Rgba,
            width,
            height,
            0,
            PixelFormat.Rgba,
            PixelType.UnsignedByte,
            pixels
        );

        // texture settings
        GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

        // tell ImGui about it
        io.Fonts.SetTexID((IntPtr)fontTexture);

        // optional but good
        io.Fonts.ClearTexData();

        io.WantSaveIniSettings = false;

        #endregion
    }

    public static void Resize(ResizeEventArgs e) {
        GL.Viewport(0, 0, e.Width, e.Height);
    }
    
    public static void Update(FrameEventArgs e) {
        
        ImGuiIOPtr io = ImGui.GetIO();
        GL.UseProgram(ShaderProgram);

        io.ConfigDebugHighlightIdConflicts = false;
        
        io.DisplaySize = new System.Numerics.Vector2(
            Osmium.Context.Size.X,
            Osmium.Context.Size.Y
        );

        //todo: error point use event args
        io.DeltaTime = (float) Osmium.DeltaTime;
        
        Matrix4 projection = Matrix4.CreateOrthographicOffCenter(
            0.0f,
            Osmium.Context.ClientRectangle.Size.X,
            Osmium.Context.ClientRectangle.Size.Y,
            0.0f,
            -1.0f,
            1.0f
        );
        
        
        int loc = GL.GetUniformLocation(ShaderProgram, "proj");
        GL.UniformMatrix4f(loc, 1, false, ref projection);
        
        int tex = GL.GetUniformLocation(ShaderProgram, "tex");
        GL.Uniform1i(tex, 0);
        
        
        
        // mouse position
        var mouse = Osmium.Context.MousePosition;
        
        io.MousePos = new System.Numerics.Vector2(mouse.X, mouse.Y);

        // mouse buttons
        io.MouseDown[0] = Osmium.Context.IsMouseButtonDown(OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Left);
        io.MouseDown[1] = Osmium.Context.IsMouseButtonDown(OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Right);
        io.MouseDown[2] = Osmium.Context.IsMouseButtonDown(OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Middle);
        
        //todo: key data
        
        
        ImGui.NewFrame();

        foreach (IGUIElement element in GUIElements.ToList()) {
            ImGui.Begin(element.Name, ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove);
            element.Define();
            ImGui.End();
        }

        foreach (IFreeElement element in FreeElements.ToList()) {
            element.Define();
        }
        
        
        //todo: remove
        ChangeDetection.QueryChanges();
    }

    public static unsafe void Draw(FrameEventArgs e) {
        
        if(Clear) GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        
        ImGui.Render();
        
        ImDrawDataPtr data = ImGui.GetDrawData();

        for (int i = 0; i < data.CmdListsCount; i++) {
            ImDrawListPtr cmdList = data.CmdLists[i];
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, cmdList.VtxBuffer.Size * sizeof(ImDrawVert), cmdList.VtxBuffer.Data, BufferUsage.StreamDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, cmdList.IdxBuffer.Size * sizeof(ushort), cmdList.IdxBuffer.Data, BufferUsage.StreamDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            
            
            GL.UseProgram(ShaderProgram);
            GL.BindVertexArray(VertexBufferArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBuffer);

            int count = 0;
            
            GL.ActiveTexture(TextureUnit.Texture0);

            for (int n = 0; n < cmdList.CmdBuffer.Size; n++) {
                ImDrawCmdPtr cmd = cmdList.CmdBuffer[n];
                
                GL.BindTexture(TextureTarget.Texture2d, (int) cmd.TextureId);
                
                GL.DrawElements(
                    PrimitiveType.Triangles,
                    (int) cmd.ElemCount,
                    DrawElementsType.UnsignedShort,
                    count * sizeof(ushort)
                );

                //todo: dangerous cast?
                count += (int) cmd.ElemCount;
            }
        }
        
        if(Swap) Osmium.Context.SwapBuffers();
    }
}