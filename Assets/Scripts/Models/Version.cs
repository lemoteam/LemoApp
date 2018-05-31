using System.Collections.Generic;

public class Version
{
    public string code { get; private set; }
    public string text { get; private set; }
    // public object  
        
    public Version(string code, string text)
    {
        this.code = code;
        this.text = text;
    }
}