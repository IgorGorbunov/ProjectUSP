using System;
using NXOpen.Assemblies;

/// <summary>
/// Класс исключения отсутствия параметризированного объекта на детали.
/// </summary>
public class ParamObjectNotFoundExeption : Exception
{
    /// <summary>
    /// Наименование детали.
    /// </summary>
    public readonly UspElement Element;
    /// <summary>
    /// Наименование параметризированного объекта.
    /// </summary>
    public readonly string NxObjectName;

    public ParamObjectNotFoundExeption()
    {
        
    }

    public ParamObjectNotFoundExeption(UspElement element, string nxObjectName)
    {
        Element = element;
        NxObjectName = nxObjectName;
    }

    public ParamObjectNotFoundExeption(string message, string nxObjectName)
        : base(message)
    {
        NxObjectName = nxObjectName;
    }

    public ParamObjectNotFoundExeption(string message, Exception inner, string nxObjectName)
        : base(message, inner)
    {
        NxObjectName = nxObjectName;
    }
}
