using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс складывающихся планок.
/// </summary>
public class FoldingPlank : GroupElement
{
    /// <summary>
    /// Возвращает вернюю модель элемента УСП.
    /// </summary>
    public  UpFoldingPlank UpPlank
    {
        get
        {
            if (_upPlank == null)
            {
                SetAlongSlot();
            }
            return _upPlank;
        }
    }
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

    private UpFoldingPlank _upPlank;
    private Slot _acrossSlot, _alongSlot;

    /// <summary>
    /// Инициализирует новый экземпляр класса модели складывающейся планки УСП для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент детали УСП.</param>
    public FoldingPlank(Component component)
        : base(component)
    {
        
    }

    /// <summary>
    /// Удаляет сопряжение касания между складывающейся и кондукторной планкой.
    /// </summary>
    public void DeleteJigTouch()
    {
        _upPlank.DeleteJigTouch();
    }


    private void SetAcrossSlot()
    {
        Component upComponent;
        Edge slotEdge = GetEdge(Config.UpAcrossSlot, out upComponent);
        _upPlank = new UpFoldingPlank(upComponent, this);

        Point3d point1, point2;
        slotEdge.GetVertices(out point1, out point2);
        _acrossSlot = _upPlank.GetNearestSlot(point1);
    }

    private void SetAlongSlot()
    {
        Component upComponent;
        Edge slotEdge = GetEdge(Config.UpAlongSlot, out upComponent);
        _upPlank = new UpFoldingPlank(upComponent, this);

        Point3d point1, point2;
        slotEdge.GetVertices(out point1, out point2);
        _alongSlot = _upPlank.GetNearestSlot(point1);
    }
}
