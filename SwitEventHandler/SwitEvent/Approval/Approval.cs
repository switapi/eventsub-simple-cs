using System;

namespace SwitEvent.Approval
{
	/// <summary>
	/// The approver who can reject or approve the docs
	/// </summary>
	public class Approver
	{
		/// <summary>
		/// The user id for approver
		/// </summary>
		public string approver_user_id;

		/// <summary>
		/// The status of approver
		/// </summary>
		public Int32 approver_status;

		/// <summary>
		/// The date of action
		/// </summary>
		public string action_date;

		/// <summary>
		/// Step refers which step(1 to 5) the approver is assigned
		/// </summary>
		public Int32 step;
	}

	/// <summary>
	/// StepListOfApprovers is the list of Approvers in steps
	/// Approvers are assigned to consecutive steps (from 1 to 5)
	/// Approvers can only act(approve/reject) or receive the doc when the previous step is done
	/// e.g. Approvers in step 2 will receive the doc only if the first step is approved
	/// </summary>
	public class StepListOfApprovers
	{
		/// <summary>
		/// Step refers each step of the approval
		/// </summary>
		public Int32 step;

		/// <summary>
		/// The list of approvers of the Step
		/// </summary>
		public Approver[] approvers;

		/// <summary>
		/// The status of the Step
		/// Default would be 1 (1:waiting, 2:appproved, 3:rejected)
		/// When Approvers takes action, then the status will be changed accordingly
		/// </summary>
		public Int32 step_status;
	}

	/// <summary>
	/// Referrer is the referrer of approval,
	/// who can view the doc but cannot reject or approve
	/// </summary>
	public class Referrer
	{
		/// <summary>
		/// The user id for referrer
		/// </summary>
		public string referrer_user_id;

		/// <summary>
		/// The type for register
		/// </summary>
		public Int32 register_type;
	}

	/// <summary>
	/// Asset is the attached file information for approval doc
	/// </summary>
	public class Asset
	{
		/// <summary>
		/// The asset id of approval
		/// </summary>
		public string approval_asset_id;

		/// <summary>
		/// The id of approval
		/// </summary>
		public string approval_id;

		/// <summary>
		/// The id of approval content
		/// </summary>
		public string approval_cont_id;

		/// <summary>
		/// The path of file
		/// </summary>
		public string file_path;

		/// <summary>
		/// The name of file
		/// </summary>
		public string file_name;

		/// <summary>
		/// The description of file
		/// </summary>
		public string file_desc;

		/// <summary>
		/// The mime of file
		/// </summary>
		public string file_mime;

		/// <summary>
		/// The size of file
		/// </summary>
		public UInt32 file_size;

		/// <summary>
		/// The coordination of image X
		/// </summary>
		public Int32 image_x;

		/// <summary>
		/// The coordination of image Y
		/// </summary>
		public Int32 image_y;

		/// <summary>
		/// The thumbnail of file
		/// </summary>
		public string tumbnail;

		/// <summary>
		/// the user id
		/// </summary>
		public string user_id;

		/// <summary>
		/// Created represent when Asset is created
		/// </summary>
		public string created;
	}

	/// <summary>
	/// Substitute means a user assigned by the target user(current user)
	/// who can approve or reject approvals instead
	/// </summary>
	public class Substitute
	{
		/// <summary>
		/// The user id of target user (current user)
		/// </summary>
		public string target_user_id;

		/// <summary>
		/// The user id of substitute
		/// </summary>
		public string substitute_user_id;
	}

	/// <summary>
	/// ApprovalInfo is the detailed information of approval
	/// </summary>
	public class ApprovalInfo
	{
		/// <summary>
		/// The id of approval
		/// </summary>
		public string approval_id;

		/// <summary>
		/// The id of document (displayed id)
		/// </summary>
		public string doc_id;

		/// <summary>
		/// The id of company
		/// </summary>
		public string cmp_id;

		/// <summary>
		/// The user id who requests approval
		/// </summary>
		public string req_user_id;

		/// <summary>
		/// The title of approval
		/// </summary>
		public string title;

		/// <summary>
		/// The body text of approval
		/// </summary>
		public string body_text;

		/// <summary>
		/// The body blockskit of approval
		/// </summary>
		public string body_blockskit;

		/// <summary>
		/// The content id of approval
		/// </summary>
		public string approval_cont_id;

		/// <summary>
		/// The status of approval
		/// </summary>
		public Int32 approval_status;

		/// <summary>
		/// public_type represents degree of disclosure (private or open to a certain degree)
		/// </summary>
		public Int32 public_type;

		/// <summary>
		/// requested represents requested date
		/// </summary>
		public string requested;

		/// <summary>
		/// The list of approvers in steps
		/// </summary>
		public StepListOfApprovers[] step_approvers;

		/// <summary>
		/// The list of referrer teams (assigned team can receive as a unit)
		/// </summary>
		public string[] referrer_teams;

		/// <summary>
		/// The list of referrer
		/// </summary>
		public Referrer[] referrers;

		/// <summary>
		/// The count of total step
		/// </summary>
		public Int32 step_count;

		/// <summary>
		/// completed_step represent completed or not for step
		/// </summary>
		public Int32 completed_step;

		/// <summary>
		/// created represent when Approval is created
		/// </summary>
		public string created;

		/// <summary>
		/// modified represent when Approval is modified
		/// </summary>
		public string modified;

		/// <summary>
		/// The category id of approval
		/// </summary>
		public string approval_category_id;

		/// <summary>
		/// The condition of approve (approve action policy - and, or)
		/// </summary>
		public Int32 approve_condition;

		/// <summary>
		/// The category name of approval
		/// </summary>
		public string approval_category_name;

		/// <summary>
		/// The user id of action
		/// </summary>
		public string action_user_id;

		/// <summary>
		/// The list of Asset
		/// </summary>
		public Asset[] asset_data;

		/// <summary>
		/// The content for HTML
		/// </summary>
		public string html_content;

		/// <summary>
		/// is_html_editable represent HTML is editable or not
		/// </summary>
		public bool is_html_editable;

		/// <summary>
		/// The list of substitute
		/// </summary>
		public Substitute[] substitute;

		/// <summary>
		/// is_last_approve indicates the end of approval
		/// </summary>
		public bool is_last_approve;

        /// <summary>
        /// last_approve_referrer_teams is the list of referrer teams for last approval
        /// </summary>
        public string[] last_approve_referrer_teams;
	}
}