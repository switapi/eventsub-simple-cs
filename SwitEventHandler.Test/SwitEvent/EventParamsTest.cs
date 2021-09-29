using Xunit;
using SwitEvent;
using SwitEvent.Approval;

namespace SwitEventHandler.Test.SwitEvent
{
    public class EventParamsTest
    {
		[Fact]
		public void ParseBodyTest()
		{
			var body = EventSecurityTest.GetBodyString();
			var api = EventParams.ParseBody(body);
			Assert.NotNull(api);
		}

		[Fact]
		public void ParseAPITest()
		{
			var body = EventSecurityTest.GetBodyString();
			var api = EventParams.ParseBody(body);
			Assert.NotNull(api);

			var approval = EventParams.ParseAPI<ApprovalInfo>(api.data);
			Assert.NotNull(approval);
		}
	}
}
