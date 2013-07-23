using System.Collections.Generic;
using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс для создания связей между двумя базовыми отверстиями.
/// </summary>
public class TunnelConstraint
{
    Session.UndoMarkId _markId1;

    readonly Tunnel _firstTunnel;
    readonly Tunnel _secondTunnel;
    readonly Slot _slot;
    private UspElement _fixture;

    readonly TouchAxe _axeConstr;
    readonly Touch _touchConstr;

    /// <summary>
    /// Инициализирует новый экземпляр класса связей для соединения двух отверстий.
    /// </summary>
    /// <param name="firstTunnel">Первое базовое отверстие.</param>
    /// <param name="secondTunnel">Второе базовое отверстие.</param>
    public TunnelConstraint(Tunnel firstTunnel, Tunnel secondTunnel)
    {
        _axeConstr = new TouchAxe();
        _touchConstr = new Touch();

        _firstTunnel = firstTunnel;
        _secondTunnel = secondTunnel;
        _slot = null;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса связей для touch-соединения с проверкой на
    /// пересечение отверстие-паз.
    /// </summary>
    /// <param name="firstTunnel">Отверстие.</param>
    /// <param name="slot">Паз.</param>
    /// <param name="fixture">Крепеж.</param>
    public TunnelConstraint(Tunnel firstTunnel, Slot slot, Component fixture)
    {
        _axeConstr = new TouchAxe();
        _touchConstr = new Touch();

        _firstTunnel = firstTunnel;
        _secondTunnel = null;
        _slot = slot;
        _fixture = new UspElement(fixture);
    }

    /// <summary>
    /// Производит соединение двух деталей с отверстиями по оси.
    /// </summary>
    public void SetTouchAxeConstraint()
    {
        _axeConstr.Create(_firstTunnel.ParentComponent, _firstTunnel.TunnelFace,
                         _secondTunnel.ParentComponent, _secondTunnel.TunnelFace);
    }
    /// <summary>
    /// Производит соединение двух деталей с отверстиями по ортогональным плоскостям.
    /// </summary>
    /// <param name="firstRev">True, если необходимо перевернуть первый элемент.</param>
    /// <param name="secondRev">True, если необходимо перевернуть второй элемент.</param>
    public void SetTouchFaceConstraint()
    {
        KeyValuePair<Face, double>[] pairs1 = _firstTunnel.GetOrtFacePairs();
        KeyValuePair<Face, double>[] pairs2;
        ElementIntersection intersect, fixIntersect = null;

        Component comp2;
        if (_slot == null)
        {
            pairs2 = _secondTunnel.GetOrtFacePairs();
            intersect = new ElementIntersection(_firstTunnel.Body, _secondTunnel.Body);
            comp2 = _secondTunnel.ParentComponent;
        }
        else
        {
            pairs2 = _slot.OrtFaces;
            intersect = new ElementIntersection(_firstTunnel.Body, _slot.SlotSet.Body);
            fixIntersect = new ElementIntersection(_firstTunnel.Body, _fixture.Body);
            comp2 = _slot.ParentComponent;
        }

        for (int i = 0; i < pairs2.Length; i++)
        {
            for (int j = 0; j < pairs1.Length; j++)
            {
                _markId1 = Config.TheSession.SetUndoMark(Session.MarkVisibility.Invisible, 
                                                                "SetTouch");

                _touchConstr.Create(_firstTunnel.ParentComponent, pairs1[j].Key,
                                   comp2, pairs2[i].Key);

                if (intersect.TouchExists && !fixIntersect.InterferenseExists)
                {
                    goto End;
                }
                Config.TheSession.UndoToMark(_markId1, "SetTouch");
            }
        }
    End: { }

    }


}

