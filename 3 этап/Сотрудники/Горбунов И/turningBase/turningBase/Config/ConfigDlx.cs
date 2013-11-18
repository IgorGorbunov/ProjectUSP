using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Класс настроек форм.
/// </summary>
public static class ConfigDlx
{
    /// <summary>
    /// Папка с формами для диалогов.
    /// </summary>
    public const string DlxFolder = @"dialogs";
    /// <summary>
    /// Имя файла с формой базирования элементов по отверстиям.
    /// </summary>
    public const string DlxTunnelTunnel = @"tunnel+tunnel.dlx";
    /// <summary>
    /// Имя файла с формой для базирования отверстие-паз.
    /// </summary>
    public const string DlxTunnelSlot = @"tunnel+slot.dlx";
    /// <summary>
    /// Имя файла с формой c двумя точками.
    /// </summary>
    public const string DlxPointPoint = @"point+point.dlx";
    /// <summary>
    /// Возвращает имя файла с формой для выгрузки токарной базы.
    /// </summary>
    public const string DlxTurningBase = @"turningBase.dlx";
    /// <summary>
    /// Возвращает имя файла с формой для выгрузки общей базы.
    /// </summary>
    public const string DlxMilingBase = @"milingBase.dlx";
    /// <summary>
    /// Возвращает имя файла с формой для установки кондуктора.
    /// </summary>
    public const string DlxJig = @"jig.dlx";
    /// <summary>
    /// Возвращает имя файла с общей формой.
    /// </summary>
    public const string Dlx = @"buttons.dlx";
    /// <summary>
    /// Возвращает имя файла с формой для набора высоты.
    /// </summary>
    public const string DlxHeight = @"heightSet.dlx";
    /// <summary>
    /// Возвращает имя файла с формой для набора угла.
    /// </summary>
    public const string DlxAngle = @"angleSet.dlx";
    /// <summary>
    /// Возвращает имя файла с формой для поворота элемента.
    /// </summary>
    public const string DlxTurn = @"turnElement.dlx";
    /// <summary>
    /// Возвращает имя файла с формой для вставки болта в паз.
    /// </summary>
    public const string DlxSetBoltInSlot = @"setBoltInSlot.dlx";


    public static void UnloadDialog(string name)
    {
        string dialogsPath = Path.Combine(Path.GetTempPath(), Config.TmpFolder);
        Directory.CreateDirectory(dialogsPath);

        dialogsPath = Path.Combine(dialogsPath, Config.OurTmpFolder);
        Directory.CreateDirectory(dialogsPath);

        dialogsPath = Path.Combine(dialogsPath, DlxFolder);
        Directory.CreateDirectory(dialogsPath);

        string fullPath = Path.Combine(dialogsPath, name);
        if (File.Exists(fullPath))
        {
            string etalonHash = GetDlxHashSum(name);
            string fileHash = Instr.ComputeMd5Checksum(fullPath);
            if (etalonHash != fileHash)
            {
                File.Delete(fullPath);
            }
        }

        UnloadDlx(name, dialogsPath);
    }

    private static void UnloadDlx(string name, string path)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary.Add("name", name);

        string query = Sql.GetBegin(CFile) + From;
        query += Sql.Where + Sql.Equal(CName, Sql.Par("name"));

        if (!SqlOracle.UnloadFile(query, path,
                             dictionary))
        {
            throw new TimeoutException();
        }
        
    }

    //----------------------------------------------------------------

    private const string Name = "USP_TEMPLATES";
    private const string From = "from " + Name;

    private const string CName ="NAME";
    private const string CHashSum = "HASHFILE";
    private const string CFile = "FILEBLOB";


    private static string GetDlxHashSum(string name)
    {
        Dictionary<string, string> paramDict = new Dictionary<string, string>();
        paramDict.Add("name", name);

        string query = Sql.GetBegin(CHashSum) + From;
        query += Sql.Where + Sql.Equal(CName, Sql.Par("name"));

        string sum;
        if (SqlOracle.Sel(query, paramDict, out sum))
        {
            return sum;
        }
        throw new TimeoutException();
    }
}