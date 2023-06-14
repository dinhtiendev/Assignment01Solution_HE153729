using System;
using DataAccess.Models;

namespace DataAccess.DAOs
{
	public class DbContextSingleton
	{
		private static Assignment1APIContext _instance;

		public static Assignment1APIContext Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new Assignment1APIContext();
				}
				return _instance;
			}
		}
	}
}

