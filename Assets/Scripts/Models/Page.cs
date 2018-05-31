using System.Collections.Generic;

public class Page
{
    public string pageID { get; private set; } 
    public List<Version> versions { get; private set; } 
   // public object  
        
    public Page(string pageID, List<Version> versions)
    {
        this.pageID = pageID;
        this.versions = versions;
    }
}
