using System;

/// <summary>
/// Класс исключения отсутствия модели элемента УСП в базе данных.
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
