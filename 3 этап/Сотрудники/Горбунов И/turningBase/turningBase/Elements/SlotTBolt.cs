using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс Т-образных болтов.
/// </summary>
public class SlotTBolt : UspElement
{

    private Face _tunnelFace, _bottomFace;
    private Face[] _sideFaces;

    /// <summary>
    ///Инициализирует новый экземпляр класса базовой плиты УСП для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент из сборки NX.</param>
    public SlotTBolt(Component component)
        : base(component)
    {
        SetFixtureFaces();
    }

    public void SetInSlot(Slot slot)
    {
        SetConstraints(slot);
        MoveBolt(slot);
    }

    private void SetConstraints(Slot slot)
    {
        bool isFixed = slot.ParentComponent.IsFixed;
        if (!isFixed)
        {
            slot.SlotSet.UspElement.Fix();
            NxFunctions.Update();
        }

        Center center = new Center();
        center.Create(slot, _sideFaces[0], _sideFaces[1]);
        Touch touch = new Touch();
        touch.Create(_bottomFace, slot.BottomFace);
        NxFunctions.Update();

        if (isFixed) 
            return;
        slot.SlotSet.UspElement.Unfix();
        NxFunctions.Update();
    }

    private void MoveBolt(Slot slot)
    {
        Surface bottom = new Surface(_bottomFace);
        Point3d boltPoint = bottom.CenterPoint;
        Vector direction = new Vector(boltPoint, slot.CenterPoint);
        Movement.MoveByDirection(ElementComponent, direction);
    }

    /// <summary>
    /// Устанавливает НГП, использовать после Replacement.
    /// </summary>
    public void SetTopSlotFace()
    {

    }

    private void SetFixtureFaces()
    {
        bool bottomIsSet = false, tunnelIsSet = false;

        Face[] faces = Body.GetFaces();
        foreach (Face face in faces)
        {
            switch (face.SolidFaceType)
            {
                case Face.FaceType.Cylindrical:
                    {
                        _tunnelFace = face;
                        tunnelIsSet = true;
                        Logger.WriteLine("Цилиндрическая грань крепления болта - " + face);
                    }
                    break;
                case Face.FaceType.Planar:
                    {
                        if (IsBottomFace(face))
                        {
                            _bottomFace = face;
                            Logger.WriteLine("Нижняя грань крепления болта - " + face);
                            _sideFaces = GetSideFaces(_bottomFace);
                            bottomIsSet = true;
                        }
                    }
                    break;
            }

            if (tunnelIsSet && bottomIsSet)
            {
                break;
            }
        }
    }

    bool IsBottomFace(Face face)
    {
        Edge[] edges = face.GetEdges();
        //4 стороны - 4 ребра
        if (edges.Length != 4)
        {
            return false;
        }

        for (int i = 0; i < edges.Length; i++)
        {
            if (Config.Round(edges[i].GetLength()) != UspCatalog.SlotBoltWidth) continue;

            for (int j = 0; j < edges.Length; j++)
            {
                if (Config.Round(edges[j].GetLength()) == UspCatalog.SlotBoltLength)
                {
                    return true;
                }
            }
            return false;
        }
        return false;
    }

    Face[] GetSideFaces(Face bottomFace)
    {
        int i = 0;
        Face[] sideFaces = new Face[2];

        Edge[] edges = bottomFace.GetEdges();
        foreach (Edge edge in edges)
        {
            if (Config.Round(edge.GetLength()) != UspCatalog.SlotBoltLength) continue;

            Face[] faces = edge.GetFaces();
            foreach (Face face in faces)
            {
                if (face == bottomFace) continue;

                sideFaces[i++] = face;
            }
        }
        return sideFaces;
    }
}
