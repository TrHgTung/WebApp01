using Newtonsoft.Json;

namespace WebApp01.Repository
{
	public static class SessionExtensions
	{
		// Get
		public static void SetJson( this ISession session, string key , object value) 
		{
			session.SetString(key, JsonConvert.SerializeObject(value));
		}
		// Set
		public static T GetJson<T>(this ISession session, string key){
			var sessionData = session.GetString(key);
			return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
		}
	}
}
