using System;
using NXOpen.Assemblies;

/// <summary>
/// ����� ���������� ���������� �������������������� ������� �� ������.
/// </summary>
public class ParamObjectNotFoundExeption : Exception
{
    /// <summary>
    /// ������������ ������.
    /// </summary>
    public readonly UspElement Element;
    /// <summary>
    /// ������������ �������������������� �������.
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
