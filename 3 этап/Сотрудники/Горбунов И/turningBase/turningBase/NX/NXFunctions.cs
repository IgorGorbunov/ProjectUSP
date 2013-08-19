using NXOpen;
using NXOpen.UF;

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
        Config.TheUfSession.Disp.SetDisplay(UFConstants.UF_DISP_SUPPRESS_DISPLAY);
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

    public static void Delete(NXObject objectNx)
    {
        Session.UndoMarkId markId =
            Config.TheSession.SetUndoMark(Session.MarkVisibility.Invisible, "Delete");

        Config.TheSession.UpdateManager.AddToDeleteList(objectNx);
        Config.TheSession.UpdateManager.DoUpdate(markId);
    }

}
