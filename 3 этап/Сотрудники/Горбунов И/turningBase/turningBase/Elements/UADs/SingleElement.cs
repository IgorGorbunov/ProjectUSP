﻿using System;
using System.Collections.Generic;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс содержащий элемент УСП из единственной модели.
/// </summary>
public class SingleElement : UadElement
{
    

    /// <summary>
    /// Возвращает список всех нижних плоскостей пазов для данного элемента.
    /// </summary>
    public IEnumerable<Face> SlotFaces
    {
        get
        {
            return _bottomFaces;
        }
    }
    /// <summary>
    /// Возвращает тело данного элемента.
    /// </summary>
    public Body Body
    {
        get
        {
            return _body;
        }
    }
    /// <summary>
    /// Возвращает каталог для данного элемента.
    /// </summary>
    public Catalog UspCatalog
    {
        get
        {
            if (_catalog == null)
            {
                if (Parent != null)
                {
                    _catalog = Parent.UspCatalog;
                }
                else
                {
                    string catalogNum = SqlUspElement.GetCatalogNum(Title);
                    if (catalogNum == "0")
                    {
                        _catalog = new Catalog8();
                    }
                    if (catalogNum == "1")
                    {
                        _catalog = new Catalog12();
                    }
                }
            }
            return _catalog;
        }
    }
    /// <summary>
    /// Возвращает true, если элемент для набора большого угла.
    /// </summary>
    public bool IsBiqAngleElement
    {
        get
        {
            string gost = Gost;
            List<string> gosts = SqlUspBigAngleElems.GetGosts(UspCatalog);
            foreach (string s in gosts)
            {
                if (s == gost)
                {
                    return true;
                }
            }
            return false;
        }
    }
    /// <summary>
    /// Возвращает ГОСТ элемента УСП.
    /// </summary>
    protected string Gost
    {
        get
        {
            if (_gost != null) 
                return _gost;
            _gost = Parent == null ? SqlUspElement.GetGost(Title) : Parent.Gost;
            return _gost;
        }
    }
    
    private GroupElement Parent
    {
        get { return _parent; }
        set { _parent = value; }
    }

    private GroupElement _parent;
    private Fix _fixter;
    private Body _body;

    private Catalog _catalog;
    private string _gost;
    

    List<Face> _bottomFaces;

    /// <summary>
    /// Инициализирует новый экземпляр класса элемента УСП для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент из сборки NX.</param>
    public SingleElement(Component component) : base(component)
    {
        SetBody();
        SetBottomFaces();
        //вдргу компонент побьётся
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса одномодельного элемента УСП для 
    /// заданного компонента и сборочной единицы УСП.
    /// </summary>
    /// <param name="component">Компонент из сборки NX.</param>
    /// <param name="groupElement">Сборочная единица УСП.</param>
    public SingleElement(Component component, GroupElement groupElement)
        : this(component)
    {
        Parent = groupElement;
    }

    /// <summary>
    /// Проводит поиск и устанавливает нижние плоскости пазов.
    /// </summary>
    public void SetBottomFaces()
    {
        Face[] faces = _body.GetFaces();

        _bottomFaces = new List<Face>();
        for (int j = 0; j < faces.Length; j++)
        {
            try
            {
                Face face = faces[j];

                if (face.Name != null)
                {
                    string[] split = face.Name.Split(Config.FaceNameSplitter);

                    if (split[0] == Config.SlotSymbol && 
                        split[1] == Config.SlotBottomSymbol)
                    {
                        _bottomFaces.Add(face);
                    }
                }
            }
            catch (NXException ex)
            {
                if (ex.ErrorCode != 3520016)
                {
                    UI.GetUI().NXMessageBox.Show("Ошибка!",
                                                 NXMessageBox.DialogType.Error,
                                                 "Ашипка!");
                }
            }
        }

        string mess = "НГП для данного элемента:";
        foreach (Face f in _bottomFaces)
        {
            mess += Environment.NewLine + f;
        }
        mess += Environment.NewLine + "============";
        Logger.WriteLine(mess);
    }

    /// <summary>
    /// Возвращает грань объекта по её имени.
    /// </summary>
    /// <param name="faceName">Обозначение грани.</param>
    /// <returns></returns>
    public Face GetFace(string faceName)
    {
        Face[] faces = _body.GetFaces();
        foreach (Face face in faces)
        {
            if (face.Name == faceName)
            {
                return face;
            }
        }
        string mess = "Грань " + faceName + " в элементе " + ElementComponent.Name + " не найдена.";
        mess += Environment.NewLine + "Тип элемента - " + GetType();
        Logger.WriteLine(mess);
        throw new ParamObjectNotFoundExeption(this, faceName);
    }
    /// <summary>
    /// Возвращает ребро элемента по его имени.
    /// </summary>
    /// <param name="edgeName">Обозначение ребра.</param>
    /// <returns></returns>
    public Edge GetEdge(string edgeName)
    {
        Edge[] edges = _body.GetEdges();
        foreach (Edge edge in edges)
        {
            if (edge.Name == edgeName)
            {
                return edge;
            }
        }
        string mess = "Грань " + edgeName + " в элементе " + ElementComponent.Name + " не найдена.";
        mess += Environment.NewLine + "Тип элемента - " + GetType();
        Logger.WriteLine(mess);
        throw new ParamObjectNotFoundExeption(this, edgeName);
    }

    public virtual void AttachToMe(SmallAngleElement smallAngleElement)
    {
        
    }

    /// <summary>
    /// Возвращает паз, к которому принадлежит заданное ребро.
    /// </summary>
    /// <param name="edge">Ребро паза, желательно - поперечное.</param>
    /// <returns></returns>
    public Slot GetSlot(Edge edge)
    {
        Point3d point1, point2;
        edge.GetVertices(out point1, out point2);
        return GetNearestSlot(point1);
    }
    /// <summary>
    /// Возвращает ближайший паз для данной точки.
    /// </summary>
    /// <param name="point">Точка.</param>
    /// <returns></returns>
    public Slot GetNearestSlot(Point3d point)
    {
        SlotSet slotSet = new SlotSet(this);
        slotSet.SetPoint(point);

        if (!slotSet.HaveNearestBottomFace())
            return null;

        Slot slot = slotSet.GetNearestSlot();
        return slot;
    }
    /// <summary>
    /// Возвращает объекты позиционирования заданного констрэйента для данного элемента.
    /// </summary>
    /// <param name="constraint">Заданный констрэйнт.</param>
    /// <param name="otherObjects">Объекты позиционирования для другого элемента.</param>
    /// <returns></returns>
    public List<NXObject> GetConstraintObjects(ComponentConstraint constraint, out List<NXObject> otherObjects)
    {
        List<NXObject> nxObjects = new List<NXObject>();
        otherObjects = new List<NXObject>();
        ConstraintReference[] references = constraint.GetReferences();
        foreach (ConstraintReference reference in references)
        {
            NXObject nxObject = reference.GetGeometry();
            if (nxObject.OwningComponent == ElementComponent)
            {
                nxObjects.Add(nxObject);
            }
            else
            {
                otherObjects.Add(nxObject);
            }
        }
        return nxObjects;
    }



    /// <summary>
    /// Зафиксировать элемент.
    /// </summary>
    public void Fix()
    {
        _fixter = new Fix();
        _fixter.Create(ElementComponent);
    }
    /// <summary>
    /// Разфиксировать элемент.
    /// </summary>
    public void Unfix()
    {
        if (_fixter != null)
        {
            _fixter.Delete();
        }
    }

    /// <summary>
    /// Устанавливает тело элемента, использовать после Replacement.
    /// </summary>
    public void SetBody()
    {
        Body bb = null;
        BodyCollection bc = ((Part)ElementComponent.Prototype).Bodies;
        Logger.WriteLine("У объекта \"" + ElementComponent.Name + " " + ElementComponent +
                         "\" " + bc.ToArray().Length + " тел(о).");

        foreach (Body body in bc)
        {
            NXObject tmpNxObject = ElementComponent.FindOccurrence(body);
            if (tmpNxObject != null)
            {
                bb = (Body)tmpNxObject;
                break;
            }
        }

        _body = bb;
    }

    public void ClearConstraints()
    {
        ComponentConstraint[] constraints = ElementComponent.GetConstraints();
        foreach (ComponentConstraint componentConstraint in constraints)
        {
            NxFunctions.Delete(componentConstraint);
        }
    }

    protected void ClearConstraint(Constraint.Type type, string name)
    {
        ComponentConstraint[] constraints = ElementComponent.GetConstraints();
        foreach (ComponentConstraint componentConstraint in constraints)
        {
            if (componentConstraint.ConstraintType == type && componentConstraint.Name == name)
            {
                NxFunctions.Delete(componentConstraint);
            }
        }
    }

}

