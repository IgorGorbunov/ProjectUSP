using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс базовых плит.
/// </summary>
public class BaseElement : UspElement
{
    /// <summary>
    /// Возвращает основную НГП базовой плиты.
    /// </summary>
    public Face TopSlotFace
    {
        get
        {
            if (_topSlotFace == null)
            {
                SetTopSlotFace();
            }
            return _topSlotFace;
        }
    }

    private Face _topSlotFace;

    /// <summary>
    ///Инициализирует новый экземпляр класса базовой плиты УСП для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент из сборки NX.</param>
    public BaseElement(Component component)
        : base(component)
    {
        
    }

    /// <summary>
    /// Устанавливает НГП, использовать после Replacement.
    /// </summary>
    public void SetTopSlotFace()
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
}
