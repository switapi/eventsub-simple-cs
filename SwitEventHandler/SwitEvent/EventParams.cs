using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SwitEvent
{
	public class EventAPIParam
	{
		public string event_app_id;
		public string cmp_id;
		public JObject data;
		public string event_type;
		public string verification_code;
	}

	public static class EventParams
    {
		public static EventAPIParam ParseBody(string body)
        {
			EventAPIParam evt = null;

			try
            {
				evt = JsonConvert.DeserializeObject<EventAPIParam>(body);
			}
			catch
            {
				throw;
            }

			return evt;
        }

		public static T ParseAPI<T>(JObject data)
		{
			T api = default(T);

			try
			{
				api = data.ToObject<T>();
			}
			catch
			{
				throw;
			}

			return api;
		}
	}

}
