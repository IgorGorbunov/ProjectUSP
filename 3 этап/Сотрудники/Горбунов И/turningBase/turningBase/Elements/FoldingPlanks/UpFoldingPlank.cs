using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// ����� �������� �������� �������������� ������.
/// </summary>
public class UpFoldingPlank : SingleElement
{


    /// <summary>
    /// �������������� ����� ��������� ������ �������� ��������
    /// �������������� ������ ��� ��������� ����������.
    /// </summary>
    /// <param name="component">��������� �� ������ NX.</param>
    /// <param name="foldingPlank">�������������� ������ ��� ������ ������.</param>
    public UpFoldingPlank(Component component, FoldingPlank foldingPlank) : base(component, foldingPlank)
    {

    }

    /// <summary>
    /// ������� ���������� ������� ����� ������� ��������� ��� ��������������
    ///  � ������������ ������.
    /// </summary>
    public void DeleteJigTouch()
    {
        ClearConstraint(Constraint.Type.Touch, Config.JigFoldingTouch);
    }
}

