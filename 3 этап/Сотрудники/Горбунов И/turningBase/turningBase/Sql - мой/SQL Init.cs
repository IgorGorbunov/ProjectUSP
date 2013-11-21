using System;
using System.Data;
using Devart.Data.Oracle;

/// <summary>
/// Класс c инициаизацией переменных и проверкой соединения с БД
/// </summary>
static partial class SqlOracle
{
    /// <summary>
    /// Объявление компонентов
    /// </summary>
    static OracleConnection _conn;

    private static string _connectionString;


    /// <summary>
    /// Cоединене с БД (второе исполнение)
    /// </summary>
    public static void _open()
    {
        try
        {
            _conn = new OracleConnection(_connectionString);
            _conn.Open();
            Logger.WriteLine("Соединение открыто!");
        }
        catch (Exception ex)
        {
            const string sss = "Попытка соединения с БД прошла неудачно!";
            Logger.WriteLine(sss, ex);
            throw new TimeoutException();
        }
    }
    public static void _close()
    {
        Logger.WriteLine(_conn.State + " - закрытие соединения...");
        bool closed = false;
        do
        {
            switch (_conn.State)
            {
                    case ConnectionState.Closed:
                    {
                        closed = true;
                        break;
                    }
                    case ConnectionState.Broken:
                    {
                        closed = true;
                        _conn.Close();
                        break;
                    }
                    case ConnectionState.Connecting:
                    {
                        break;
                    }
                    case ConnectionState.Executing:
                    {
                        break;
                    }
                    case ConnectionState.Fetching:
                    {
                        break;
                    }
                    case ConnectionState.Open:
                    {
                        closed = true;
                        _conn.Close();
                        break;
                    }
            }
        } while (!closed);
        Logger.WriteLine("Успешно!");
    }



    /// <summary>
    /// Метод построения строки соеднинения
    /// </summary>   
    /// <returns></returns>
    public static void BuildConnectionString(string user, string password, string dataSource)
    {
        _connectionString = "User id=" + user +
                            ";password=" + password +
                            ";Data Source = " + dataSource;
    }
    /// <summary>
    /// Метод построения строки соеднинения
    /// </summary>   
    /// <returns></returns>
    public static void BuildConnectionString(string user, string password, string dataSource, string host, string port)
    {
        _connectionString = "User id=" + user +
                                             ";password=" + password +
                                               ";Service Name  = " + dataSource +
                                                    ";Host = " + host +
                                                        ";Direct = true" +
                                                            ";Port = " + port;

    }


}