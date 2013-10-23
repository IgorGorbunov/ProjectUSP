/// <summary>
/// Класс для наложения связей пазирования.
/// </summary>
class SlotConstraint
{
    readonly Center _centerConstr;

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

        _centerConstr = new Center();
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
    /// <summary>
    /// Производит соединение вдоль паза по рёбрам первого паза.
    /// </summary>
    public void SetCenterEdgeConstraint()
    {
        _centerConstr.Create(_firstSlot.ParentComponent,
                            _firstSlot.EdgeLong1, _firstSlot.EdgeLong2,
                            _secondSlot.ParentComponent,
                            _secondSlot.SideFace1, _secondSlot.SideFace2);

        Config.TheUfSession.Modl.Update();
    }
    /// <summary>
    /// Реверс детали УСП вдоль паза.
    /// </summary>
    public void Reverse()
    {
        _centerConstr.Reverse();
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

