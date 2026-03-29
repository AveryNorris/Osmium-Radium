using System;
using OsmiumNucleus;

namespace Dummy;

public class Test
{
    public static int A() {
        
        Osmium.Initialize();

        Osmium.AddScene("Main");
        
        Osmium.Run();
        
        
        return 0;
    }
} 