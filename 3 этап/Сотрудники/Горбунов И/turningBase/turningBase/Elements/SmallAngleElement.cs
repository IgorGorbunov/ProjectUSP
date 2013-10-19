using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс кондукторных элементов для набора высоты.
/// </summary>
public class SmallAngleElement : UspElement
{
    /// <summary>
    /// Возвращает верхнюю грань элемента.
    /// </summary>
    public Face TopFace
    {
        get
        {
            if (_topFace == null)
            {
                SetFaces();
            }
            return _topFace;
        }
    }
    /// <summary>
    /// Возвращает нижнюю грань элемента.
    /// </summary>
    public Face BottomFace
    {
        get
        {
            if (_bottomFace == null)
            {
                SetFaces();
            }
            return _bottomFace;
        }
    }
    /// <summary>
    /// Возвращает первую боковую грань продольного отверстия в продольном пазе элемента.
    /// </summary>
    public Face HoleSideFace0
    {
        get
        {
            return _holeSide0;
        }
    }
    /// <summary>
    /// Возвращает вторую боковую грань продольного отверстия в продольном пазе элемента.
    /// </summary>
    public Face HoleSideFace1
    {
        get
        {
            return _holeSide1;
        }
    }
    /// <summary>
    /// Возвращает ребро элемента на поперечном пазе.
    /// </summary>
    public Edge AcrossSlotEdge
    {
        get
        {
            return _acrossEdge;
        }
    }
    /// <summary>
    /// Возвращает верхнее ребро у БОЛЬШЕГО КРАЯ элемента УСП.
    /// </summary>
    public Edge TopEdge
    {
        get
        {
            return _topEdge;
        }
    }
    /// <summary>
    /// Возвращает нижнее ребро у БОЛЬШЕГО КРАЯ элемента УСП.
    /// </summary>
    public Edge BottomEdge
    {
        get
        {
            return _bottomEdge;
        }
    }



    private Face _topFace, _bottomFace, _holeSide0, _holeSide1;
    private Edge _acrossEdge, _topEdge, _bottomEdge;



    /// <summary>
    ///Инициализирует новый экземпляр класса кондукторного элемента для набора высоты для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент из сборки NX.</param>
    public SmallAngleElement(Component component)
        : base(component)
    {
        Update();
    }
    /// <summary>
    /// Обновление граней после Replacement.
    /// </summary>
    public void Update()
    {
        SetFaces();
    }
    /// <summary>
    /// Метод ставит текущий элемент на заданный.
    /// </summary>
    /// <param name="element">Заданный элемент.</param>
    public void SetOn(HeightElement element)
    {

        NxFunctions.Update();
    }


    private void SetFaces()
    {
        _topFace = GetFace(Config.TopFace);
        _bottomFace = GetFace(Config.BottomFace);
        _holeSide0 = GetFace(Config.HoleSide0);
        _holeSide1 = GetFace(Config.HoleSide1);
        _acrossEdge = GetEdge(Config.AcrossSlot);
        _topEdge = GetEdge(Config.TopEdge);
        _bottomEdge = GetEdge(Config.BottomEdge);
    }
}
