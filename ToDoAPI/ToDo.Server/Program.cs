using System;
using Microsoft.Owin.Hosting;

namespace TodoApi
{
	public class Program
	{
		protected static void Main(string[] args)
		{
			var port = 8080;
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
