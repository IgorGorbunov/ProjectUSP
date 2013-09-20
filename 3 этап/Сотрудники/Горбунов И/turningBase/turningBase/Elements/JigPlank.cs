using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс кондукторных планок.
/// </summary>
public class JigPlank : UspElement
{
    /// <summary>
    /// Возвращает основную НГП кондукторной планки.
    /// </summary>
    public Face SlotFace
    {
        get
        {
            if (_topSlotFace == null)
            {
                SetSlotFace();
            }
            return _topSlotFace;
        }
    }
    /// <summary>
    /// Возвращает грань для касания втулки и кондукторной планки.
    /// </summary>
    public Face TopJigFace
    {
        get
        {
            if (_topJigFace == null)
            {
                SetTopJigFace();
            }
            return _topJigFace;
        }
    }
    /// <summary>
    /// Возвращает грань для центрирования втулки в кондукторной планке.
    /// </summary>
    public Face SleeveFace
    {
        get
        {
            if (_sleeveFace == null)
            {
                SetSleeveFace();
            }
            return _sleeveFace;
        }
    }

    private Face _topSlotFace, _topJigFace, _sleeveFace;

    /// <summary>
    /// Инициализирует новый экземпляр класса кондукторной планки УСП для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент детали УСП.</param>
    public JigPlank(Component component) : base(component)
    {
        
    }

    /// <summary>
    /// Устанавливает НГП, использовать после Replacement.
    /// </summary>
    public void SetSlotFace()
    {
        Face[] faces = Body.GetFaces();

        int nEgdes = 0;
        foreach (Face face in faces)
        {
            if (face.Name != Config.SlotBottomName)
                continue;
            Edge[] edges = face.GetEdges();
            if (nEgdes > edges.Length)
                continue;
            _topSlotFace = face;
            nEgdes = edges.Length;
        }
    }
    /// <summary>
    /// Устанавливает верхнюю грань для втулки, использовать после Replacement.
    /// </summary>
    public void SetTopJigFace()
    {
        _topJigFace = GetFace(Config.JigTopName);
    }
    /// <summary>
    /// Устанавливает цилиндрическую грань для втулки, использовать после Replacement.
    /// </summary>
    public void SetSleeveFace()
    {
        _sleeveFace = GetFace(Config.JigSleeveName);
    }

    /// <summary>
    /// Создаёт констрэйнт TouchAxe кондукторной планки и обрабатываемой детали.
    /// </summary>
    /// <param name="component">Компонент обрабатываемой детали.</param>
    /// <param name="face">Обрабатываемая грань.</param>
    /// <returns></returns>
    public TouchAxe SetOn(Component component, Face face)
    {
        TouchAxe touchAxe = new TouchAxe();
        touchAxe.Create(ElementComponent, SleeveFace, component, face);
        return touchAxe;
    }
}
