using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Возвращает грань для центрирования пазового болта в кондукторной планке.
    /// </summary>
    public Face HoleFace
    {
        get
        {
            if (_holeFace == null)
            {
                SetHoleFace();
            }
            return _holeFace;
        }
    }
    /// <summary>
    /// Возвращает поперечный паз.
    /// </summary>
    private Slot AcrossSlot
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
    private Slot AlongSlot
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

    private Face _topSlotFace, _topJigFace, _sleeveFace, _holeFace;

    /// <summary>
    /// Инициализирует новый экземпляр класса кондукторной планки УСП для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент детали УСП.</param>
    public JigPlank(Component component) : base(component)
    {
        
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

    public void SetOn(FoldingPlank fPlank)
    {
        IEnumerable<UspElement> fixElements = NxFunctions.FixElements(this, null);
        try
        {

            SlotTouch alongTouch = new SlotTouch(AlongSlot, fPlank.AlongSlot);
            alongTouch.SetTouchFaceConstraint();

            SlotConstraint alongConstraint2 = new SlotConstraint(AlongSlot, fPlank.AlongSlot);
            alongConstraint2.SetCenterConstraint2();
            
            SlotConstraint acrossConstraint = new SlotConstraint(AcrossSlot, fPlank.AcrossSlot);
            acrossConstraint.SetCenterConstraint();

            Vector jigDir = AlongSlot.Direction2;
            Vector foldDir = fPlank.AlongSlot.Direction2;
            if (!jigDir.IsCoDirectional(foldDir)) 
                return;
            alongConstraint2.Reverse();
            acrossConstraint.Reverse();
        }
        finally 
        {
            NxFunctions.Unfix(fixElements);
        }
    }

    public void SetOn(HeightElement heightElement)
    {
        IEnumerable<UspElement> fixElements = NxFunctions.FixElements(this, null);
        try
        {
            Face holeFace = heightElement.HoleFace;
            TouchAxe touchAxe = new TouchAxe();
            touchAxe.Create(HoleFace, holeFace);
        }
        finally
        {
            NxFunctions.Unfix(fixElements);
        }
    }


    /// <summary>
    /// Устанавливает НГП, использовать после Replacement.
    /// </summary>
    private void SetSlotFace()
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
    private void SetTopJigFace()
    {
        _topJigFace = GetFace(Config.JigTopName);
    }
    /// <summary>
    /// Устанавливает цилиндрическую грань для втулки, использовать после Replacement.
    /// </summary>
    private void SetSleeveFace()
    {
        _sleeveFace = GetFace(Config.JigSleeveName);
    }

    private void SetHoleFace()
    {
        _holeFace = GetFace(Config.JigHoleName);
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
