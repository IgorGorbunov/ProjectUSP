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
    ComponentConstraint constr;

    ComponentPositioner componentPositioner;
    ComponentNetwork componentNetwork;


    /// <summary>
    /// Инициализирует новый экземпляр класса связей для соединения двух отверстий.
    /// </summary>
    /// <param name="constr">Констрэйнт по двум боковым граням паза.</param>
    public TunnelConstraint()
    {
        this.initConstraints();
    }

    /// <summary>
    /// Производит соединение двух деталей с отверстиями.
    /// </summary>
    /// <param name="firstTunnel">Первое отверстие.</param>
    /// <param name="secondTunnel">Второе отверстие.</param>
    public void setEachOtherConstraint(Tunnel firstTunnel, Tunnel secondTunnel)
    {
        this.createConstr(firstTunnel, secondTunnel);
    }

    /// <summary>
    /// Производит реверс констрэйнта.
    /// </summary>
    public void reverse()
    {
        constr.FlipAlignment();
        this.executeConstraints();
    }

    void createConstr(Tunnel firstTunnel, Tunnel secondTunnel)
    {
        constr = (ComponentConstraint)componentPositioner.CreateConstraint();
        constr.ConstraintAlignment = Constraint.Alignment.ContraAlign;
        constr.ConstraintType = NXOpen.Positioning.Constraint.Type.Touch;

        Component component1 = firstTunnel.ParentComponent;
        ConstraintReference constraintReference1 =
            constr.CreateConstraintReference(component1,
                                             firstTunnel.TunnelFace, true, false, false);

        Component component2 = secondTunnel.ParentComponent;
        ConstraintReference constraintReference3 =
            constr.CreateConstraintReference(component2,
                                             secondTunnel.TunnelFace, true, false, false);

        executeConstraints();
    }

    void executeConstraints()
    {
        componentNetwork.Solve();
        Config.theUFSession.Modl.Update();
    }

    void initConstraints()
    {
        componentPositioner = Config.workPart.ComponentAssembly.Positioner;

        componentNetwork = (ComponentNetwork)componentPositioner.EstablishNetwork();
        componentNetwork.MoveObjectsState = true;
    }
}

