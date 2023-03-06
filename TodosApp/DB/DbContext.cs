using System.Data.SQLite;

namespace TodosApp.DB;

public class DbContext
{
    public SQLiteConnection Connection;
    
    private static DbContext? _instance = null;
    private static readonly object Padlock = new object();

    private readonly string _path = "todos_app.db";
    private bool _open = false;
    private readonly string _seed = @"
CREATE TABLE user ( 
	Id                  INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	Identifier          VARCHAR(255)  NOT NULL     ,
	Nickname            VARCHAR(100)  NOT NULL     ,
	CreatedAt           DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
	UpdatedAt           DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL
 );

CREATE  TABLE todo ( 
	Id                  INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	Author              INTEGER UNSIGNED NOT NULL     ,
	Assignee            INTEGER UNSIGNED NOT NULL     ,
	Task                CHAR(255)       ,
	Description         TEXT       ,
	Completed           BIT  NOT NULL DEFAULT (0)    ,
	CreatedAt           DATETIME  NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	UpdatedAt           DATETIME  NOT NULL DEFAULT (CURRENT_TIMESTAMP),
  	FOREIGN KEY(Author, Assignee) REFERENCES user(id, id)
 );
";

    private DbContext()
    {
        Connection = new SQLiteConnection(String.Format("Data source={0}", _path));

        if (!File.Exists(_path))
        {
            SQLiteConnection.CreateFile(_path);
            
            Console.WriteLine("creating the database");
        
            Connection.Open();
            _open = true;
            
            var seedCommand = new SQLiteCommand(_seed, Connection);
            seedCommand.ExecuteNonQuery();
        
            Console.WriteLine("the database is created");
        }

        OpenConnectionIfClosed();
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