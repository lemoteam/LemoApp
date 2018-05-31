using System.Collections.Generic;

public class Page
{
    public string pageID { get; private set; } 
    public object versions { get; private set; } 
   // public object  
        
    public Page(string pageID, object versions)
    {
        this.pageID = pageID;
        this.versions = versions;
    }
}

/*
public class Versions
{
    public string code { get; private set; }
    public string text { get; private set; }
}
*/