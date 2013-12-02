using System.Globalization;
using NXOpen;
using NXOpen.Positioning;

/// <summary>
/// Класс для создания различный связей и ограничений.
/// </summary>
public class Constrainter
{
    protected ComponentConstraint Constr;
    protected ComponentPositioner CompPositioner;

    ComponentNetwork _componentNetwork;
    private bool clear;

    protected Constrainter()
    {
        InitConstraints();
    }

    /// <summary>
    /// Производит реверс констрэйнта.
    /// </summary>
    public void Reverse()
    {
        Constr.FlipAlignment();
    }

    /// <summary>
    /// Удаляет соединение.
    /// </summary>
    public void Delete()
    {
        if (Constr == null)
            return;
        NXObject objectToDelete = Constr;
        Config.TheSession.UpdateManager.AddToDeleteList(objectToDelete);
        Constr = null;
    }
    /// <summary>
    /// Возвращает true, если сопряжение переограничено.
    /// </summary>
    /// <returns></returns>
    public bool IsOverConstrained()
    {
        if (Constr.GetConstraintStatus() == Constraint.SolverStatus.OverConstrained ||
            Constr.GetConstraintStatus() == Constraint.SolverStatus.NotConsistentUnknown)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// Устанавливает необходимое значение в contsraint.
    /// </summary>
    /// <param name="value">Значение.</param>
    public void EditValue(double value)
    {
        InitConstraints();
        Session.UndoMarkId markId2 = Config.TheSession.SetUndoMark(Session.MarkVisibility.Invisible, "Assembly Constraints Update");

        NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
        numberFormatInfo.NumberGroupSeparator = ".";

        Constr.Expression.RightHandSide = value.ToString(numberFormatInfo);
        ExecuteConstraints();

        //важно!
        Config.TheSession.UpdateManager.DoUpdate(markId2);
    }

    protected void ExecuteConstraints()
    {
        _componentNetwork.Solve();

        //для того чтобы нормально работали фиксы
        //показывает обновления при изменения constraint
        CompPositioner.ClearNetwork();
    }

    private void InitConstraints()
    {
        CompPositioner = Config.WorkPart.ComponentAssembly.Positioner;
        
        _componentNetwork = (ComponentNetwork)CompPositioner.EstablishNetwork();
        _componentNetwork.MoveObjectsState = true;
        
    }
    



}

