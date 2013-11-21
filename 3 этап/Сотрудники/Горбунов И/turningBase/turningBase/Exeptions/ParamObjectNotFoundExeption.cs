using System;

/// <summary>
/// ����� ���������� ���������� �������������������� ������� �� ������.
/// </summary>
public class ParamObjectNotFoundExeption : Exception
{
    /// <summary>
    /// ������������ ������.
    /// </summary>
    public readonly string PartName;
    /// <summary>
    /// ������������ �������������������� �������.
    /// </summary>
    public readonly string NxObjectName;

    public ParamObjectNotFoundExeption()
    {
        
    }

    public ParamObjectNotFoundExeption(string partName, string objectName, string nxObjectName)
    {
        PartName = partName;
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
