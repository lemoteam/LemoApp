using System.Collections.Generic;
using System;

public class Message {

	// Class properties
    public string id { get; private set; }
    public string content { get; private set; }
    public string imageSlug { get; private set; }
    public string title { get; private set; }

    public Message(string id, string content, string imageSlug, string title)
    {
        this.id = id;
        this.content = content;
        this.imageSlug = imageSlug;
        this.title = title;
    }
}

