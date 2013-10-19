using System;
using System.Collections.Generic;
using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс содержащий элемент УСП.
/// </summary>
public class UspElement
{
    /// <summary>
    /// Возвращает компонент элемента.
    /// </summary>
    public Component ElementComponent
    {
        get
        {
            return _component;
        }
    }
    /// <summary>
    /// Возвращает список всех нижних плоскостей пазов для данного элемента.
    /// </summary>
    public List<Face> SlotFaces
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
            return _catalog;
        }
    }
    /// <summary>
    /// Возвращает обозначение элемента УСП.
    /// </summary>
    private string Title
    {
        get { return GetTitle(); }
    }

    private readonly Component _component;
    private Fix _fixter;
    private Body _body;
    private Catalog _catalog;

    List<Face> _bottomFaces;

    /// <summary>
    /// Инициализирует новый экземпляр класса элемента УСП для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент из сборки NX.</param>
    public UspElement(Component component)
    {
        _component = component;

        SetBody();
        SetBottomFaces();
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
        return null;
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
        return null;
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
        BodyCollection bc = ((Part)_component.Prototype).Bodies;
        Logger.WriteLine("У объекта \"" + ElementComponent.Name + " " + ElementComponent +
                         "\" " + bc.ToArray().Length + " тел(о).");
        foreach (Body body in bc)
        {
            NXObject tmpNxObject = _component.FindOccurrence(body);
            if (tmpNxObject != null)
            {
                bb = (Body)tmpNxObject;
                break;
            }
        }

        _body = bb;
    }

    private string GetTitle()
    {
        string componentName = ElementComponent.Name;
        string[] split = componentName.Split('_');
        string nameWithEx = split[split.Length - 1];
        split = nameWithEx.Split('.');
        return split[0];
    }

    
}

