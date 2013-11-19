using System;

public class EmptyQueryExeption : Exception
{
    public EmptyQueryExeption()
    {
        
    }

    public EmptyQueryExeption(string message)
        : base(message)
    {

    }

    public EmptyQueryExeption(string message, Exception inner)
        : base(message, inner)
    {

    }
}
