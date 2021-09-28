namespace SwitEvent
{
    public class EventType
    {
		// EventURLVerification is an event used when configuring your EventsAPI app
		public const string EventURLVerification = "url_verification";

		// EventApprovalCreate is an event used when creating approval
		public const string EventApprovalCreate = "approval.create";

		// EventApprovalCreate is an event used when updating approval
		public const string EventApprovalUpdate = "approval.update";

		// EventApprovalCreate is an event used when deleting approval
		public const string EventApprovalDelete = "approval.delete";

		// EventApprovalCreate is an event used when requesting approval
		public const string EventApprovalRequest = "approval.request";

		// EventApprovalCreate is an event used when recalling approval
		public const string EventApprovalRecall = "approval.recall";

		// EventApprovalCreate is an event used when approving approval
		public const string EventApprovalApprove = "approval.approve";

		// EventApprovalCreate is an event used when rejecting approval
		public const string EventApprovalReject = "approval.reject";

		// EventApprovalCreate is an event used when approving last
		public const string EventApprovalLastApprove = "approval.last_approve";
    }
}
