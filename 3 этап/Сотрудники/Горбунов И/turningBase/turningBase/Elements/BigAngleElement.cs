using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс элементов УСП для набора больших углов.
/// </summary>
public class BigAngleElement : UspElement
{
    /// <summary>
    /// Возвращает нижнюю грань элемента.
    /// </summary>
    public Face BottomFace
    {
        get
        {
            return _bottomFace;
        }
    }
    /// <summary>
    /// Возвращает наклонную грань элемента.
    /// </summary>
    public Face InclineFace
    {
        get
        {
            return _inclineFace;
        }
    }
    /// <summary>
    /// Возвращает ребро элемента на продольном пазе.
    /// </summary>
    public Edge AlongSlotEdge
    {
        get
        {
            return _alongEdge;
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


    private Face _bottomFace, _inclineFace;
    private Edge _alongEdge, _acrossEdge;

    /// <summary>
    ///Инициализирует новый экземпляр класса кондукторного элемента для набора высоты для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент из сборки NX.</param>
    public BigAngleElement(Component component)
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

    ///// <summary>
    ///// Метод ставит текущий элемент на заданный.
    ///// </summary>
    ///// <param name="element">Заданный элемент.</param>
    //public void SetOn(UspElement element)
    //{

    //    NxFunctions.Update();
    //}

    private void SetFaces()
    {
        _bottomFace = GetFace(Config.BottomFace);
        _inclineFace = GetFace(Config.Incline);
        _alongEdge = GetEdge(Config.AlongSlot);
        _acrossEdge = GetEdge(Config.AcrossSlot);
    }
}
