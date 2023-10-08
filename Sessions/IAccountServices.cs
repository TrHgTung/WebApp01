using WebApp01.Models;

namespace WebApp01.Sessions
{
	public interface IAccountServices
	{
		public AdminModel Login(string username, string password);
	}
}
