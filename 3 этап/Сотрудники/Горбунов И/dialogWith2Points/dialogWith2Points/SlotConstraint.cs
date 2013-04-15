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
    ComponentConstraint long_constraint, touch_constraint;

    ComponentPositioner componentPositioner;
    ComponentNetwork componentNetwork;

    /// <summary>
    /// Инициализирует новый экземпляр класса связей для центрирования по пазу и касания плоскостями.
    /// </summary>
    /// <param name="long_slot">Констрэйнт по двум боковым граням паза.</param>
    /// <param name="touch">Констрэйнт на касание.</param>
    public SlotConstraint(ComponentConstraint long_slot, ComponentConstraint touch)
    {
        this.long_constraint = long_slot;
        this.touch_constraint = touch;

        this.initConstraints();
    }
    /// <summary>
    /// Инициализирует новый пустой экземпляр класса связей.
    /// </summary>
    public SlotConstraint()
        : this(null, null)
    {

    }

    /*public void removeTouch()
    {
        NXObject object_to_delete = touch_constraint;
        Config.theSession.UpdateManager.AddToDeleteList(object_to_delete);
    }*/

    /// <summary>
    /// Производит реверс констрэйнта ЦЕНТР вдоль паза.
    /// </summary>
    public void reverse()
    {
        long_constraint.FlipAlignment();
        this.executeConstraints();
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
        this.createLong(slot1, slot2);

        slot1.findTopFace();
        slot2.findTopFace();

        this.createTopTouch(slot1, slot2);
    }

    /*void createBottom(Slot slot1, Slot slot2)
    {
        bottom_constraint = (ComponentConstraint)componentPositioner.CreateConstraint();
        bottom_constraint.ConstraintAlignment = Constraint.Alignment.CoAlign;
        bottom_constraint.ConstraintType = NXOpen.Positioning.Constraint.Type.Touch;

        Component component1 = slot1.ParentComponent;
        ConstraintReference constraintReference1 =
            bottom_constraint.CreateConstraintReference(component1,
                                                        slot1.BottomFace, false, false, false);

        Component component2 = slot2.ParentComponent;
        ConstraintReference constaintReference3 =
            bottom_constraint.CreateConstraintReference(component2,
                                                        slot2.BottomFace, false, false, false);
        executeConstraints();
    }*/
    void createLong(Slot slot1, Slot slot2)
    {
        long_constraint = (ComponentConstraint)componentPositioner.CreateConstraint();
        long_constraint.ConstraintType = NXOpen.Positioning.Constraint.Type.Center22;

        Component component1 = slot1.ParentComponent;
        ConstraintReference constraintReference1 =
            long_constraint.CreateConstraintReference(component1,
                                                      slot1.SideFace1, false, false, false);
        ConstraintReference constraintReference2 =
            long_constraint.CreateConstraintReference(component1,
                                                      slot1.SideFace2, false, false, false);

        Component component2 = slot2.ParentComponent;
        ConstraintReference constraintReference3 =
            long_constraint.CreateConstraintReference(component2,
                                                      slot2.SideFace1, false, false, false);
        ConstraintReference constraintReference4 =
            long_constraint.CreateConstraintReference(component2,
                                                      slot2.SideFace2, false, false, false);

        executeConstraints();
    }
    /*public void createSideTouch(Slot slot1, Slot slot2)
    {
        touch_constraint = (ComponentConstraint)componentPositioner.CreateConstraint();
        touch_constraint.ConstraintAlignment = Constraint.Alignment.ContraAlign;
        touch_constraint.ConstraintType = NXOpen.Positioning.Constraint.Type.Touch;

        Component component1 = slot1.ParentComponent;
        ConstraintReference constraintReference1 =
            touch_constraint.CreateConstraintReference(component1,
                                                       slot1.TouchFace, false, false, false);

        Component component2 = slot2.ParentComponent;
        ConstraintReference constraintReference3 =
            touch_constraint.CreateConstraintReference(component2,
                                                       slot2.TouchFace, false, false, false);
        executeConstraints();

        escapeOverConstrained(touch_constraint);
    }*/
    void createTopTouch(Slot slot1, Slot slot2)
    {
        touch_constraint = (ComponentConstraint)componentPositioner.CreateConstraint();
        touch_constraint.ConstraintAlignment = Constraint.Alignment.ContraAlign;
        touch_constraint.ConstraintType = NXOpen.Positioning.Constraint.Type.Touch;

        Component component1 = slot1.ParentComponent;
        ConstraintReference constraintReference1 =
            touch_constraint.CreateConstraintReference(component1,
                                                       slot1.TopFace, false, false, false);

        Component component2 = slot2.ParentComponent;
        ConstraintReference constraintReference3 =
            touch_constraint.CreateConstraintReference(component2,
                                                       slot2.TopFace, false, false, false);

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

