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

    /// <summary>
    /// Инициализирует новый экземпляр класса связей для соединения двух отверстий.
    /// </summary>
    /// <param name="constr">Констрэйнт по двум боковым граням паза.</param>
    public TunnelConstraint()
    {
        axeConstr = new TouchAxeConstraint();
    }

    /// <summary>
    /// Производит соединение двух деталей с отверстиями.
    /// </summary>
    /// <param name="firstTunnel">Первое отверстие.</param>
    /// <param name="secondTunnel">Второе отверстие.</param>
    public void setEachOtherConstraint(Tunnel firstTunnel, Tunnel secondTunnel)
    {
        axeConstr.create(firstTunnel.ParentComponent, firstTunnel.TunnelFace,
                         secondTunnel.ParentComponent, secondTunnel.TunnelFace);
    }

    /// <summary>
    /// Производит реверс вдоль отверстия.
    /// </summary>
    public void reverse()
    {
        axeConstr.reverse();
    }

}

