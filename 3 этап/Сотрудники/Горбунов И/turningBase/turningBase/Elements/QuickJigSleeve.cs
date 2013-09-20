using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// ����� ������������� ������������ ������.
/// </summary>
public class QuickJigSleeve : JigSleeve
{
    /// <summary>
    /// ���������� ����� ��� ������� ������ � ������������ ������.
    /// </summary>
    public Face TopSleeveFace
    {
        get
        {
            if (_topFace == null)
            {
                SetTopSleeveFace();
            }
            return _topFace;
        }
    }

    private Face _topFace;

    /// <summary>
    /// �������������� ����� ��������� ������ ������������� ������������� ������ ��� 
    /// ��� ��������� ����������.
    /// </summary>
    /// <param name="component">��������� ������ ���.</param>
    public QuickJigSleeve(Component component) : base(component)
    {
        
    }

    /// <summary>
    /// ������������� ������� ����� ������� � ������������ �������, ������������ ����� Replacement.
    /// </summary>
    public void SetTopSleeveFace()
    {
        _topFace = GetFace(Config.SleeveTopName);
    }
}
