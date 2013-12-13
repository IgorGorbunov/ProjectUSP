using System;
using NXOpen.Assemblies;

/// <summary>
/// Класс исключения отсутствия параметризированного объекта на детали.
/// </summary>
public class ParamObjectNotFoundExeption : Exception
{
    /// <summary>
    /// Одномодельная деталь УСП.
    /// </summary>
    public readonly SingleElement SingleElement;
    /// <summary>
    /// Многомодельная деталь УСП.
    /// </summary>
    public readonly GroupElement GroupElement;
    /// <summary>
    /// Наименование параметризированного объекта.
    /// </summary>
    public readonly string NxObjectName;
    /// <summary>
    /// Модель элемента УСП.
    /// </summary>
    public readonly UadElement Element;

    public ParamObjectNotFoundExeption()
    {
        
    }

    public ParamObjectNotFoundExeption(SingleElement element, string nxObjectName)
    {
        Element = element;
        NxObjectName = nxObjectName;
    }

    public ParamObjectNotFoundExeption(string message, GroupElement element, string nxObjectName)
        : base(message)
    {
        Element = element;
        NxObjectName = nxObjectName;
    }

    public ParamObjectNotFoundExeption(string message, SingleElement element, string nxObjectName)
        : base(message)
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
