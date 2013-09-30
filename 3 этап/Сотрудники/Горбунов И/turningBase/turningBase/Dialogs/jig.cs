﻿//==============================================================================
//  WARNING!!  This file is overwritten by the Block UI Styler while generating
//  the automation code. Any modifications to this file will be lost after
//  generating the code again.
//
//       Filename:  C:\ug_customization\application\dialogs\jig\jig.cs
//
//        This file was generated by the NX Block UI Styler
//        Created by: USP
//              Version: NX 7.5
//              Date: 09-13-2013  (Format: mm-dd-yyyy)
//              Time: 17:58 (Format: hh-mm)
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
using System.Data;
using System.IO;
using NXOpen;
using NXOpen.BlockStyler;
using NXOpen.UF;

//------------------------------------------------------------------------------
//Represents Block Styler application class
//------------------------------------------------------------------------------
public sealed class Jig : DialogProgpam
{
    //class members
    private readonly string _theDialogName;

    private UIBlock group0;// Block type: Group
    private UIBlock _selection0;// Block type: Selection
    private UIBlock _instrDiametrBlock;// Block type: Double
    private UIBlock _sleeveTypeBlock;// Block type: Enumeration
    private UIBlock _importPlankButton;// Block type: Button
    private UIBlock group01;// Block type: Group
    private UIBlock _label0;// Block type: Label
    private UIBlock _label02;// Block type: Label
    private UIBlock _double01;// Block type: Double
    private UIBlock _double02;// Block type: Double
    private UIBlock _toggle0;// Block type: Toggle
    private UIBlock _button01;// Block type: Button
    private UIBlock _toggle01;// Block type: Toggle
    private UIBlock _integer0;// Block type: Integer
    private UIBlock _direction0;

    private Surface _selectedFace;
    private UspElement _workpiece;

    private string _gost;

    private Catalog _catalog;

    private readonly List<string[]> _goodSleeves = new List<string[]>();
    private int _nSleeveColumns;

    private JigPlank _jigPlank;
    private QuickJigSleeve _quickJigSleeve;
    private Edge _thisEdge, _otherEdge;
    private bool _oneEdge;

    private TouchAxe _touchAxeJigElement, _touchAxeSleeveJig;
    private Touch _sleeveJigTouch;
    private Distance _distanceConstr = new Distance();
    private double _recommendDistance, _realDistance;
    private bool _distanceNegative;
    private bool _distanceReverse;

    private const double _DISTANCE_COEF = 0.4;

    private double _startAngle;

    //private 
    
    //------------------------------------------------------------------------------
    //Constructor for NX Styler class
    //------------------------------------------------------------------------------
    public Jig()
    {
        try
        {
            _theDialogName = AppDomain.CurrentDomain.BaseDirectory +
                             Config.DlxFolder + Path.DirectorySeparatorChar + Config.DlxJig;

            TheDialog = Config.TheUi.CreateDialog(_theDialogName);
            TheDialog.AddApplyHandler(apply_cb);
            TheDialog.AddOkHandler(ok_cb);
            TheDialog.AddUpdateHandler(update_cb);
            TheDialog.AddInitializeHandler(initialize_cb);
            TheDialog.AddFocusNotifyHandler(focusNotify_cb);
            TheDialog.AddKeyboardFocusNotifyHandler(keyboardFocusNotify_cb);
            TheDialog.AddDialogShownHandler(dialogShown_cb);
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Logger.WriteError(ex.ToString());
            Message.Show("Ошибка!", Message.MessageIcon.Error, ex.ToString());
            throw;
        }
    }
    //------------------------------- DIALOG LAUNCHING ---------------------------------
    //
    //    Before invoking this application one needs to open any part/empty part in NX
    //    because of the behavior of the blocks.
    //
    //    Make sure the dlx file is in one of the following locations:
    //        1.) From where NX session is launched
    //        2.) $UGII_USER_DIR/application
    //        3.) For released applications, using UGII_CUSTOM_DIRECTORY_FILE is highly
    //            recommended. This variable is set to a full directory path to a file 
    //            containing a list of root directories for all custom applications.
    //            e.g., UGII_CUSTOM_DIRECTORY_FILE=$UGII_ROOT_DIR\menus\custom_dirs.dat
    //
    //    You can create the dialog using one of the following way:
    //
    //    1. Journal Replay
    //
    //        1) Replay this file through Tool->Journal->Play Menu.
    //
    //    2. USER EXIT
    //
    //        1) Create the Shared Library -- Refer "Block UI Styler programmer's guide"
    //        2) Invoke the Shared Library through File->Execute->NX Open menu.
    //
    //------------------------------------------------------------------------------
    
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
            group0 = TheDialog.TopBlock.FindBlock("group0");
            _selection0 = TheDialog.TopBlock.FindBlock("selection0");
            _instrDiametrBlock = TheDialog.TopBlock.FindBlock("double0");
            _sleeveTypeBlock = TheDialog.TopBlock.FindBlock("enum0");
            _importPlankButton = TheDialog.TopBlock.FindBlock("button0");
            group01 = TheDialog.TopBlock.FindBlock("group01");
            _label0 = TheDialog.TopBlock.FindBlock("label0");
            _label02 = TheDialog.TopBlock.FindBlock("label02");
            _double01 = TheDialog.TopBlock.FindBlock("double01");
            _double02 = TheDialog.TopBlock.FindBlock("double02");
            _toggle0 = TheDialog.TopBlock.FindBlock("toggle0");
            _button01 = TheDialog.TopBlock.FindBlock("button01");
            _toggle01 = TheDialog.TopBlock.FindBlock("toggle01");
            _integer0 = TheDialog.TopBlock.FindBlock("integer0");
            _direction0 = TheDialog.TopBlock.FindBlock("direction0");

            _catalog = new Catalog12();
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Message.Show("Block Styler", Message.MessageIcon.Error,
                                           ex.ToString());
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

            _double01.GetProperties().SetDouble("Value", 0.0);
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Config.TheUi.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error,
                                           ex.ToString());
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
            Config.TheUi.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error,
                                           ex.ToString());
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
                Logger.WriteLine("Нажата кнопка 'Выбора объекта'.");
                SetFace(block);
            }
            else if(block == _instrDiametrBlock)
            {
            //---------Enter your code here-----------
            }
            else if(block == _sleeveTypeBlock)
            {
            //---------Enter your code here-----------
            }
            else if(block == _importPlankButton)
            {
            //---------Enter your code here-----------
                //запуск галереи
                _gost = "15321-70";
                ImportJig();
            }
            else if(block == _label0)
            {
            //---------Enter your code here-----------
            }
            else if (block == _double02)
            {
                SetUserDistance(block);
            }
            else if (block == _direction0)
            {
                SelectOtherDistanceConstraint();
            }
            else if (block == _double01)
            {
                //---------Enter your code here-----------
                bool isFixed = _workpiece.ElementComponent.IsFixed;
                if (!isFixed)
                {
                    _workpiece.Fix();
                }
                double angle = _double01.GetProperties().GetDouble("Value");
                Vector jigVector = new Vector(_jigPlank.SleeveFace);
                Movement.MoveByRotation(_jigPlank.ElementComponent, jigVector, angle - _startAngle);
                _startAngle = angle;
                NxFunctions.Update();
                if (!isFixed)
                {
                    _workpiece.Unfix();
                }
            }
            else if (block == _toggle0)
            {
                //---------Enter your code here-----------
            }
            else if (block == _button01)
            {
                //---------Enter your code here-----------
            }
            else if (block == _toggle01)
            {
                //---------Enter your code here-----------
            }
            else if (block == _integer0)
            {
                //---------Enter your code here-----------
            }
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Config.TheUi.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error,
                                           ex.ToString());
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
            Config.TheUi.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error,
                                           ex.ToString());
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
            Config.TheUi.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error,
                                           ex.ToString());
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
            Config.TheUi.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error,
                                           ex.ToString());
        }
    }
    
    //---------------------------------------------------------------------------------

    private void SetFace(UIBlock block)
    {
        TaggedObject[] taggedObjects = block.GetProperties().GetTaggedObjectVector("SelectedObjects");
        _selectedFace = new Surface((Face) taggedObjects[0]);
        _workpiece = new UspElement(_selectedFace.Face.OwningComponent);

        Logger.WriteLine("Выбран объект " + _selectedFace.Face);

        SetEnable(_instrDiametrBlock, true);
        SetEnable(_sleeveTypeBlock, true);
        SetEnable(_importPlankButton, true);

        _instrDiametrBlock.GetProperties().SetDouble("MaximumValue", _selectedFace.Radius * 2);
        _instrDiametrBlock.GetProperties().SetDouble("Value", _selectedFace.Radius * 2);
    }

    private void ImportJig()
    {
        DataTable sleeves = SqlUspElement.GetSleeves(_catalog, GetSleeveTypeConditions(), _gost);
        KeyValuePair<string, double> bestSleeve = FilterSleeves(sleeves);
        if (_goodSleeves.Count > 0)
        {
            double outDiametr = SqlUspElement.GetDiametr(_catalog, bestSleeve.Key);
            string jigTitle = SqlUspJigs.GetByDiametr(_catalog, outDiametr);
            Katalog2005.Algorithm.SpecialFunctions.LoadPart(jigTitle, false);
            _jigPlank = new JigPlank(Katalog2005.Algorithm.SpecialFunctions.LoadedPart);
            Katalog2005.Algorithm.SpecialFunctions.LoadPart(bestSleeve.Key, false);
            _quickJigSleeve = new QuickJigSleeve(Katalog2005.Algorithm.SpecialFunctions.LoadedPart);
            SetConstraints();
            SetRecommendDistance();
            SetEnable(group01, true);
            SetEnable(_direction0, !_oneEdge);
        }
        else
        {
            string mess = "Подходящих втулок не было найдено!" + Environment.NewLine +
                          "Измените параметры.";
            Logger.WriteWarning(mess);
            Message.ShowError(mess);
            SetEnable(group01, false);
        }
    }

    private void SetConstraints()
    {
        bool isFixed = _workpiece.ElementComponent.IsFixed;
        if (!isFixed)
        {
            _workpiece.Fix();
        }
        NxFunctions.Update();
#if (!DEBUG)
        NxFunctions.FreezeDisplay();
#endif
        _touchAxeJigElement = _jigPlank.SetOn(_workpiece.ElementComponent, _selectedFace.Face);
        _sleeveJigTouch = _quickJigSleeve.SetOnJig(_jigPlank);
        _touchAxeSleeveJig = _quickJigSleeve.SetToJig(_jigPlank);
        NxFunctions.Update();
        SelectDistanceConstraint();
        NxFunctions.Update();
#if (!DEBUG)
        NxFunctions.UnFreezeDisplay();
#endif

        if (!isFixed)
        {
            _workpiece.Unfix();
        }
        NxFunctions.Update();
    }

    private Edge[] GetEgdes()
    {
        Edge[] edges = _selectedFace.Face.GetEdges();
        Edge[] twoEgdes = new Edge[2];
        int i = 0;
        foreach (Edge edge in edges)
        {
            if (i == 2)
                break;

            if (edge.SolidEdgeType != Edge.EdgeType.Circular ||
                Config.Round(_selectedFace.Radius) != Config.Round(Geom.GetRadius(edge))) 
                continue;
           
            twoEgdes[i] = edge;
            i++;
        }
        return twoEgdes;
    }

    private void SelectDistanceConstraint()
    {
        Edge[] edges = GetEgdes();
        //0.1 - чтобы легче определить пересечение кондуктора с заготовкой
        _realDistance = _jigPlank.UspCatalog.PSlotHeight + 0.1;
        if (edges[1] == null)
        {
            SetDistanceConstraint(edges[0]);
            _oneEdge = true;
        }
        else
        {
            _oneEdge = false;
            SetDistanceConstraint(edges[0]);
            _thisEdge = edges[0];
            _otherEdge = edges[1];
            ElementIntersection intersection = new ElementIntersection(_workpiece.Body,
                                                                       _jigPlank.Body);
            if (intersection.AnyIntersectionExists || _distanceConstr.IsOverConstrained())
            {
                SetDistanceConstraint(edges[1]);
                _thisEdge = edges[1];
                _otherEdge = edges[0];
            }
        }
    }

    private void SelectOtherDistanceConstraint()
    {
        SetDistanceConstraint(_otherEdge);
        Edge tmpEdge = _thisEdge;
        _thisEdge = _otherEdge;
        _otherEdge = tmpEdge;
    }

    private void SetDistanceConstraint(Edge edge)
    {
        bool intersecst = SetDistance(edge);
        if (!intersecst) 
            return;
        _touchAxeJigElement.Reverse();
        SetDistance(edge);
    }

    private bool SetDistance(Edge edge)
    {

        _distanceConstr.Delete();
        _distanceConstr = new Distance();
        _distanceConstr.Create(_workpiece.ElementComponent, edge, _jigPlank.ElementComponent, _jigPlank.SlotFace, _realDistance);
        _distanceReverse = false;
        NxFunctions.Update();

        ElementIntersection intersection = new ElementIntersection(_workpiece.Body,
                                                                   _jigPlank.Body);

        if (!intersection.AnyIntersectionExists && !_distanceConstr.IsOverConstrained())
            return false;

        _distanceConstr.Reverse();
        _distanceReverse = true;
        NxFunctions.Update();

        if (!intersection.AnyIntersectionExists && !_distanceConstr.IsOverConstrained())
            return false;

        _distanceConstr.Delete();
        _distanceConstr = new Distance();
        _realDistance = -_realDistance;
        _distanceConstr.Create(_workpiece.ElementComponent, edge, _jigPlank.ElementComponent, _jigPlank.SlotFace, _realDistance);
        _distanceReverse = false;
        NxFunctions.Update();

        if (!intersection.AnyIntersectionExists && !_distanceConstr.IsOverConstrained())
            return false;

        _distanceConstr.Reverse();
        _distanceReverse = true;
        NxFunctions.Update();

        if (!intersection.AnyIntersectionExists && !_distanceConstr.IsOverConstrained())
            return false;

        return true;
    }

    private void SetRecommendDistance()
    {
        double sleeveWorkpieceLength = _DISTANCE_COEF * _quickJigSleeve.InnerDiametr;
        Surface surface1 = new Surface(_jigPlank.SlotFace);
        Surface surface2 = new Surface(_quickJigSleeve.BottomFace);
        _recommendDistance = sleeveWorkpieceLength + Math.Abs(surface1.GetDistance(surface2));

        _label02.GetProperties().SetString("Label", "Рекомендуемое расстояние - " + Config.Round(_recommendDistance));
    }

    private void SetUserDistance(UIBlock block)
    {
        double distance = block.GetProperties().GetDouble("Value");
        bool negative = _realDistance < 0;
        _distanceConstr.EditValue(distance);
    }

    private string GetSleeveTypeConditions()
    {
        string gost;
        switch (_sleeveTypeBlock.GetProperties().GetEnumAsString("Value"))
        {
            case "Быстросменные":
                gost = SqlTabUspData.GetGost(SqlTabUspData.GostUsp.QuickSleeves, _catalog);
                return " and " + SqlTabUspData.CGost + " = '" + gost + "'";
            case "Обычные":
                gost = SqlTabUspData.GetGost(SqlTabUspData.GostUsp.Sleeves, _catalog);
                return " and " + SqlTabUspData.CGost + " = " + gost + "'";
        }
        return "";
    }

    private double GetDiametr()
    {
        return _instrDiametrBlock.GetProperties().GetDouble("Value");
    }

    private KeyValuePair<string, double> FilterSleeves(DataTable sleeves)
    {
        double diametr = GetDiametr();
        _goodSleeves.Clear();
        _nSleeveColumns = sleeves.Columns.Count;

        foreach (DataRow row in sleeves.Rows)
        {
            string[] split = row[1].ToString().Split(' ');
            double minD = Double.Parse(split[1]);
            double maxD = Double.Parse(split[3]);
            if (minD >= diametr || maxD < diametr) 
                continue;

            string[] newRow = new string[_nSleeveColumns];
            for (int i = 0; i < sleeves.Columns.Count; i++)
            {
                newRow[i] = row[i].ToString();
            }
            _goodSleeves.Add(newRow);
        }

        KeyValuePair<string, double> bestSleeve = SortSleeves();
        return bestSleeve;
    }

    private KeyValuePair<string, double> SortSleeves()
    {
        KeyValuePair<string, double>[] sleeveDict = new KeyValuePair<string, double>[_goodSleeves.Count];
        int i = 0;
        foreach (string[] goodSleeve in _goodSleeves)
        {
            sleeveDict[i] = new KeyValuePair<string, double>(goodSleeve[0], Double.Parse(goodSleeve[3]));
            i++;
        }
        if (_goodSleeves.Count > 1)
        {
            Instr.QSortPairs(sleeveDict, 0, _goodSleeves.Count);
        }
        if (_goodSleeves.Count > 0)
        {
            return sleeveDict[sleeveDict.Length - 1];
        }
        return new KeyValuePair<string, double>();
    }
}
