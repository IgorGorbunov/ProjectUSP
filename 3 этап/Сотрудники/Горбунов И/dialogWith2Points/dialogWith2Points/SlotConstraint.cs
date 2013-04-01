using System;
using System.Collections.Generic;
using System.Text;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

class SlotConstraint
{
    public ComponentConstraint bottom_constraint, long_constraint, touch_constraint;

    ComponentPositioner componentPositioner;
    ComponentNetwork componentNetwork;

    public SlotConstraint(ComponentConstraint bottom,
                          ComponentConstraint long_slot, ComponentConstraint touch)
    {
        this.bottom_constraint = bottom;
        this.long_constraint = long_slot;
        this.touch_constraint = touch;

        this.initConstraints();
    }

    public SlotConstraint()
        : this(null, null, null)
    {

    }

    public void removeTouch()
    {
        NXObject object_to_delete = touch_constraint;
        Config.theSession.UpdateManager.AddToDeleteList(object_to_delete);
    }

    public void reverse()
    {
        long_constraint.FlipAlignment();
    }


    public void setLongConstraint(Slot slot1, Slot slot2)
    {
        this.createLong(slot1, slot2);
        this.createBottom(slot1, slot2);

        slot1.setTouchFace();
        slot2.setTouchFace();

        this.createSideTouch(slot1, slot2);
    }
    public void setEachOtherConstraint(Slot slot1, Slot slot2)
    {
        this.createLong(slot1, slot2);

        slot1.findTopFace();
        slot2.findTopFace();

        createTopTouch(slot1, slot2);
    }

    public void createBottom(Slot slot1, Slot slot2)
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
    }
    public void createLong(Slot slot1, Slot slot2)
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
    public void createSideTouch(Slot slot1, Slot slot2)
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
    }
    public void createTopTouch(Slot slot1, Slot slot2)
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

    public void executeConstraints()
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

    void escapeOverConstrained(ComponentConstraint constrain)
    {
        Constraint.SolverStatus status = constrain.GetConstraintStatus();
        if (status == Constraint.SolverStatus.OverConstrained)
        {
            this.reverse();

            executeConstraints();
        }
    }
}

