using System;

public class PartAlreadyLoadedExeption : Exception
{
    public PartAlreadyLoadedExeption()
    {
        
    }

    public PartAlreadyLoadedExeption(string message)
        : base(message)
    {

    }

    public PartAlreadyLoadedExeption(string message, Exception inner)
        : base(message, inner)
    {

    }
}
