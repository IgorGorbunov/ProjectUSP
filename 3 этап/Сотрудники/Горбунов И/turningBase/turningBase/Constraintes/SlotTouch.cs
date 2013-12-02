using System.Collections.Generic;
using NXOpen;

/// <summary>
/// Класс для наложения связей пазирования.
/// </summary>
class SlotTouch
{
    Touch _touchConstr;

    readonly Slot _firstSlot;
    readonly Slot _secondSlot;

    Session.UndoMarkId _markId1;

    /// <summary>
    /// Инициализирует новый экземпляр класса связей для пазирования.
    /// </summary>
    /// <param name="firstSlot">Первый паз.</param>
    /// <param name="secondSlot">Второй паз.</param>
    public SlotTouch(Slot firstSlot, Slot secondSlot)
    {
        _firstSlot = firstSlot;
        _secondSlot = secondSlot;
    }


    /// <summary>
    /// Производит соединение двух деталей по ортогональным пазам плоскостям.
    /// </summary> 
    public void SetTouchFaceConstraint()
    {
        KeyValuePair<Face, double>[] pairs1 = _firstSlot.OrtFaces;
        KeyValuePair<Face, double>[] pairs2 = _secondSlot.OrtFaces;

        ElementIntersection intersect = new ElementIntersection(_firstSlot.Body, _secondSlot.Body);
        for (int i = 0; i < pairs2.Length; i++)
        {
            for (int j = 0; j < pairs1.Length; j++)
            {
                _markId1 = Config.TheSession.SetUndoMark(Session.MarkVisibility.Invisible,
                                                                "SetTouch");

                _touchConstr = new Touch();
                _touchConstr.Create(pairs1[j].Key, pairs2[i].Key);
                MoveToEachOther(pairs1[j].Key, pairs2[i].Key);

                if (intersect.TouchExists)
                {
                    goto End;
                }
                Config.TheSession.UndoToMark(_markId1, "SetTouch");
            }
        }
    End: { }
        NxFunctions.Update();
    }

    private void MoveToEachOther(Face face1, Face face2)
    {
        Point3d p1 = _firstSlot.CenterPoint;
        Point3d p2 = _secondSlot.CenterPoint;

        Surface pl1 = new Surface(face1);
        Surface pl2 = new Surface(face2);

        Point3d projection1 = pl1.GetProection(p1);
        Point3d projection2 = pl2.GetProection(p2);

        Vector vec = new Vector(projection2, projection1);
        Movement.MoveByDirection(_secondSlot.ParentComponent, vec);
    }
}

