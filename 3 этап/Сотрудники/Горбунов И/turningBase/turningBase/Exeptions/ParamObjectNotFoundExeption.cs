using System;
using NXOpen.Assemblies;

/// <summary>
/// ����� ���������� ���������� �������������������� ������� �� ������.
/// </summary>
public class ParamObjectNotFoundExeption : Exception
{
    /// <summary>
    /// ������������� ������ ���.
    /// </summary>
    public readonly SingleElement SingleElement;
    /// <summary>
    /// �������������� ������ ���.
    /// </summary>
    public readonly GroupElement GroupElement;
    /// <summary>
    /// ������������ �������������������� �������.
    /// </summary>
    public readonly string NxObjectName;
    /// <summary>
    /// ������ �������� ���.
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
