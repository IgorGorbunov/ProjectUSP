using System.Collections.Generic;
using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс содержащий элемент УСП.
/// </summary>
public class GroupElement : UadElement
{

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
    /// Возвращает ГОСТ элемента УСП.
    /// </summary>
    public string Gost
    {
        get { return SqlUspElement.GetGost(Title); }
    }

    private List<SingleElement> _children;
    private Catalog _catalog;


    /// <summary>
    /// Инициализирует новый экземпляр класса элемента УСП для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент из сборки NX.</param>
    protected GroupElement(Component component) : base(component)
    {
        SetChildren();
    }

    /// <summary>
    /// Возвращает ребро элемента по его имени.
    /// </summary>
    /// <param name="edgeName">Обозначение ребра.</param>
    /// <param name="component">Компонент, в котором найдено ребро.</param>
    /// <returns></returns>
    protected Edge GetEdge(string edgeName, out Component component)
    {
        Edge edge = null;
        component = null;
        foreach (SingleElement singleElement in _children)
        {
            try
            {
                edge = singleElement.GetEdge(edgeName);
            }
            catch (ParamObjectNotFoundExeption)
            {
                Logger.WriteLine("В элементе '" + singleElement.Title + "' сборочного элемента '" +
                    Title + "' ребро '" + edgeName + "' не найдено.");
            }
            if (edge == null) 
                continue;
            component = singleElement.ElementComponent;
            return edge;
        }
        string mess = "В сборочном элементе '" + Title + "' ребро '" +
                      edgeName + "' не найдено.";
        throw new ParamObjectNotFoundExeption(mess, this, edgeName);
    }

    /// <summary>
    /// Возвращает грань элемента по его имени.
    /// </summary>
    /// <param name="faceName">Обозначение ребра.</param>
    /// <param name="component">Компонент, в котором найдена грань.</param>
    /// <returns></returns>
    protected Face GetFace(string faceName, out Component component)
    {
        Face face = null;
        component = null;
        foreach (SingleElement singleElement in _children)
        {
            try
            {
                face = singleElement.GetFace(faceName);
            }
            catch (ParamObjectNotFoundExeption)
            {
                Logger.WriteLine("В элементе '" + singleElement.Title + "' сборочного элемента '" +
                    Title + "' грань '" + faceName + "' не найдена.");
            }
            if (face == null)
                continue;
            component = singleElement.ElementComponent;
            return face;
        }
        string mess = "В сборочном элементе '" + Title + "' грань '" +
                      faceName + "' не найдена.";
        throw new ParamObjectNotFoundExeption(mess, this, faceName);
    }


    private void SetChildren()
    {
        Component[] components = ElementComponent.GetChildren();
        _children = new List<SingleElement>();
        foreach (Component component in components)
        {
            _children.Add(new SingleElement(component, this));
        }
    }

}