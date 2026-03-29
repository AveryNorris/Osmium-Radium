using System.IO;
using System;


namespace Dummy.FrickPoop;


public class Lol
{
    public static void FileName() {
        Console.WriteLine(File.ReadAllText("/home/avery/Programming/TestingAgain/testfile.txt"));
    }
}