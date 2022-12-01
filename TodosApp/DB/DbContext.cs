using System.Data.SQLite;

namespace TodosApp.DB;

public class DbContext
{
    private static DbContext? _instance = null;
    private static readonly object Padlock = new object();

    private readonly string _path = "db.sql";
    private bool _open = false;
    public SQLiteConnection Connection;

    private DbContext()
    {
        Connection = new SQLiteConnection(String.Format("Data source={0}", _path));

        if (!File.Exists(_path))
        {
            SQLiteConnection.CreateFile(_path);
        }

        Connection.Open();
        _open = true;
    }
    
    public static DbContext Instance
    {
        get
        {
            lock (Padlock)
            {
                if (_instance == null)
                {
                    _instance = new DbContext();
                }
                
                return _instance;
            }
        }
    }

    public void OpenConnectionIfClosed()
    {
        if (!_open)
        {
            Connection.Open();
            _open = true;
        }
    }
    
    public void CloseConnectionIfOpen()
    {
        if (_open)
        {
            Connection.Close();
            _open = false;
        }
    }
}