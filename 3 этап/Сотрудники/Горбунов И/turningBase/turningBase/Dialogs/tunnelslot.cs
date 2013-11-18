﻿//==============================================================================
//  WARNING!!  This file is overwritten by the Block UI Styler while generating
//  the automation code. Any modifications to this file will be lost after
//  generating the code again.
//
//       Filename:  C:\ug_customization\application\dialogs\tunnel+slot\tunnelslot.cs
//
//        This file was generated by the NX Block UI Styler
//        Created by: USP
//              Version: NX 7.5
//              Date: 06-03-2013  (Format: mm-dd-yyyy)
//              Time: 17:38 (Format: hh-mm)
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
using System.IO;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.BlockStyler;
using NXOpen.UF;

//------------------------------------------------------------------------------
//Represents Block Styler application class
//------------------------------------------------------------------------------
// ReSharper disable UnusedMember.Global
public class Tunnelslot : DialogProgpam
// ReSharper restore UnusedMember.Global
{
    private readonly string _theDialogName;

    private UIBlock _selection0;// Block type: Selection
    private UIBlock _faceSelect0;// Block type: Face Collector
    private UIBlock _direction0;// Block type: Position
    private UIBlock _selection01;// Block type: Selection
    private UIBlock _point0;// Block type: Specify Point
    private UIBlock _slotTunPoint;
    private UIBlock _toggle0;// Block type: Specify Point

    //---------------------------------------------------------------------------------

    private UspElement _element1, _element2;
    private Tunnel _tunnel1;
    private SlotSet _slotSet1, _slotSet2;
    private static Slot _slot1;
    private static Slot _slot2;

    private TunnelSlotConstraint _constraint;

    private Catalog _firstElementCatalog;

    private static bool _secondPointSelected;
    private static bool _firstPointSelected;

    private static bool _hasNearestSlot1;
    private static bool _hasNearestSlot2;

    //------------------------------------------------------------------------------
    //Constructor for NX Styler class
    //------------------------------------------------------------------------------
    public Tunnelslot()
    {
        try
        {
            _theDialogName = AppDomain.CurrentDomain.BaseDirectory +
                ConfigDlx.DlxFolder + Path.DirectorySeparatorChar + ConfigDlx.DlxTunnelSlot;

            TheDialog = Config.TheUi.CreateDialog(_theDialogName);
            TheDialog.AddApplyHandler(apply_cb);
            TheDialog.AddOkHandler(ok_cb);
            TheDialog.AddUpdateHandler(update_cb);
            TheDialog.AddCancelHandler(cancel_cb);
            TheDialog.AddFilterHandler(filter_cb);
            TheDialog.AddInitializeHandler(initialize_cb);
            TheDialog.AddFocusNotifyHandler(focusNotify_cb);
            TheDialog.AddKeyboardFocusNotifyHandler(keyboardFocusNotify_cb);
            TheDialog.AddDialogShownHandler(dialogShown_cb);
        }
        catch (Exception)
        {
            //---- Enter your exception handling code here -----
            Message.Show("Конструктор NX диалога не запустился!");
            throw;
        }
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
            _selection0 = TheDialog.TopBlock.FindBlock("selection0");
            _faceSelect0 = TheDialog.TopBlock.FindBlock("face_select0");
            _slotTunPoint = TheDialog.TopBlock.FindBlock("point01");
            _direction0 = TheDialog.TopBlock.FindBlock("direction0");
            _selection01 = TheDialog.TopBlock.FindBlock("selection01");
            _point0 = TheDialog.TopBlock.FindBlock("point0");
            _toggle0 = TheDialog.TopBlock.FindBlock("toggle0");
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Config.TheUi.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
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
            mask[0].Type = UFConstants.UF_solid_type;
            mask[0].Subtype = UFConstants.UF_all_subtype;
            mask[0].SolidBodySubtype = UFConstants.UF_UI_SEL_FEATURE_CYLINDRICAL_FACE;
            _selection0.GetProperties().SetSelectionFilter("SelectionFilter", Selection.SelectionAction.ClearAndEnableSpecific, mask);

            _toggle0.GetProperties().SetLogical("Value", false);
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Config.TheUi.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: apply_cb
    //------------------------------------------------------------------------------
    private static int apply_cb()
    {
        int errorCode = 0;
        try
        {
            //---- Enter your callback code here -----
            Logger.WriteLine("Нажата кнопка ПРИМЕНИТЬ.");
            CleanUp();
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            errorCode = 1;
            Config.TheUi.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
            Logger.WriteError(ex.ToString());
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
                Logger.WriteLine("Нажат выбор объекта по первому select.");
                //SetFirstComponent(block);
                SetFirstFace(block);
            }
            else if(block == _faceSelect0)
            {
            //---------Enter your code here-----------
                Logger.WriteLine("Нажат выбор первой грани.");
                //SetFirstFace(block);
            }
            else if (block == _slotTunPoint)
            {
                //---------Enter your code here-----------
                Logger.WriteLine("Нажата постановка первой точки.");
                SetFirstPoint(block);
            }
            else if (block == _direction0)
            {
                //---------Enter your code here-----------
                Logger.WriteLine("Нажат реверс.");
                _constraint.Reverse();
            }
            else if(block == _selection01)
            {
            //---------Enter your code here-----------
                Logger.WriteLine("Нажат выбор объекта по второму select.");
                SetSecondComponent(block);
            }
            else if(block == _point0)
            {
            //---------Enter your code here-----------
                Logger.WriteLine("Нажата постановка второй точки.");
                SetSecondPoint(block);
                
            }
            else if (block == _toggle0)
            {
                //---------Enter your code here-----------
                Logger.WriteLine("Нажат переключатель вставки болта.");
                _constraint.InsertTBolt();
            }
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Config.TheUi.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
            Logger.WriteError(ex.ToString());
        }
        return 0;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: ok_cb
    //------------------------------------------------------------------------------
    private static int ok_cb()
    {
// ReSharper disable RedundantAssignment
        int errorCode = 0;
// ReSharper restore RedundantAssignment
        try
        {
            errorCode = apply_cb();
            //---- Enter your callback code here -----
            Logger.WriteLine("Нажата кнопка ОК.");
            CleanUp();
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            errorCode = 1;
            Config.TheUi.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
            Logger.WriteError(ex.ToString());
        }
        return errorCode;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: cancel_cb
    //------------------------------------------------------------------------------
    private static int cancel_cb()
    {
        try
        {
            //---- Enter your callback code here -----
            Logger.WriteLine("Нажата кнопка ОТМЕНА.");
            CleanUp();
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Config.TheUi.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
            Logger.WriteError(ex.ToString());
        }
        return 0;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: filter_cb
    //------------------------------------------------------------------------------
    private static int filter_cb(UIBlock block, TaggedObject selectedObject)
    {
        return(UFConstants.UF_UI_SEL_ACCEPT);
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: focusNotify_cb
    //This callback is executed when any block (except the ones which receive keyboard entry such as Integer block) receives focus.
    //------------------------------------------------------------------------------
    private static void focusNotify_cb(UIBlock block, bool focus)
    {
        try
        {
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Config.TheUi.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: keyboardFocusNotify_cb
    //This callback is executed when block which can receive keyboard entry, receives the focus.
    //------------------------------------------------------------------------------
    private static void keyboardFocusNotify_cb(UIBlock block, bool focus)
    {
        try
        {
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Config.TheUi.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }

    //--------------------------------------------------------------------

    void SetSecondComponent(UIBlock block)
    {
        if (SetComponent(block, ref _element2, _firstElementCatalog))
        {
            SetEnable(_point0, true);

            _slotSet2 = new SlotSet(_element2);
            _element2.SetBottomFaces();
        }
        else
        {
            SetEnable(_point0, false);
        }
        _secondPointSelected = false;
        if (_slot2 != null)
        {
            _slot2.Unhighlight();
        }
        UnSelectObjects(_point0);
        SetEnable(_direction0, false);
    }
    bool SetComponent(UIBlock block, ref UspElement element,
                      Catalog anotherElementCatalog)
    {
        PropertyList propList = block.GetProperties();
        TaggedObject[] tagObs = propList.GetTaggedObjectVector("SelectedObjects");

        //если не деселект
        if (tagObs.Length > 0)
        {
            Component parentComponent = Config.FindCompByBodyTag(tagObs[0].Tag);
            if (Geom.IsComponent(tagObs[0]))
            {
                Logger.WriteLine("Объект - " + tagObs[0] +
                    " - " + parentComponent.Name);

                element = new UspElement(parentComponent);

                if (anotherElementCatalog != null)
                {
                    if (element.UspCatalog != anotherElementCatalog)
                    {
                        string mess = "Детали не из одного каталога!" + Environment.NewLine +
                             "Пожалуйста, перевыберите элемент.";
                        Logger.WriteWarning(mess);
                        Logger.WriteLine("Объект - " + tagObs[0]);
                        UnSelectObjects(block);

                        Message.Show(mess);

                        block.Focus();

                        element = null;
                        return false;
                    }
                }
                return true;
            }
            string message = "Выбрана не деталь УСП!" + Environment.NewLine +
                             "Пожалуйста, перевыберите элемент.";
            Logger.WriteWarning(message);
            Logger.WriteLine("Объект - " + tagObs[0]);
            UnSelectObjects(block);

            Config.TheUi.NXMessageBox.Show("Error!",
                                           NXMessageBox.DialogType.Error,
                                           message);

            block.Focus();
            return false;
        }
        Logger.WriteLine("Деселект объекта.");
        return false;
    }

    void SetFirstFace(UIBlock block)
    {
        if (SetFace(block, ref _tunnel1, ref _element1))
        {
            SetEnable(_slotTunPoint, true);
        }
        else
        {
            SetEnable(_slotTunPoint, false);
        }
        _firstPointSelected = false;
        if (_slot1 != null)
        {
            _slot1.Unhighlight();
        }
        UnSelectObjects(_slotTunPoint);
        SetEnable(_direction0, false);
    }
    bool SetFace(UIBlock block, ref Tunnel tunnel, ref UspElement element)
    {
        PropertyList propertyList = block.GetProperties();
        TaggedObject[] to = propertyList.GetTaggedObjectVector("SelectedObjects");

        //если не деселект
        if (to.Length > 0)
        {
            Face face = (Face) to[0];
            element = new UspElement(face.OwningComponent);
            if (2 * Geom.GetRadius(face) >= element.UspCatalog.Diametr)
            {
                Logger.WriteLine("Грань выбрана - " + face);
                tunnel = new Tunnel(face, element);
                _firstElementCatalog = _element1.UspCatalog;
                _slotSet1 = new SlotSet(_element1);
                _element1.SetBottomFaces();
                return true;
            }
            const string message1 = "Отверстие слишком маленькое! Выберите другую грань!";
            Logger.WriteWarning(message1 + Environment.NewLine + "Выбрана грань - " + face);
            UnSelectObjects(block);
            Config.TheUi.NXMessageBox.Show("Error!",
                                           NXMessageBox.DialogType.Error,
                                           message1);
            block.Focus();
            return false;
        }
        Logger.WriteLine("Деселект грани");
        return false;
    }

    void SetFirstPoint(UIBlock block)
    {
        if (SetPoint(block, ref _slotSet1))
        {
            if (_firstPointSelected)
            {
                _slot1.Unhighlight();
            }
            _firstPointSelected = true;

            _hasNearestSlot1 = _slotSet1.HasNearestSlot(out _slot1);

            if (_hasNearestSlot1 && 
                Geom.PointIsBetweenStraights(_tunnel1.CentralPoint, 
                          new Surface(_slotSet1.BottomFace), 
                          new Straight(_slot1.EdgeLong1), new Straight(_slot1.EdgeLong2)) &&
                Geom.DirectionsAreOnStraight(_tunnel1.Direction, _slot1.BottomDirection))
            {
                _slot1.Highlight();
                SetConstraints();
            }
            else
            {
                string message = "Базовое отверстие не пересекается с пазом!" + Environment.NewLine +
                         "Выберите другое отверстие или паз!";
                Logger.WriteWarning(message);
                Config.TheUi.NXMessageBox.Show("Error!",
                                               NXMessageBox.DialogType.Error,
                                               message);
                _firstPointSelected = false;
                UnSelectObjects(_slotTunPoint);
                _slotTunPoint.Focus();
            }
        }
        else
        {
            _firstPointSelected = false;
            SetEnable(block, false);

            UnSelectObjects(_faceSelect0);
            SetEnable(_faceSelect0, false);

            UnSelectObjects(_selection0);
            _selection0.Focus();
        }
    }
    void SetSecondPoint(UIBlock block)
    {
        if (SetPoint(block, ref _slotSet2))
        {
            if (_secondPointSelected)
            {
                _slot2.Unhighlight();
            }
            _secondPointSelected = true;
            _hasNearestSlot2 = _slotSet2.HasNearestSlot(out _slot2);

            if (_slot2.Type == Config.SlotType.Pslot)
            {
                string message = "Должен быть выбран Т-образный паз!" + Environment.NewLine +
                         "Выберите другой паз!";
                Logger.WriteWarning(message);
                Config.TheUi.NXMessageBox.Show("Error!",
                                               NXMessageBox.DialogType.Error,
                                               message);

                _secondPointSelected = false;
                UnSelectObjects(block);
                return;
            }

            _slot2.Highlight();
            SetConstraints();
        }
        else
        {
            _secondPointSelected = false;
            SetEnable(block, false);

            UnSelectObjects(_selection01);
            _selection01.Focus();
        }
    }

    static bool SetPoint(UIBlock block, ref SlotSet slotSet)
    {
        PropertyList propertyList = block.GetProperties();
        Point3d point = propertyList.GetPoint("Point");

        if (SetPoint(point, ref slotSet))
        {
            return true;
        }
        string message = "Базовые плоскости пазов не найдены!" + Environment.NewLine +
                         "Выберите другой элемент!";
        Logger.WriteWarning(message);
        Config.TheUi.NXMessageBox.Show("Error!",
                                       NXMessageBox.DialogType.Error,
                                       message);
        UnSelectObjects(block);
        return false;
    }
    static bool SetPoint(Point3d point, ref SlotSet slotSet)
    {
        slotSet.SetPoint(point);
        if (slotSet.HaveNearestBottomFace())
        {
            slotSet.SetNearestEdges();

            return true;
        }
        return false;
    }


    void SetConstraints()
    {
        if (!_firstPointSelected || !_secondPointSelected) return;
        Logger.WriteLine("Запущена процедура позиционирования.");

        if (_hasNearestSlot1 && _hasNearestSlot2)
        {
            _tunnel1.SetSlot(_slot1);

            bool withBolt = _toggle0.GetProperties().GetLogical("Value");
            _constraint = new TunnelSlotConstraint(_element1, _tunnel1, _element2, _slot2);
            _constraint.Create(withBolt);

            SetEnable(_direction0, true);
            SetEnable(_toggle0, true);
        }
        else
        {
            string mess = "Ближайший слот для первого элемента найден - " + _hasNearestSlot1 +
                          Environment.NewLine;
            mess += "Ближайший слот для второго элемента найден - " + _hasNearestSlot2;
            Logger.WriteLine(mess);
        }
    }

    static void CleanUp()
    {
        if (_slot1 != null)
        {
            _slot1.Unhighlight();
        }
        if (_slot2 != null)
        {
            _slot2.Unhighlight();
        }
        _secondPointSelected = false;
        _firstPointSelected = false;

        _hasNearestSlot1 = false;
        _hasNearestSlot2 = false;
    }

}