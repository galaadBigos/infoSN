using InfoSN.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace InfoSN.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IDbConnection _dbConnection;

		public HomeController(ILogger<HomeController> logger, IDbConnection dbConnection)
		{
			_logger = logger;
			_dbConnection = dbConnection;
		}

		public IActionResult Index()
		{
			_dbConnection.Open();

			IDbCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "SELECT * FROM [User]";
			IDataReader reader = cmd.ExecuteReader();
			string id;
			string username;
			while (reader.Read())
			{
				id = reader.GetString(0);
				username = reader.GetString(1);

			}

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}