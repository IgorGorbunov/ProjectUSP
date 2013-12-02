using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс складывающихся планок.
/// </summary>
public class FoldingPlank : UspElement
{
    /// <summary>
    /// Возвращает поперечный паз.
    /// </summary>
    public Slot AcrossSlot
    {
        get
        {
            if (_acrossSlot == null)
            {
                SetAcrossSlot();
            }
            return _acrossSlot;
        }
    }
    /// <summary>
    /// Возвращает продольный паз.
    /// </summary>
    public Slot AlongSlot
    {
        get
        {
            if (_alongSlot == null)
            {
                SetAlongSlot();
            }
            return _alongSlot;
        }
    }

    private Slot _acrossSlot, _alongSlot;

    /// <summary>
    /// Инициализирует новый экземпляр класса модели складывающейся планки УСП для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент детали УСП.</param>
    public FoldingPlank(Component component)
        : base(component)
    {
        
    }


    private void SetAcrossSlot()
    {
        Edge slotEdge = GetEdge(Config.AcrossSlot);
        Point3d point1, point2;
        slotEdge.GetVertices(out point1, out point2);
        _acrossSlot = GetNearestSlot(point1);
    }

    private void SetAlongSlot()
    {
        Edge slotEdge = GetEdge(Config.AlongSlot);
        Point3d point1, point2;
        slotEdge.GetVertices(out point1, out point2);
        _alongSlot = GetNearestSlot(point1);
    }
}
