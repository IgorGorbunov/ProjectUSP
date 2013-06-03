using System;
using System.Collections.Generic;
using System.Text;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс для создания различный связей и ограничений.
/// </summary>
public class Constrainter
{
    protected ComponentConstraint constr;
    protected ComponentPositioner componentPositioner;

    ComponentNetwork componentNetwork;

    protected Constrainter()
    {
        this.initConstraints();
    }

    /// <summary>
    /// Производит реверс констрэйнта.
    /// </summary>
    public virtual void reverse()
    {
        constr.FlipAlignment();
        this.executeConstraints();
    }

    protected void initConstraints()
    {
        componentPositioner = Config.workPart.ComponentAssembly.Positioner;

        componentNetwork = (ComponentNetwork)componentPositioner.EstablishNetwork();
        componentNetwork.MoveObjectsState = true;
    }
    protected void executeConstraints()
    {
        componentNetwork.Solve();
        //Config.theUFSession.Modl.Update();
    }


    
}

