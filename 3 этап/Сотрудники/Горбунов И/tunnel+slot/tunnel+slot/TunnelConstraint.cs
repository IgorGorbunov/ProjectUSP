using System;
using System.Collections.Generic;
using System.Text;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс для создания связей между двумя базовыми отверстиями.
/// </summary>
public class TunnelConstraint
{
    TouchAxeConstraint axeConstr;
    TouchConstraint touchConstr;

    NXOpen.Session.UndoMarkId markId1;
    Face correctFace1, correctFace2;

    Tunnel firstTunnel, secondTunnel;

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
    /// <param name="first">True, если необходимо перевернуть первый элемент.</param>
    /// <param name="second">True, если необходимо перевернуть второй элемент.</param>
    public void setTouchFaceConstraint(bool firstRev, bool secondRev)
    {
        KeyValuePair<Face, double>[] pairs1 = this.firstTunnel.getOrtFacePairs(firstRev);
        KeyValuePair<Face, double>[] pairs2 = this.secondTunnel.getOrtFacePairs(secondRev);
        ElementIntersection intersect = new ElementIntersection(firstTunnel.Body, secondTunnel.Body);


        for (int i = 0; i < pairs2.Length; i++)
        {
            for (int j = 0; j < pairs1.Length; j++)
            {
                markId1 = Config.theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "SetTouch");

                touchConstr.create(this.firstTunnel.ParentComponent, pairs1[j].Key,
                                   this.secondTunnel.ParentComponent, pairs2[i].Key);

                if (intersect.TouchExists)
                {
                    goto End;
                }

                //последний проход - касание в любом случае
                if (i == (pairs2.Length - 1) && j == (pairs1.Length - 1))
                {
                    correctFace1 = pairs1[j].Key;
                    correctFace2 = pairs2[i].Key;
                }

                Config.theSession.UndoToMark(markId1, "SetTouch");
            }
        }

        //if (intersect.InterferenseExists)
        //{
        //    this.reverseAfterItersect();
        //}


    End: { }     


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
        axeConstr.reverse();

        setTouchFaceConstraint(first, second);
    }

}

