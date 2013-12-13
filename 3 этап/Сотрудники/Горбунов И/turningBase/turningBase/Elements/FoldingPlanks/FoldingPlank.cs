using System;
using System.Collections.Generic;
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
    public UpFoldingPlank UpPlank
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
    /// <summary>
    /// Возвращает цилиндрическую грань отверстия в верхнем элементе для болта.
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
    /// Возвращает цилиндрическую грань отверстия в верхнем элементе для болта.
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
    /// Возвращает цилиндрическую грань отверстия в верхнем элементе для болта.
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
                Component component;
                GetFace(Config.FoldindBoltHole1, out component);
            }
            catch (Exception)
            {
                hole1Finded = false;
            }
            bool holeFinded = true;
            try
            {
                Component component;
                GetFace(Config.FoldindBoltHole, out component);
            }
            catch (Exception)
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
            throw new ParamObjectNotFoundExeption("Модель сборочного элемента '" + Title + "' неверно параметризована.", this, null);
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

    public Face[] LegsFaces
    {
        get
        {
            if (_legsFaces == null)
            {
                SetLegsFaces();
            }
            return _legsFaces;
        }
    }

    public Face TopFace
    {
        get
        {
            if (_topFace == null)
            {
                SetTopFace();
            }
            return _topFace;
        }
    }

    public FoldingType Type
    {
        get
        {
            if (ExistNotSlot())
            {
                return FoldingType.WithoutSlots;
            }
            return WithLongHole ? FoldingType.WithLongHole : FoldingType.WithCylHole;
        }
    }

    public double DistanceBetweenLegs
    {
        get
        {
            if (_distanceBetweenLegs == default(double))
            {
                SetDistanceBetweenLegs();
            }
            return _distanceBetweenLegs;
        }
    }

    public enum FoldingType
    {
        Null = 0,
        WithoutSlots = 1,
        WithCylHole = 2,
        WithLongHole = 3
    }

    private UpFoldingPlank _upPlank;
    private Slot _acrossSlot, _alongSlot;
    private Face _holeFace, _holeFace1, _holeFace2;
    private Face _topFace;
    private Face[] _holeAxeFaces;
    private Face[] _legsFaces;
    private double _distanceBetweenLegs;

    private Edge _acrossEdge;

    

    /// <summary>
    /// Инициализирует новый экземпляр класса модели складывающейся планки УСП 
    /// для заданного компонента.
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
        _acrossEdge = GetEdge(Config.UpAcrossSlot, out upComponent);
        _upPlank = new UpFoldingPlank(upComponent, this);

        Point3d point1, point2;
        _acrossEdge.GetVertices(out point1, out point2);
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

    private void SetHoleFace()
    {
        Component upComponent;
        _holeFace = GetFace(Config.FoldindBoltHole, out upComponent);
        if (_upPlank == null)
        {
            _upPlank = new UpFoldingPlank(upComponent, this);
        }
    }
    private void SetHoleFace1()
    {
        Component upComponent;
        _holeFace1 = GetFace(Config.FoldindBoltHole1, out upComponent);
        if (_upPlank == null)
        {
            _upPlank = new UpFoldingPlank(upComponent, this);
        }
    }
    private void SetHoleFace2()
    {
        Component upComponent;
        _holeFace2 = GetFace(Config.FoldindBoltHole2, out upComponent);
        if (_upPlank == null)
        {
            _upPlank = new UpFoldingPlank(upComponent, this);
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

    private void SetTopFace()
    {
        Component upComponent;
        Edge edge = GetEdge(Config.UpAcrossSlot, out upComponent);
        if (_upPlank == null)
        {
            _upPlank = new UpFoldingPlank(upComponent, this);
        }

        Face[] faces = edge.GetFaces();
        Vector vectorCyl = new Surface(HoleFace).Direction2;
        foreach (Face face in faces)
        {
            Vector vectorPlanar = new Surface(face).Direction2;
            if (!vectorCyl.IsParallel(vectorPlanar)) 
                continue;
            _topFace = face;
            return;
        }
    }

    private void SetLegsFaces()
    {
        _legsFaces = new Face[2];
        SetAcrossSlot();
        IEnumerable<Edge> edges = NxFunctions.GetParallelEdges(TopFace.GetEdges(), _acrossEdge, DistanceBetweenLegs);
        int i = 0;
        foreach (Edge edge in edges)
        {
            Face[] faces = edge.GetFaces();
            foreach (Face face in faces)
            {
                if (face == TopFace)
                    continue;
                _legsFaces[i] = face;
                i++;
                break;
            }
        }
    }

    //------------------------------- SQL - Structure -------------------

    private const string _T_NAME_NOT_SLOT = "USP_FOLDING_NOT_SLOT_ELEMS";
    private const string _C_TITLE = "TITLE";
    private const string _C_WIDTH = "WIDTH";

    private const string _FROM = " from " + _T_NAME_NOT_SLOT;
    private const string _WHERE = " where ";

    //--------------------------- SQL -------------------------------

    private bool ExistNotSlot()
    {
        return SqlOracle.Exist(Title, _C_TITLE, _T_NAME_NOT_SLOT);
    }

    private void SetDistanceBetweenLegs()
    {
        Dictionary<string, string> paramDict = new Dictionary<string, string>();
        paramDict.Add("TITLE", Title);
        decimal d;

        string query = Sql.GetBegin(_C_WIDTH) + _FROM + _WHERE;
        query += Sql.EqualStr(_C_TITLE, Title);

        if (SqlOracle.Sel(query, paramDict, out d))
        {
            _distanceBetweenLegs = (double)d;
            return;
        }
        throw new TimeoutException();
    }
    
}
