using System;

public class BadQueryExeption : Exception
{
    public BadQueryExeption()
    {
        
    }

    public BadQueryExeption(string message)
        : base(message)
    {

    }

    public BadQueryExeption(string message, Exception inner)
        : base(message, inner)
    {

    }
}
