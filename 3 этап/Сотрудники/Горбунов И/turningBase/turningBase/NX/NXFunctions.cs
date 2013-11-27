using System.Collections.Generic;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;
using NXOpen.UF;
using NXOpen.Utilities;

internal static class NxFunctions
{
    public static bool GetFace(TaggedObject[] to, UspElement element, out Face face)
    {
        TaggedObject t = to[0];

        Part p = (Part) element.ElementComponent.OwningPart;
        BodyCollection bc = p.Bodies;
        foreach (Body b in bc)
        {
            Face[] fc = b.GetFaces();

            foreach (Face f in fc)
            {
                if (f.Tag != t.Tag) continue;
                face = f;
                return true;
            }
        }

        face = null;
        return false;
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
            Delete(component);
        }
    }

    private static void SetAsterix(Point3d point)
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

    private static void SetAsterix(double[] point)
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
    public static UspElement GetUnsuppressElement(Point3d point)
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

                UspElement element = new UspElement(component);

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

}
