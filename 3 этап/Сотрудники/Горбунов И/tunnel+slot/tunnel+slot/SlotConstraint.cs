/// <summary>
/// Класс для наложения связей пазирования.
/// </summary>
class SlotConstraint
{
    readonly CenterConstraint _centerConstr;

    readonly Slot _firstSlot;
    readonly Slot _secondSlot;

    /// <summary>
    /// Инициализирует новый экземпляр класса связей для пазирования.
    /// </summary>
    /// <param name="firstSlot">Первый паз.</param>
    /// <param name="secondSlot">Второй паз.</param>
    public SlotConstraint(Slot firstSlot, Slot secondSlot)
    {
        _firstSlot = firstSlot;
        _secondSlot = secondSlot;

        _centerConstr = new CenterConstraint();
    }

    /// <summary>
    /// Производит соединение вдоль паза.
    /// </summary>
    public void SetCenterConstraint()
    {
        _centerConstr.Create(_firstSlot.ParentComponent,
                            _firstSlot.SideFace1, _firstSlot.SideFace2,
                            _secondSlot.ParentComponent,
                            _secondSlot.SideFace1, _secondSlot.SideFace2);

        Config.TheUfSession.Modl.Update();
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
    public void SetEachOtherConstraint(Slot slot1, Slot slot2)
    {
        //this.createLong(slot1, slot2);

        slot1.FindTopFace();
        slot2.FindTopFace();

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

