using System;
using NXOpen.Assemblies;

/// <summary>
/// Класс исключения отсутствия параметризированного объекта на детали.
/// </summary>
public class PartNotFoundExeption : Exception
{

    public PartNotFoundExeption()
    {
        
    }

    public PartNotFoundExeption(string message)
        : base(message)
    {

    }

    public PartNotFoundExeption(string message, Exception inner)
        : base(message, inner)
    {

    }
}
