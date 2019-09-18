using System;
using Microsoft.Owin.Hosting;

namespace TodoApi
{
	public static class Server
	{
		/// <summary>
		/// Start the server at the specified port.
		/// </summary>
		/// <param name="port">Port.</param>
		public static void Start(int port)
		{
			var url = $"http://localhost:{port}";
			using (WebApp.Start<Startup>(url))
			{
				Console.WriteLine($"Web Server is running at {url}.");
				Console.WriteLine("Press any key to quit.");
				Console.ReadLine();
			}
		}
	}
}
