using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Возвращает паз на нижней грани элемента.
    /// </summary>
    public Slot TopSlot
    {
        get
        {
            if (_topSlot == null)
            {
                SetTopSlotSet(false);
            }
            return _topSlot;
        }
    }
    public Slot TopSideSlot
    {
        get 
        { 
            SetTopSlotSet(true);
            return _topSlot;
        }
    }
    /// <summary>
    /// Возвращает паз на верхней грани элемента.
    /// </summary>
    public Slot BottomSlot
    {
        get
        {
            if (_bottomSlot == null)
            {
                SetBottomSlotSet(false);
            }
            return _bottomSlot;
        }
    }
    public Slot BottomSideSlot
    {
        get
        {
            SetBottomSlotSet(true);
            return _bottomSlot;
        }
    }

    public bool HasOutTopSlotSet
    {
        get
        {
            if (_topSlotSetSurface == null)
            {
                SetTopSlotSet(false);
            }
            return _hasOutTopSlotSet;
        }
    }

    public bool HasOutBottomSlotSet
    {
        get
        {
            if (_bottomSlotSetSurface == null)
            {
                SetBottomSlotSet(false);
            }
            return _hasOutBottomSlotSet;
        }
    }

    private Face _holeFace, _topFace, _bottomFace;

    private Surface _bottomSlotSetSurface, _topSlotSetSurface;
    private bool _hasOutTopSlotSet, _hasOutBottomSlotSet;

    private Slot _topSlot;
    private Slot _bottomSlot;

    private SlotConstraint _slotConstraint;
    private Touch _touchConstraint;
    private TouchAxe _touchAxe;

    /// <summary>
    ///Инициализирует новый экземпляр класса кондукторного элемента для набора высоты для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент из сборки NX.</param>
    public HeightElement(Component component)
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
        
        _touchConstraint = new Touch();
        _touchConstraint.Create(ElementComponent, BottomFace, element.ElementComponent, element.TopFace);

        if (HasOutBottomSlotSet && element.HasOutTopSlotSet)
        {
            _slotConstraint = new SlotConstraint(BottomSlot, element.TopSlot);
            _slotConstraint.SetCenterConstraint();
        }
        else
        {
            _slotConstraint = new SlotConstraint(BottomSideSlot, element.TopSideSlot);
            _slotConstraint.SetCenterConstraint();
        }
        
        _touchAxe = new TouchAxe();
        _touchAxe.Create(ElementComponent, HoleFace, element.ElementComponent, element.HoleFace);
        NxFunctions.Update();
    }

    //private void SetBottomSlot()
    //{
    //    SetSlot(BottomFace, ref _bottomSlot);
    //}

    //private void SetTopSlot()
    //{
    //    SetSlot(TopFace, ref _topSlot);
    //}

    private void SetSlot(Point3d point, ref Slot slot, Surface surface)
    {
        //Edge[] edges = face.GetEdges();
        //Point3d point1, point2;
        //edges[0].GetVertices(out point1, out point2);
        SlotSet slotSet = new SlotSet(this, surface.Face);
        slotSet.SetPoint(point);
        if (!slotSet.HaveNearestBottomFace()) 
            return;
        //slotSet.SetNearestEdges();
        slot = slotSet.GetNearestSlot();
        //slotSet.HasNearestSlot(out slot);
    }


    private bool SetSlotSet(Face face, out Surface slotSetSurface)
    {
        double minDistance = double.MaxValue;
        Surface mainSurface = new Surface(face);
        bool hasSlotSet = false;
        slotSetSurface = null;
        foreach (Face slotFace in SlotFaces)
        {
            Surface slotSurface = new Surface(slotFace);
            if (!Geom.IsEqual(mainSurface.Direction1, slotSurface.Direction1)) 
                continue;
            hasSlotSet = true;
            double distance = Math.Abs(mainSurface.GetDistance(slotSurface));
            if (distance > minDistance) 
                continue;
            minDistance = distance;
            slotSetSurface = slotSurface;
        }
        return hasSlotSet;
    }

    private void SetBottomSlotSet(bool sideSlot)
    {
        Surface projectSurface;
        if (SetSlotSet(_bottomFace, out _bottomSlotSetSurface))
        {
            if (sideSlot)
            {
                _hasOutBottomSlotSet = false;
                _bottomSlot = GetSideSlot(_bottomSlotSetSurface, GetPointOnHoleEdge());
            }
            else
            {
                _hasOutBottomSlotSet = true;
                projectSurface = _bottomSlotSetSurface;
                Point3d pointToProject = GetPointOnHoleEdge();
                Point3d point = projectSurface.GetProection(pointToProject);
                SetSlot(point, ref _bottomSlot, _bottomSlotSetSurface);
            }
        }
        else
        {
            _hasOutBottomSlotSet = false;
            projectSurface = new Surface(_bottomFace);

            _bottomSlot = GetSideSlot(projectSurface, GetPointOnHoleEdge());
        }
    }

    private void SetTopSlotSet(bool sideSlot)
    {
        Surface projectSurface;
        if (SetSlotSet(_topFace, out _topSlotSetSurface))
        {
            if (sideSlot)
            {
                _hasOutTopSlotSet = false;
                _topSlot = GetSideSlot(_topSlotSetSurface, GetPointOnHoleEdge());
            }
            else
            {
                _hasOutTopSlotSet = true;
                projectSurface = _topSlotSetSurface;
                Point3d pointToProject = GetPointOnHoleEdge();
                Point3d point = projectSurface.GetProection(pointToProject);
                SetSlot(point, ref _topSlot, _topSlotSetSurface);
            }
        }
        else
        {
            _hasOutTopSlotSet = false;
            projectSurface = new Surface(_topFace);
            _topSlot = GetSideSlot(projectSurface, GetPointOnHoleEdge());
        }
    }

    private Face GetNearestSlotFace(Surface outSurface, Point3d point)
    {
        double minDistance = Double.MaxValue;
        Face face = null;
        foreach (Face slotFace in SlotFaces)
        {
            Surface surface = new Surface(slotFace);
            if (surface.IsParallel(outSurface))
                continue;
            double distance = surface.GetDistance(point);
            if (distance > minDistance) 
                continue;
            minDistance = distance;
            face = slotFace;
        }
        return face;
    }

    private Slot GetSideSlot(Surface outSurface, Point3d point)
    {
        double minDistance = Double.MaxValue;
        Slot goodSlot = null;
        foreach (Face slotFace in SlotFaces)
        {
            Surface surface = new Surface(slotFace);
            if (surface.IsParallel(outSurface))
                continue;

            SlotSet slotSet = new SlotSet(this, surface.Face);
            slotSet.SetPoint(point);
            Slot slot = slotSet.GetNearestSlot();

            Vector vector1 = outSurface.Direction2;
            Vector vector2 = slot.Direction2;
            if (!vector1.IsParallel(vector2))
                continue;

            double distance = surface.GetDistance(point);
            if (distance > minDistance)
                continue;
            minDistance = distance;
            goodSlot = slot;
        }
        return goodSlot;
    }

    private Point3d GetPointOnHoleEdge()
    {
        double faceRadius = (new Surface(HoleFace)).Radius;
        Edge[] edges = HoleFace.GetEdges();
        foreach (Edge edge in edges)
        {
            double edgeRadius = Geom.GetDiametr(edge)/2;
            if (Config.Round(faceRadius) != Config.Round(edgeRadius)) 
                continue;
            Point3d point1, point2;
            edge.GetVertices(out point1, out point2);
            return point1;
        }
        return new Point3d();
    }

    private void SetFaces()
    {
        _holeFace = GetFace(Config.HeightHole);
        _topFace = GetFace(Config.TopFace);
        _bottomFace = GetFace(Config.BottomFace);
    }
}
