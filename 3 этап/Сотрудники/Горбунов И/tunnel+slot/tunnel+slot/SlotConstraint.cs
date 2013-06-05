using System;
using System.Collections.Generic;
using System.Text;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс для наложения связей пазирования.
/// </summary>
class SlotConstraint
{
    TouchConstraint touchConstr;
    CenterConstraint centerConstr;

    Slot firstSlot, secondSlot;

    /// <summary>
    /// Инициализирует новый экземпляр класса связей для пазирования.
    /// </summary>
    /// <param name="firstSlot">Первый паз.</param>
    /// <param name="secondSlot">Второй паз.</param>
    public SlotConstraint(Slot firstSlot, Slot secondSlot)
    {
        this.firstSlot = firstSlot;
        this.secondSlot = secondSlot;

        this.touchConstr = new TouchConstraint();
        this.centerConstr = new CenterConstraint();
    }

    /// <summary>
    /// Производит соединение вдоль паза.
    /// </summary>
    public void setCenterConstraint()
    {
        centerConstr.create(this.firstSlot.ParentComponent,
                            this.firstSlot.SideFace1, this.firstSlot.SideFace2,
                            this.secondSlot.ParentComponent,
                            this.secondSlot.SideFace1, this.secondSlot.SideFace2);

        Config.theUFSession.Modl.Update();
    }

    /*void setLongConstraint(Slot slot1, Slot slot2)
    {
        this.createLong(slot1, slot2);
        this.createBottom(slot1, slot2);

        slot1.setTouchFace();
        slot2.setTouchFace();

        this.createSideTouch(slot1, slot2);
    }*/

    /// <summary>
    /// Производит соединение двух пазов.
    /// </summary>
    /// <param name="slot1">Первый паз.</param>
    /// <param name="slot2">Второй паз.</param>
    public void setEachOtherConstraint(Slot slot1, Slot slot2)
    {
        //this.createLong(slot1, slot2);

        slot1.findTopFace();
        slot2.findTopFace();

        //this.createTopTouch(slot1, slot2);
    }



    /*void escapeOverConstrained(ComponentConstraint constrain)
    {
        Constraint.SolverStatus status = constrain.GetConstraintStatus();
        if (status == Constraint.SolverStatus.OverConstrained)
        {
            this.reverse();

            executeConstraints();
        }
    }*/
}

