using System;
using System.IO;


public class FileLogger
{
    public static void WriteString(string text)
    {
        string path = "gamelog.txt";

        DateTime dt = DateTime.Now;
        

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine($"{dt.ToString("yyyy-MM-dd HH:mm:ss")}: {text}");
        writer.Close();
    }
}
