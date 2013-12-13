using System;
using System.Collections.Generic;
using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс кондукторных планок.
/// </summary>
public class JigPlank : SingleElement
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
    /// <summary>
    /// Возвращает true, если отверстие на верхем элементе УСП длинное и продольное.
    /// </summary>
    public bool WithLongHole
    {
        get
        {

            bool hole1Finded = true;
            try
            {
                GetFace(Config.JigHole1Name);
            }
            catch (ParamObjectNotFoundExeption)
            {
                hole1Finded = false;
            }
            bool holeFinded = true;
            try
            {
                GetFace(Config.JigHoleName);
            }
            catch (ParamObjectNotFoundExeption)
            {
                holeFinded = false;
            }
            if (hole1Finded && !holeFinded)
            {
                return true;
            }
            if (!hole1Finded && holeFinded)
            {
                return false;
            }
            throw new ParamObjectNotFoundExeption("Модель элемента '" + Title + "' неверно параметризована.", this, null);
        }
    }

    public Face[] HoleAxesFaces
    {
        get
        {
            if (_holeAxeFaces == null)
            {
                SetHoleAxeFaces();
            }
            return _holeAxeFaces;
        }
    }
    /// <summary>
    /// Возвращает грань 1 для центрирования пазового болта в кондукторной планке.
    /// </summary>
    public Face HoleFace1
    {
        get
        {
            if (_holeFace1 == null)
            {
                SetHoleFace1();
            }
            return _holeFace1;
        }
    }
    /// <summary>
    /// Возвращает грань 2 для центрирования пазового болта в кондукторной планке.
    /// </summary>
    public Face HoleFace2
    {
        get
        {
            if (_holeFace2 == null)
            {
                SetHoleFace2();
            }
            return _holeFace2;
        }
    }

    private Slot _acrossSlot, _alongSlot;

    private Face _topSlotFace, _topJigFace, _sleeveFace, _holeFace, _holeFace1, _holeFace2;
    private Face[] _holeAxeFaces;

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
    /// <summary>
    /// Создаёт сопряжения кондукторной и складывающейся планки.
    /// </summary>
    /// <param name="fPlank"></param>
    public void SetOn(FoldingPlank fPlank)
    {
        IEnumerable<SingleElement> fixElements = NxFunctions.FixElements(this, null);
        try
        {
            if (WithLongHole)
            {
                switch (fPlank.Type)
                {
                    case FoldingPlank.FoldingType.WithoutSlots:
                        SetOnFoldingWithoutSlotsLong(fPlank);
                        break;
                    case FoldingPlank.FoldingType.WithCylHole:
                        SetOnFoldingWithCylHoleLong(fPlank);
                        break;
                    case FoldingPlank.FoldingType.WithLongHole:
                        SetOnFoldingWithLongHoleLong(fPlank);
                        break;
                }
            }
            else
            {
                switch (fPlank.Type)
                {
                    case FoldingPlank.FoldingType.WithoutSlots:
                        SetOnFoldingWithoutSlotsCyl(fPlank);
                        break;
                    case FoldingPlank.FoldingType.WithCylHole:
                        SetOnFoldingWithCylHoleCyl(fPlank);
                        break;
                    case FoldingPlank.FoldingType.WithLongHole:
                        SetOnFoldingWithLongHoleCyl(fPlank);
                        break;
                }
            }
            NxFunctions.Update();
        }
        finally 
        {
            NxFunctions.Unfix(fixElements);
        }
    }

    private void SetOnFoldingWithoutSlotsLong(FoldingPlank fPlank)
    {
        Slot jigSlot = AlongSlot;
        KeyValuePair<Face, double>[] faces = jigSlot.OrtFaces;
        Touch touch = new Touch();
        //нижняя грань кондукторной планки + верхняя грань складывающейся планки
        touch.Create(faces[faces.Length - 1].Key, fPlank.TopFace);

        Center centerAxe = new Center();
        centerAxe.CreateAxe12(fPlank.HoleFace, HoleAxesFaces[0], HoleAxesFaces[1]);

        Center center = new Center();
        center.Create22(jigSlot, fPlank.LegsFaces[0], fPlank.LegsFaces[1]);
    }

    private void SetOnFoldingWithLongHoleLong(FoldingPlank fPlank)
    {
        SlotTouch alongTouch = new SlotTouch(AlongSlot, fPlank.AlongSlot);
        alongTouch.SetTouchFaceConstraint(Config.JigFoldingTouch);

        Center center = new Center();
        center.Create22(AlongSlot, fPlank.AlongSlot);

        Center centerAxe = new Center();
        centerAxe.CreateAxe22(HoleFace1, HoleFace2, fPlank.HoleFace1, fPlank.HoleFace2);
    }

    private void SetOnFoldingWithCylHoleLong(FoldingPlank fPlank)
    {
        SlotTouch alongTouch = new SlotTouch(AlongSlot, fPlank.AlongSlot);
        alongTouch.SetTouchFaceConstraint(Config.JigFoldingTouch);

        SlotConstraint alongConstraint2 = new SlotConstraint(AlongSlot, fPlank.AlongSlot);
        alongConstraint2.SetCenterConstraint2();

        Center centerAxe = new Center();
        centerAxe.CreateAxe12(fPlank.HoleFace, HoleAxesFaces[0], HoleAxesFaces[1]);

        Vector jigDir = AlongSlot.Direction2;
        Vector foldDir = fPlank.AlongSlot.Direction2;
        if (!jigDir.IsCoDirectional(foldDir))
            return;
        alongConstraint2.Reverse();
        //acrossConstraint.Reverse();
    }

    private void SetOnFoldingWithoutSlotsCyl(FoldingPlank fPlank)
    {
        Slot jigSlot = AlongSlot;
        KeyValuePair<Face, double>[] faces = jigSlot.OrtFaces;
        Touch touch = new Touch();
        //нижняя грань кондукторной планки + верхняя грань складывающейся планки
        touch.Create(faces[faces.Length - 1].Key, fPlank.TopFace);

        TouchAxe touchAxe = new TouchAxe();
        touchAxe.Create(HoleFace, fPlank.HoleFace);

        Center center = new Center();
        center.Create22(jigSlot, fPlank.LegsFaces[0], fPlank.LegsFaces[1]);

        //Vector jigDir = AlongSlot.Direction2;
        //Vector foldDir = fPlank.AlongSlot.Direction2;
        //if (!jigDir.IsCoDirectional(foldDir))
        //    return;
        //alongConstraint2.Reverse();
        //acrossConstraint.Reverse();
    }

    private void SetOnFoldingWithLongHoleCyl(FoldingPlank fPlank)
    {
        SlotTouch alongTouch = new SlotTouch(AlongSlot, fPlank.AlongSlot);
        alongTouch.SetTouchFaceConstraint(Config.JigFoldingTouch);

        Center center = new Center();
        center.Create12(HoleFace, fPlank.HoleFace1, fPlank.HoleFace2);

        SlotConstraint acrossConstraint = new SlotConstraint(AcrossSlot, fPlank.AcrossSlot);
        acrossConstraint.SetCenterConstraint();

        Vector jigDir = AlongSlot.Direction2;
        Vector foldDir = fPlank.AlongSlot.Direction2;
        if (!jigDir.IsCoDirectional(foldDir))
            return;
        acrossConstraint.Reverse();
    }

    private void SetOnFoldingWithCylHoleCyl(FoldingPlank fPlank)
    {
        SlotTouch alongTouch = new SlotTouch(AlongSlot, fPlank.AlongSlot);
        alongTouch.SetTouchFaceConstraint(Config.JigFoldingTouch);

        SlotConstraint alongConstraint2 = new SlotConstraint(AlongSlot, fPlank.AlongSlot);
        alongConstraint2.SetCenterConstraint2();

        TouchAxe touchAxe = new TouchAxe();
        touchAxe.Create(HoleFace, fPlank.HoleFace);

        Vector jigDir = AlongSlot.Direction2;
        Vector foldDir = fPlank.AlongSlot.Direction2;
        if (!jigDir.IsCoDirectional(foldDir))
            return;
        alongConstraint2.Reverse();
        //acrossConstraint.Reverse();
    }

    public void SetOn(HeightElement heightElement)
    {
        IEnumerable<SingleElement> fixElements = NxFunctions.FixElements(this, null);
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


    private void SetHoleAxeFaces()
    {
        _holeAxeFaces = new Face[2];
        List<Face> cylFaces = new List<Face>();
        Edge[] edges1 = HoleFace1.GetEdges();
        foreach (Edge edge1 in edges1)
        {
            Face[] faces1 = edge1.GetFaces();
            foreach (Face face1 in faces1)
            {
                if (face1 == HoleFace1)
                    continue;
                if (face1.SolidFaceType == Face.FaceType.Cylindrical)
                {
                    cylFaces.Add(face1);
                }
            }
        }

        Edge[] edges2 = HoleFace2.GetEdges();
        int i = 0;
        foreach (Edge edge2 in edges2)
        {
            Face[] faces2 = edge2.GetFaces();
            foreach (Face face2 in faces2)
            {
                if (face2 == HoleFace2)
                    continue;
                if (face2.SolidFaceType != Face.FaceType.Cylindrical ||
                    !Instr.Exist(cylFaces, face2))
                    continue;
                _holeAxeFaces[i] = face2;
                i++;
            }
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

    private void SetHoleFace1()
    {
        _holeFace1 = GetFace(Config.JigHole1Name);
    }

    private void SetHoleFace2()
    {
        _holeFace2 = GetFace(Config.JigHole2Name);
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
