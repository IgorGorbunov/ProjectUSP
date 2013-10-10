using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс кондукторных элементов для набора высоты.
/// </summary>
public class HeightElement : UspElement
{
    /// <summary>
    /// Возвращает грань отверстия.
    /// </summary>
    public Face HoleFace
    {
        get
        {
            if (_holeFace == null)
            {
                SetFaces();
            }
            return _holeFace;
        }
    }
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

    private Face _holeFace, _topFace, _bottomFace;

    /// <summary>
    ///Инициализирует новый экземпляр класса кондукторного элемента для набора высоты для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент из сборки NX.</param>
    public HeightElement(Component component)
        : base(component)
    {
        
    }

    public void Update()
    {
        SetFaces();
    }


    private void SetFaces()
    {
        _holeFace = GetFace(Config.HeightHole);
        _topFace = GetFace(Config.HeightTop);
        _bottomFace = GetFace(Config.HeightBottom);
    }
}
