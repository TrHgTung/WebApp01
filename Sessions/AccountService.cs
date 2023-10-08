using WebApp01.Models;

namespace WebApp01.Sessions
{
	public class AccountService : IAccountServices
	{
		private List<AdminModel> admins;
		public AccountService() {
			admins = new List<AdminModel>()
			{
				// super user admin
				new AdminModel()
				{
					Username = "admin",
					Password = "Test@123",
				},
				new AdminModel()
				{
					Username = "tung",
					Password = "Test@123",
				},
			};
		}
		public AdminModel Login(string username, string password)
		{
			return admins.SingleOrDefault(x => x.Username == username && x.Password == password);
		}
	}
}
