using System;
using System.Collections.Generic;

// namespace unit_testing.functions
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             ConfigManager config1 = new ConfigManager();
//             config1.SetConfig("theme", "dark");
//
//             DatabaseManager db1 = new DatabaseManager();
//             db1.Connect();
//
//             Logger logger = new Logger();
//             logger.Log("Application started");
//
//             UserInterface ui = new UserInterface();
//             ui.DisplayMainMenu();
//
//             UserService service = new UserService();
//             service.LoadUser("Alice");
//
//             Console.WriteLine("Main Program Theme: " + config1.GetConfig("theme"));
//         }
//     }
//
//     class ConfigManager
//     {
//         private static ConfigManager _instance;
//         private Dictionary<string, string> _settings = new Dictionary<string, string>();
//
//         public static ConfigManager Instance
//         {
//             get
//             {
//                 if (_instance == null)
//                 {
//                     _instance = new ConfigManager();
//                 }
//                 return _instance;
//             }
//         }
//         public void SetConfig(string key, string value)
//         {
//             _settings[key] = value;
//         }
//
//         public string GetConfig(string key)
//         {
//             return _settings.ContainsKey(key) ? _settings[key] : "undefined";
//         }
//     }
//
//     class DatabaseManager
//     {
//         private static DatabaseManager _instance;
//         public static DatabaseManager Instance
//         {
//             get
//             {
//                 if (_instance == null)
//                 {
//                     _instance = new DatabaseManager();
//                 }
//                 return _instance;
//             }
//         }
//         public void Connect()
//         {
//             Console.WriteLine("Connecting to database... (new connection each time)");
//         }
//
//         public string Query(string sql)
//         {
//             Console.WriteLine("Executing query: " + sql);
//             return "Result for [" + sql + "]";
//         }
//     }
//
//     class Logger
//     {
//         private static Logger _instance;
//         public static Logger Instance
//         {
//             get
//             {
//                 if (_instance == null)
//                 {
//                     _instance = new Logger();
//                 }
//                 return _instance;
//             }
//         }
//         public void Log(string message)
//         {
//             Console.WriteLine("[LOG " + DateTime.Now + "] " + message);
//         }
//     }
//
//     class UserInterface
//     {
//         public void DisplayMainMenu()
//         {
//             Logger.Instance.Log("Displaying main menu...");
//             string theme = ConfigManager.Instance.GetConfig("theme");
//             Console.WriteLine($"Main menu loaded with theme: {theme}");
//         }
//     }
//
//     class UserService
//     {
//         private ConfigManager config = new ConfigManager();
//         private Logger logger = new Logger();
//
//         public void LoadUser(string username)
//         {
//             Logger.Instance.Log("Loading user: " + username);
//             DatabaseManager.Instance.Connect();
//             string result = DatabaseManager.Instance.Query("SELECT * FROM Users WHERE Name = '" + username + "'");
//             Logger.Instance.Log("User data loaded: " + result);
//
//             string theme = ConfigManager.Instance.GetConfig("theme");
//             Console.WriteLine("User " + username + " prefers theme: " + theme);
//         }
//     }
// }
