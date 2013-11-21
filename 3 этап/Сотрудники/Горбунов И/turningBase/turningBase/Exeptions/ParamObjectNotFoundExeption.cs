using System;

/// <summary>
/// Класс исключения отсутствия параметризированного объекта на детали.
/// </summary>
public class ParamObjectNotFoundExeption : Exception
{
    /// <summary>
    /// Наименование детали.
    /// </summary>
    public string PartName;

    public ParamObjectNotFoundExeption()
    {
        
    }

    public ParamObjectNotFoundExeption(string partName, string objectName)
    {
        PartName = partName;
    }

    public ParamObjectNotFoundExeption(string message)
        : base(message)
    {

    }

    public ParamObjectNotFoundExeption(string message, Exception inner)
        : base(message, inner)
    {

    }
}
