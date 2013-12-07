using System;
using System.Collections.Generic;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс содержащий элемент УСП.
/// </summary>
public class GroupElement : UadElement
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
    public override string Title
    {
        get { return _title; }
    }
    /// <summary>
    /// Возвращает ГОСТ элемента УСП.
    /// </summary>
    public string Gost
    {
        get { return SqlUspElement.GetGost(Title); }
    }


    private readonly Component _component;
    private List<SingleElement> _children;

    private readonly string _title;
    private Catalog _catalog;


    /// <summary>
    /// Инициализирует новый экземпляр класса элемента УСП для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент из сборки NX.</param>
    protected GroupElement(Component component)
    {
        _component = component;
        SetChildren();

        //вдргу компонент побьётся
        _title = GetTitle();
    }

    /// <summary>
    /// Возвращает ребро элемента по его имени.
    /// </summary>
    /// <param name="edgeName">Обозначение ребра.</param>
    /// <param name="component"></param>
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
                Logger.WriteLine("В элементе '", singleElement.Title, "' сборочного элемента '",
                    Title, "' ребро '", edgeName, "' не найдено.");
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


    private void SetChildren()
    {
        Component[] components = _component.GetChildren();
        _children = new List<SingleElement>();
        foreach (Component component in components)
        {
            _children.Add(new SingleElement(component, this));
        }
    }

    private string GetTitle()
    {
        string componentName = ElementComponent.Name;
        string[] split = componentName.Split('_');
        string nameWithEx = split[split.Length - 1];
        split = nameWithEx.Split('.');
        string[] nameWithRevision = split[0].Split(' ');
        return nameWithRevision[0];
    }

}

