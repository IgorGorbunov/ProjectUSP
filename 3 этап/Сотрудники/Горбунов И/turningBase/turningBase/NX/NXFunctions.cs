﻿using System.Collections.Generic;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;
using NXOpen.UF;
using NXOpen.Utilities;

internal static class NxFunctions
{
    public static IEnumerable<Edge> GetParallelEdges(Edge[] edges, Edge parallelEdge, double distanceBetween)
    {
        for (int i = 0; i < edges.Length; i++)
        {
            Vector parallelVector = new Vector(parallelEdge);
            if (edges[i].SolidEdgeType != Edge.EdgeType.Linear)
                continue;
            Vector vector1 = new Vector(edges[i]);
            for (int j = i + 1; j < edges.Length; j++)
            {
                if (edges[j].SolidEdgeType != Edge.EdgeType.Linear)
                    continue;
                Vector vector2 = new Vector(edges[j]);
                if (!vector1.IsParallel(parallelVector))
                    continue;
                if (!vector1.IsParallel(vector2))
                    continue;
                Straight straight = new Straight(vector2);
                if (straight.GetDistance(vector1.Start) == 0)
                    continue;
                if (!Geom.IsEqual(vector1.GetStartsLength(vector2), distanceBetween)) 
                    continue;
                Edge[] parallelEdges = new Edge[2];
                parallelEdges[0] = edges[i];
                parallelEdges[1] = edges[j];
                return parallelEdges;
            }
        }
        return null;
    }

    /// <summary>
    /// Обновление сессии (как правило нужно при создании связей).
    /// </summary>
    public static void Update()
    {
        Config.TheUfSession.Modl.Update();
    }

    /// <summary>
    /// Заморозить экран.
    /// </summary>
    public static void FreezeDisplay()
    {
#if(!DEBUG)
        Config.TheUfSession.Disp.SetDisplay(UFConstants.UF_DISP_SUPPRESS_DISPLAY);
#endif
    }
    /// <summary>
    /// Разморозить экран.
    /// </summary>
    public static void UnFreezeDisplay()
    {
        int displayCode;
        Config.TheUfSession.Disp.AskDisplay(out displayCode);

        Config.TheUfSession.Disp.SetDisplay(UFConstants.UF_DISP_UNSUPPRESS_DISPLAY);

        if (displayCode == UFConstants.UF_DISP_SUPPRESS_DISPLAY)
        {
            Config.TheUfSession.Disp.RegenerateDisplay();
        }
    }
    /// <summary>
    /// Удаляет NX-объект.
    /// </summary>
    /// <param name="objectNx">NX-объект</param>
    public static void Delete(NXObject objectNx)
    {
        Session.UndoMarkId markId =
            Config.TheSession.SetUndoMark(Session.MarkVisibility.Invisible, "Delete");

        Config.TheSession.UpdateManager.AddToDeleteList(objectNx);
        Config.TheSession.UpdateManager.DoUpdate(markId);
    }
    /// <summary>
    /// Удаляет компоненты из NX.
    /// </summary>
    /// <param name="components">Компоненты.</param>
    public static void Delete(IEnumerable<Component> components)
    {
        foreach (Component component in components)
        {
            ComponentConstraint[] constraints = component.GetConstraints();
            foreach (ComponentConstraint componentConstraint in constraints)
            {
                Delete(componentConstraint);
            }
            Delete((NXObject)component);
        }
    }
    /// <summary>
    /// Удаляет компоненты из NX.
    /// </summary>
    /// <param name="components"></param>
    public static void Delete(params Component[] components)
    {
        IEnumerable<Component> iComponents = components;
        Delete(iComponents);
    }

    public static void SetAsterix(Point3d point)
    {
        UFObj.DispProps props = new UFObj.DispProps();
        props.blank_status = UFConstants.UF_OBJ_NOT_BLANKED;
        props.color = 186;//RED
        props.font = UFConstants.UF_OBJ_FONT_SOLID;
        props.highlight_status = true;
        props.layer = 1;
        props.line_width = UFConstants.UF_OBJ_WIDTH_NORMAL;

        double[] position = new double[3];
        position[0] = point.X;
        position[1] = point.Y;
        position[2] = point.Z;

        Config.TheUfSession.Disp.DisplayTemporaryPoint(Tag.Null, UFDisp.ViewType.UseActiveMinus, position, ref props, UFDisp.PolyMarker.FilledCircle);
    }

    public static void SetAsterix(double[] point)
    {
        SetAsterix(new Point3d(point[0], point[1], point[2]));
    }

    public static void SetAsterix(double coord1, double coord2, double coord3)
    {
        SetAsterix(new double[]{coord1, coord2, coord3});
    }

    public static void SetAsterix(Vertex vertex)
    {
        SetAsterix(vertex.Point);
    }

    /// <summary>
    /// Возвращает непогашенный элемент УСП, к которому принадлежит заданная точка.
    /// </summary>
    /// <param name="point">Точка НА компоненте.</param>
    /// <returns></returns>
    public static SingleElement GetUnsuppressElement(Point3d point)
    {
        PartCollection collection = Config.TheSession.Parts;
        foreach (Part part in collection)
        {
            Tag[] occurences;
            Config.TheUfSession.Assem.AskOccsOfPart(Config.WorkPart.Tag, part.Tag, out occurences);

            foreach (Tag tag in occurences)
            {
                Component component = (Component)NXObjectManager.Get(tag);
                if (component.IsBlanked) continue;

                SingleElement element = new SingleElement(component);

                double[] surrCoords = new double[3];
                surrCoords[0] = point.X;
                surrCoords[1] = point.Y;
                surrCoords[2] = point.Z;

                int status;
                Config.TheUfSession.Modl.AskPointContainment(surrCoords, element.Body.Tag, out status);

                if (status == 3)
                {
                    return element;
                }
            }
        }
        return null;
    }
    /// <summary>
    /// Создаёт сопряжения фиксирования на заданных одномодельных элементах УСП, 
    /// если таких сопряжений не было до вызова метода и возвращает список тех элементов 
    /// на которые данные сопряжения были наложены в результате выполнения данного метода.
    /// </summary>
    /// <param name="element1">Первый одномодельный элемент УСП.</param>
    /// <param name="element2">Второй одномодельный эелмент УСП.</param>
    /// <returns></returns>
    public static IEnumerable<SingleElement> FixElements(SingleElement element1, SingleElement element2)
    {
        SingleElement fixElement1 = null;
        if (element1 != null)
        {
            bool firstElementIsFixed = element1.ElementComponent.IsFixed;
            if (!firstElementIsFixed)
            {
                fixElement1 = new SingleElement(element1.ElementComponent);
                fixElement1.Fix();
            }
        }

        SingleElement fixElement2 = null;
        if (element2 != null)
        {
            bool secondElementIsFixed = element2.ElementComponent.IsFixed;
            if (!secondElementIsFixed)
            {
                fixElement2 = new SingleElement(element2.ElementComponent);
                fixElement2.Fix();
            }
        }
        Update();
        SingleElement[] array = { fixElement1, fixElement2 };
        return array;
    }

    public static IEnumerable<SingleElement> FixElements(Component component1, Component component2)
    {
        SingleElement element1 = new SingleElement(component1);
        SingleElement element2 = new SingleElement(component2);
        return FixElements(element1, element2);
    }

    public static void Unfix(IEnumerable<SingleElement> uspElements)
    {
        if (uspElements == null)
            return;
        foreach (SingleElement uspElement in uspElements)
        {
            if (uspElement != null)
            {
                uspElement.Unfix();
            }
        }
        Update();
    }

}
