using System;
using System.Collections.Generic;
using System.Text;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;
using NXOpen.UF;

/// <summary>
/// Класс для создания связей между двумя базовыми отверстиями.
/// </summary>
public class TunnelConstraint
{
    TouchAxeConstraint axeConstr;
    TouchConstraint touchConstr;

    NXOpen.Session.UndoMarkId markId1;

    Tunnel firstTunnel, secondTunnel;
    Slot slot;

    /// <summary>
    /// Инициализирует новый экземпляр класса связей для соединения двух отверстий.
    /// </summary>
    /// <param name="firstTunnel">Первое базовое отверстие.</param>
    /// <param name="secondTunnel">Второе базовое отверстие.</param>
    public TunnelConstraint(Tunnel firstTunnel, Tunnel secondTunnel)
    {
        axeConstr = new TouchAxeConstraint();
        touchConstr = new TouchConstraint();

        this.firstTunnel = firstTunnel;
        this.secondTunnel = secondTunnel;
        this.slot = null;
    }

    public TunnelConstraint(Tunnel firstTunnel, Slot slot)
    {
        axeConstr = new TouchAxeConstraint();
        touchConstr = new TouchConstraint();

        this.firstTunnel = firstTunnel;
        this.secondTunnel = null;
        this.slot = slot;
    }

    /// <summary>
    /// Производит соединение двух деталей с отверстиями по оси.
    /// </summary>
    public void setTouchAxeConstraint()
    {
        axeConstr.create(this.firstTunnel.ParentComponent, firstTunnel.TunnelFace,
                         this.secondTunnel.ParentComponent, secondTunnel.TunnelFace);
    }
    /// <summary>
    /// Производит соединение двух деталей с отверстиями по ортогональным плоскостям.
    /// </summary>
    /// <param name="firstRev">True, если необходимо перевернуть первый элемент.</param>
    /// <param name="secondRev">True, если необходимо перевернуть второй элемент.</param>
    public void setTouchFaceConstraint(bool firstRev, bool secondRev)
    {
        KeyValuePair<Face, double>[] pairs1 = this.firstTunnel.getOrtFacePairs(firstRev);
        KeyValuePair<Face, double>[] pairs2;
        ElementIntersection intersect;

        Component comp2;
        if (slot == null)
        {
            pairs2 = this.secondTunnel.getOrtFacePairs(secondRev);
            intersect = new ElementIntersection(firstTunnel.Body, secondTunnel.Body);
            comp2 = this.secondTunnel.ParentComponent;
        }
        else
        {
            pairs2 = this.slot.OrtFaces;
            intersect = new ElementIntersection(this.firstTunnel.Body, this.slot.Body);
            comp2 = this.slot.ParentComponent;
        }
        Config.FreezeDisplay();

        for (int i = 0; i < pairs2.Length; i++)
        {
            for (int j = 0; j < pairs1.Length; j++)
            {
                markId1 = Config.TheSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "SetTouch");

                touchConstr.create(this.firstTunnel.ParentComponent, pairs1[j].Key,
                                   comp2, pairs2[i].Key);

                if (intersect.TouchExists)
                {
                    goto End;
                }
                Config.TheSession.UndoToMark(markId1, "SetTouch");
            }
        }
    End: { }

        Config.UnFreezeDisplay();
        Config.TheUfSession.Modl.Update();
    }

    /// <summary>
    /// Производит реверс вдоль отверстия.
    /// </summary>
    //public void reverseAfterItersect()
    //{
    //    //axeConstr.reverse();
    //    Config.theUI.NXMessageBox.Show("Tst", NXMessageBox.DialogType.Error, "!!");
    //    touchConstr.delete();

    //    setTouchFaceConstraint(true, true);
    //    Config.theUFSession.Modl.Update();
    //}

    /// <summary>
    /// Производит реверс соединения базовых отверстий для одного/обоих элементов.
    /// </summary>
    /// <param name="first">True, если необходим реверс первого элемента.</param>
    /// <param name="second">True, если необходим реверс второго элемента.</param>
    public void reverse(bool first, bool second)
    {
        touchConstr.delete();
        axeConstr.Reverse();

        setTouchFaceConstraint(first, second);
    }

}

