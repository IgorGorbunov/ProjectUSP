
using System.Data.OracleClient;
using UchetUSP.LOG;


/// <summary>
/// ����� c ������������� ���������� � ��������� ���������� � ��
/// </summary>
partial class SQLOracle
{

    /// <summary>
    /// ���������� �����������
    /// </summary>
    static OracleConnection _conn;

    static private OracleDataAdapter oracleDataAdapter1;

    static private System.Data.DataSet dataSet11;

    static private OracleCommand oracleSelectCommand1;

    static private OracleCommand oracleInsertCommand1;

    static private OracleCommand oracleUpdateCommand1;

    static private OracleDataReader reader;



    /// <summary>
    /// ��������� ��������� ������
    /// </summary>
    public static string _user = "";
    static string _password = "";
    static string _dataSource = "";

    public static string _connectionString;



    /// <summary>
    /// C�������� � �� (������ ����������)
    /// </summary>
    static void _open()
    {
        try
        {

            _conn = new OracleConnection(_connectionString);
            _conn.Open();
        }
        catch (System.Exception ex)
        {
            //System.Windows.Forms.MessageBox.Show(ex.ToString());
            Message.Show(ex);
        }
    }
    static void _close()
    {
        _conn.Close();
    }


    /// <summary>
    /// ������������� �������� ��� �������������������� SELECT
    /// </summary>
    static private void initObjectsForSelectQuery()
    {

        oracleDataAdapter1 = new OracleDataAdapter();

        _conn = new OracleConnection(_connectionString);

        dataSet11 = new System.Data.DataSet();

        oracleSelectCommand1 = new OracleCommand();

        oracleDataAdapter1.SelectCommand = oracleSelectCommand1;

        oracleSelectCommand1.Connection = _conn;

    }



    /// <summary>
    /// ������������� �������� ��� �������������������� INSERT
    /// </summary>
    static private void initObjectsForInsertQuery()
    {

        oracleDataAdapter1 = new OracleDataAdapter();

        _conn = new OracleConnection(_connectionString);

        dataSet11 = new System.Data.DataSet();

        oracleInsertCommand1 = new OracleCommand();

        oracleDataAdapter1.InsertCommand = oracleInsertCommand1;

        oracleInsertCommand1.Connection = _conn;
    }


    /// <summary>
    /// ������������� �������� ��� �������������������� UPDATE
    /// </summary>
    static private void initObjectsForUpdateQuery()
    {

        oracleDataAdapter1 = new OracleDataAdapter();

        _conn = new OracleConnection(_connectionString);

        dataSet11 = new System.Data.DataSet();

        oracleUpdateCommand1 = new OracleCommand();

        oracleDataAdapter1.UpdateCommand = oracleUpdateCommand1;

        oracleUpdateCommand1.Connection = _conn;
    }



    /// <summary>
    /// ���������� �������� �������������������� Select
    /// </summary>
    static private void disposeObjectsForSelectQuery()
    {
        oracleDataAdapter1.Dispose();

        _conn.Dispose();

        dataSet11.Dispose();

        oracleSelectCommand1.Dispose();

    }


    /// <summary>
    /// ���������� �������� �������������������� Insert
    /// </summary>
    static private void disposeObjectsForInsertQuery()
    {
        oracleDataAdapter1.Dispose();

        _conn.Dispose();

        dataSet11.Dispose();

        oracleInsertCommand1.Dispose();

    }


    /// <summary>
    /// ���������� �������� �������������������� Update
    /// </summary>
    static private void disposeObjectsForUpdateQuery()
    {
        oracleDataAdapter1.Dispose();

        _conn.Dispose();

        dataSet11.Dispose();

        oracleUpdateCommand1.Dispose();

    }


    /// <summary>
    /// ����� ���������� ������ �����������
    /// </summary>   
    /// <returns></returns>
    public static void BuildConnectionString(string user, string password, string dataSource)
    {
        _connectionString = "User id=" + user +
                                             ";password=" + password +
                                             ";Data Source = " + dataSource;

    }

    /// <summary>
    /// ����� �������� ���������� � �� Oracle
    /// </summary>   
    /// <returns></returns>
    public static bool CheckConnection()
    {
        initObjectsForSelectQuery();

        try
        {
            oracleSelectCommand1.Connection.Open();

            oracleSelectCommand1.Connection.Close();

            return true;

        }
        catch (OracleException ex)
        {
            //MessageBox.Show(ex.Message);
            Message.Show(ex);

            oracleSelectCommand1.Connection.Close();

            Log.WriteLog(ex);

            return false;

        }
        finally
        {
            disposeObjectsForSelectQuery();
        }

    }


}