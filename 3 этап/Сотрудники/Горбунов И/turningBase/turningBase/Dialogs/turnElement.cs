﻿//==============================================================================
//  WARNING!!  This file is overwritten by the Block UI Styler while generating
//  the automation code. Any modifications to this file will be lost after
//  generating the code again.
//
//       Filename:  C:\ug_customization\application\dialogs\turnElement\turnElement.cs
//
//        This file was generated by the NX Block UI Styler
//        Created by: USP
//              Version: NX 7.5
//              Date: 10-28-2013  (Format: mm-dd-yyyy)
//              Time: 15:20 (Format: hh-mm)
//
//==============================================================================

//==============================================================================
//  Purpose:  This TEMPLATE file contains C# source to guide you in the
//  construction of your Block application dialog. The generation of your
//  dialog file (.dlx extension) is the first step towards dialog construction
//  within NX.  You must now create a NX Open application that
//  utilizes this file (.dlx).
//
//  The information in this file provides you with the following:
//
//  1.  Help on how to load and display your Block UI Styler dialog in NX
//      using APIs provided in NXOpen.BlockStyler namespace
//  2.  The empty callback methods (stubs) associated with your dialog items
//      have also been placed in this file. These empty methods have been
//      created simply to start you along with your coding requirements.
//      The method name, argument list and possible return values have already
//      been provided for you.
//==============================================================================

//------------------------------------------------------------------------------
//These imports are needed for the following template code
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.BlockStyler;
using NXOpen.Positioning;
using NXOpen.UF;

//------------------------------------------------------------------------------
//Represents Block Styler application class
//------------------------------------------------------------------------------
public class TurnElement : DialogProgpam
{
    //class members
    private readonly string _theDialogName;

    private UIBlock _group0;// Block type: Group
    private UIBlock _selection0;// Block type: Selection
    private UIBlock _button0;// Block type: Button
    private UIBlock _button01;// Block type: Button

    //------------------------------------------------------------------------------

    private HeightElement _element;
    private bool _elementSelected;

    private List<NXObject> _otherElementObjects; 

    private Slot _checkerSlot;
    private List<Slot> _bottomSlots;

    private const int _TURNS = 4;
    private const int _TURN_ANGLE = 90;


    //------------------------------------------------------------------------------
    //Constructor for NX Styler class
    //------------------------------------------------------------------------------
    public TurnElement()
    {
        try
        {
            Init();
            _theDialogName = Path.Combine(ConfigDlx.FullDlxFolder, ConfigDlx.DlxTurn);

            TheDialog = Config.TheUi.CreateDialog(_theDialogName);
            TheDialog.AddApplyHandler(apply_cb);
            TheDialog.AddOkHandler(ok_cb);
            TheDialog.AddUpdateHandler(update_cb);
            TheDialog.AddInitializeHandler(initialize_cb);
            TheDialog.AddFocusNotifyHandler(focusNotify_cb);
            TheDialog.AddKeyboardFocusNotifyHandler(keyboardFocusNotify_cb);
            TheDialog.AddDialogShownHandler(dialogShown_cb);
        }
        catch (TimeoutException)
        {
            //---- Enter your exception handling code here -----
            //const string mess = "Нет соединения с БД!";
            //Logger.WriteError(mess, ex);
            //Message.Show(mess);
            throw;
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            string mess = "Ошибка в конструкторе " + GetType().Name;
            Logger.WriteError(mess, ex);
            Message.Show(mess);
            throw;
        }
    }
    
    //------------------------------------------------------------------------------
    // Following method cleanup any housekeeping chores that may be needed.
    // This method is automatically called by NX.
    //------------------------------------------------------------------------------
    public static int UnloadLibrary(string arg)
    {
        try
        {
            //---- Enter your code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Logger.WriteError(ex.ToString());
            Message.Show("Block Styler", Message.MessageType.Error, ex);
        }
        return 0;
    }
    
    //------------------------------------------------------------------------------
    //---------------------Block UI Styler Callback Functions--------------------------
    //------------------------------------------------------------------------------
    
    //------------------------------------------------------------------------------
    //Callback Name: initialize_cb
    //------------------------------------------------------------------------------
    private void initialize_cb()
    {
        try
        {
            _group0 = TheDialog.TopBlock.FindBlock("group0");
            _selection0 = TheDialog.TopBlock.FindBlock("selection0");
            _button0 = TheDialog.TopBlock.FindBlock("button0");
            _button01 = TheDialog.TopBlock.FindBlock("button01");
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Logger.WriteError(ex.ToString());
            Message.Show("Block Styler", Message.MessageType.Error, ex);
        }
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: dialogShown_cb
    //This callback is executed just before the dialog launch. Thus any value set 
    //here will take precedence and dialog will be launched showing that value. 
    //------------------------------------------------------------------------------
    private void dialogShown_cb()
    {
        try
        {
            //---- Enter your callback code here -----
            Selection.MaskTriple[] mask = new Selection.MaskTriple[1];
            mask[0].Type = UFConstants.UF_component_type;
            _selection0.GetProperties().SetSelectionFilter("SelectionFilter", Selection.SelectionAction.ClearAndEnableSpecific, mask);
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Logger.WriteError(ex.ToString());
            Message.Show("Block Styler", Message.MessageType.Error, ex);
        }
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: apply_cb
    //------------------------------------------------------------------------------
    private int apply_cb()
    {
        int errorCode = 0;
        try
        {
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            errorCode = 1;
            Logger.WriteError(ex.ToString());
            Message.Show("Block Styler", Message.MessageType.Error, ex);
        }
        return errorCode;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: update_cb
    //------------------------------------------------------------------------------
    private int update_cb( UIBlock block)
    {
        try
        {
            if(block == _selection0)
            {
            //---------Enter your code here-----------
                Logger.WriteLine("Нажата кнопка выбора компонента!");
                SetComponent(block);
                
            }
            else if(block == _button0)
            {
            //---------Enter your code here-----------
                if (_elementSelected)
                {
                    List<Point3d> initialPoints;
                    DeleteSlotConstraints(out initialPoints);
                    SetBottomTurn(initialPoints[0], 90);
                }
            }
            else if(block == _button01)
            {
            //---------Enter your code here-----------
                if (_elementSelected)
                {
                    List<Point3d> initialPoints;
                    DeleteSlotConstraints(out initialPoints);
                    SetBottomTurn(initialPoints[0], -90);
                }
            }
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Logger.WriteError(ex.ToString());
            Message.Show("Block Styler", Message.MessageType.Error, ex);
        }
        return 0;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: ok_cb
    //------------------------------------------------------------------------------
    private int ok_cb()
    {
        int errorCode;
        try
        {
            errorCode = apply_cb();
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            errorCode = 1;
            Logger.WriteError(ex.ToString());
            Message.Show("Block Styler", Message.MessageType.Error, ex);
        }
        return errorCode;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: focusNotify_cb
    //This callback is executed when any block (except the ones which receive keyboard entry such as Integer block) receives focus.
    //------------------------------------------------------------------------------
    private void focusNotify_cb(UIBlock block, bool focus)
    {
        try
        {
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Logger.WriteError(ex.ToString());
            Message.Show("Block Styler", Message.MessageType.Error, ex);
        }
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: keyboardFocusNotify_cb
    //This callback is executed when block which can receive keyboard entry, receives the focus.
    //------------------------------------------------------------------------------
    private void keyboardFocusNotify_cb(UIBlock block, bool focus)
    {
        try
        {
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Logger.WriteError(ex.ToString());
            Message.Show("Block Styler", Message.MessageType.Error, ex);
        }
    }

    //----------------------------------------------------------------------------------

    private void SetComponent(UIBlock block)
    {
        TaggedObject[] taggedObjects = block.GetProperties()
                                            .GetTaggedObjectVector("SelectedObjects");
        if (taggedObjects.Length == 1)
        {
            _element = new HeightElement((Component)taggedObjects[0]);
            Logger.WriteLine("Выбран " + _element.ElementComponent + " - " + _element.ElementComponent.Name);
            _elementSelected = true;
        }
        else
        {
            _elementSelected = false;
            string mess = "";
            if (taggedObjects.Length > 1)
            {
                mess = "Выбрано больше двух элементов!";
            }
            if (taggedObjects.Length < 1)
            {
                mess = "Элемент не выбран!";
            }
            Logger.WriteError(mess);
            Message.ShowError(mess);
        }
    }

    private void SetTurns()
    {

    }

    private void DeleteSlotConstraints(out List<Point3d> centerPoints)
    {
        centerPoints = new List<Point3d>();
        ComponentConstraint[] componentConstraints = _element.ElementComponent.GetConstraints();
        foreach (ComponentConstraint componentConstraint in componentConstraints)
        {
            if (componentConstraint.ConstraintType == Constraint.Type.Center22)
            {
                List<NXObject> faces = _element.GetConstraintObjects(componentConstraint, out _otherElementObjects);

                Surface surface1 = new Surface((Face) faces[0]);
                Surface surface2 = new Surface((Face) faces[1]);
                centerPoints.Add(surface1.GetCenterPoint(surface2));

                NxFunctions.Delete(componentConstraint);
            }
        }
        NxFunctions.Update();
    }

    private void SetTopTurn()
    {
        
    }

    private void SetBottomTurn(Point3d point, double angle)
    {
        Slot oldSlot = _element.GetNearestSlot(point);
        if (_element.IsHorizontSlot(oldSlot))
        {
            Point3d newPoint = GetTurn(point, angle);
            Slot newSlot = _element.GetNearestSlot(newPoint);
            Center center = new Center();
            center.Create22(newSlot, (Face)_otherElementObjects[0], (Face)_otherElementObjects[1]);

            Vector rightSlotDirection = GetRightSlotDirection((Face) _otherElementObjects[0],
                                                              (Face) _otherElementObjects[1]);
            Vector slotDirection = _element.GetHorizontSlotDirection(newSlot);
            Message.Tst(rightSlotDirection, slotDirection);
            Message.Tst(rightSlotDirection.GetAngle(slotDirection));

            if (!rightSlotDirection.IsCoDirectionalInProject(slotDirection))
            {
                Message.Tst("Пазы не сонаправлены!");
                center.Reverse();
            }
            
        }
        //if (_element.HasOutBottomSlotSet)
        //{
        //    Slot firstSlot = _element.BottomSlot;
        //    Point3d[] centerPoints = GetTurn(firstSlot);
        //    _bottomSlots = new List<Slot>();
        //    for (int i = 0; i < centerPoints.Length; i++)
        //    {
        //        _checkerSlot = _element.GetNearestSlot(centerPoints[i]);
        //        if (!_bottomSlots.Exists(ExistSlot))
        //        {
        //            _bottomSlots.Add(_checkerSlot);
        //        }
        //    }
        //}
        NxFunctions.Update();
    }

    private Vector GetRightSlotDirection(Face face1, Face face2)
    {
        Surface surface1 = new Surface(face1);
        Surface surface2 = new Surface(face2);
        Point3d center = surface1.GetCenterPoint(surface2);
        return _element.GetOrtHoleDirection(center);
    }

    private bool ExistSlot(Slot slot)
    {
        return slot == _checkerSlot;
    }


    private Point3d GetTurn(Point3d oldPoint, double angle)
    {
        //Point3d[] centerPoints = new Point3d[_TURNS];
        //centerPoints[0] = slot.CenterPoint;

        Surface surface = new Surface(_element.HoleFace);
        Vector vector = surface.VectorDirection2;

        //for (int i = 1; i < _TURNS; i++)
        //{
        //    centerPoints[i] = vector.GetRotatePoint(centerPoints[0], _TURN_ANGLE * i);
        //}
        return vector.GetRotatePoint(oldPoint, angle);
    }

    private void Init()
    {
        Check();
    }

    private void Check()
    {
        ConfigDlx.UnloadDialog(ConfigDlx.DlxTurn);
    }

    
}
