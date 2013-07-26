//==============================================================================
//  WARNING!!  This file is overwritten by the Block UI Styler while generating
//  the automation code. Any modifications to this file will be lost after
//  generating the code again.
//
//       Filename:  C:\ug_customization\application\dialogs\turningBase\turningBase.cs
//
//        This file was generated by the NX Block UI Styler
//        Created by: USP
//              Version: NX 7.5
//              Date: 07-26-2013  (Format: mm-dd-yyyy)
//              Time: 15:48 (Format: hh-mm)
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

//------------------------------------------------------------------------------
//Represents Block Styler application class
//------------------------------------------------------------------------------
public sealed class TurningBase
{
    //class members
    private static TurningBase _theturningBase;
    private readonly string _theDialogName;
    private BlockDialog _theDialog;
    private UIBlock _group0;// Block type: Group
    private UIBlock _faceSelect0;// Block type: Face Collector

    private Point3d _point;
    private double[] _direct;

    private bool _faceSelected;

    
    //------------------------------------------------------------------------------
    //Constructor for NX Styler class
    //------------------------------------------------------------------------------
    public TurningBase()
    {
        try
        {
            _theDialogName = AppDomain.CurrentDomain.BaseDirectory +
                Config.DlxFolder + Path.DirectorySeparatorChar + Config.DlxTurningBase;

            _theDialog = Config.TheUi.CreateDialog(_theDialogName);
            _theDialog.AddApplyHandler(new NXOpen.BlockStyler.BlockDialog.Apply(apply_cb));
            _theDialog.AddOkHandler(new NXOpen.BlockStyler.BlockDialog.Ok(ok_cb));
            _theDialog.AddUpdateHandler(new NXOpen.BlockStyler.BlockDialog.Update(update_cb));
            _theDialog.AddCancelHandler(new NXOpen.BlockStyler.BlockDialog.Cancel(cancel_cb));
            _theDialog.AddInitializeHandler(new NXOpen.BlockStyler.BlockDialog.Initialize(initialize_cb));
            _theDialog.AddFocusNotifyHandler(new NXOpen.BlockStyler.BlockDialog.FocusNotify(focusNotify_cb));
            _theDialog.AddKeyboardFocusNotifyHandler(new NXOpen.BlockStyler.BlockDialog.KeyboardFocusNotify(keyboardFocusNotify_cb));
            _theDialog.AddDialogShownHandler(new NXOpen.BlockStyler.BlockDialog.DialogShown(dialogShown_cb));
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            throw ex;
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
    public static void Main()
    {
        try
        {
            Logger.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++" + " ������ ������ ��������� " + Environment.CommandLine);
            _theturningBase = new TurningBase();
            // The following method shows the dialog immediately
            _theturningBase.Show();
            Logger.WriteLine("--------------------------------------------------" + " ����� ������ ��������� " + Environment.CommandLine);
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Message.Show(ex);
        }
        finally
        {
            _theturningBase.Dispose();
        }
    }
    //------------------------------------------------------------------------------
    // This method specifies how a shared image is unloaded from memory
    // within NX. This method gives you the capability to unload an
    // internal NX Open application or user  exit from NX. Specify any
    // one of the three constants as a return value to determine the type
    // of unload to perform:
    //
    //
    //    Immediately : unload the library as soon as the automation program has completed
    //    Explicitly  : unload the library from the "Unload Shared Image" dialog
    //    AtTermination : unload the library when the NX session terminates
    //
    //
    // NOTE:  A program which associates NX Open applications with the menubar
    // MUST NOT use this option since it will UNLOAD your NX Open application image
    // from the menubar.
    //------------------------------------------------------------------------------
     public static int GetUnloadOption(string arg)
    {
        //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);
         return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);
        // return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
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
            Message.Show(ex);
        }
        return 0;
    }
    
    //------------------------------------------------------------------------------
    //This method shows the dialog on the screen
    //------------------------------------------------------------------------------
    private NXOpen.UIStyler.DialogResponse Show()
    {
        try
        {
            _theDialog.Show();
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Message.Show(ex);
        }
        return 0;
    }
    
    //------------------------------------------------------------------------------
    //Method Name: Dispose
    //------------------------------------------------------------------------------
    private void Dispose()
    {
        if(_theDialog != null)
        {
            _theDialog.Dispose();
            _theDialog = null;
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
            _group0 = _theDialog.TopBlock.FindBlock("group0");
            _faceSelect0 = _theDialog.TopBlock.FindBlock("face_select0");
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Message.Show(ex);
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
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Message.Show(ex);
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
            Message.Show(ex);
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
            if(block == _faceSelect0)
            {
            //---------Enter your code here-----------
                Logger.WriteLine("����� ����� �����.");
                SetFirstFace(block);
            }
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Message.Show(ex);
        }
        return 0;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: ok_cb
    //------------------------------------------------------------------------------
    private int ok_cb()
    {
        int errorCode = 0;
        try
        {
            errorCode = apply_cb();
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            errorCode = 1;
            Message.Show(ex);
        }
        return errorCode;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: cancel_cb
    //------------------------------------------------------------------------------
    private int cancel_cb()
    {
        try
        {
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Message.Show(ex);
        }
        return 0;
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
            Message.Show(ex);
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
            Message.Show(ex);
        }
    }

    void SetFirstFace(UIBlock block)
    {
        if (SetFace(block, out _point, out _direct))
        {
            Message.Tst(_point, _direct[0], _direct[1], _direct[2]);
        }

    }
    static bool SetFace(UIBlock block, out Point3d point3D, out double[] direction)
    {
        point3D = new Point3d();
        direction = new double[3];

        PropertyList propertyList = block.GetProperties();
        TaggedObject[] to = propertyList.GetTaggedObjectVector("SelectedObjects");

        //���� �� ��������
        if (to.Length > 0)
        {
            TaggedObject face = to[0];

            int type;
            double voidDouble;
            double[] dir = new double[3];
            double[] box = new double[6];
            double[] point = new double[3];

            Config.TheUfSession.Modl.AskFaceData(face.Tag, out type, point, dir, box, out voidDouble, out voidDouble, out type);

            //�������������� �����
            if (type == 16)
            {
                point3D.X = point[0];
                point3D.Y = point[1];
                point3D.Z = point[2];

                direction = dir;

                return true;
            }
            const string message = "����� �� ��������������! �������� ������ �����!";
            Logger.WriteWarning(message + Environment.NewLine + "������� ����� - " + face);
            Message.Show(message);
            UnSelectObjects(block);
            return false;
        }
        Logger.WriteLine("�������� �����");
        return false;
    }

    static void UnSelectObjects(UIBlock block)
    {
        PropertyList propList = block.GetProperties();
        propList.SetTaggedObjectVector("SelectedObjects", new TaggedObject[0]);
    }
}
