using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Responses.Model
{

	/// <summary>
	/// Model object of Features information response
	/// </summary>
	public class ResponseModelFeatures
		: Helpers.IResponse
	{
		/// <summary>
		/// Model Due Dates that contains the information of this Features object
		/// </summary>
		[JsonProperty("due_dates")]
		public ResponseModelDueDates DueDates { get; set; }

		/// <summary>
		/// Model Time Tracking that contains the information of this Features object
		/// </summary>
		[JsonProperty("time_tracking")]
		public ResponseModelTimeTracking TimeTracking { get; set; }

		/// <summary>
		/// Model Priorities that contains the information of this Features object
		/// </summary>
		[JsonProperty("priorities")]
		public ResponseModelPriorities Priorities { get; set; }

		/// <summary>
		/// Model Tags that contains the information of this Features object
		/// </summary>
		[JsonProperty("tags")]
		public ResponseModelTag Tags { get; set; }

		/// <summary>
		/// Model Time Estimates that contains the information of this Features object
		/// </summary>
		[JsonProperty("time_estimates")]
		public ResponseModelTimeEstimates TimeEstimates { get; set; }

		/// <summary>
		/// Model Custom Fields that contains the information of this Features object
		/// </summary>
		[JsonProperty("custom_fields")]
		public ResponseModelCustomFields CustomFields { get; set; }

		/// <summary>
		/// Model Dependency Warning that contains the information of this Features object
		/// </summary>
		[JsonProperty("dependency_warning")]
		public ResponseModelDependencyWarning DependencyWarning { get; set; }

		/// <summary>
		/// Model Multiple Assignees that contains the information of this Features object
		/// </summary>
		[JsonProperty("multiple_assignees")]
		public ResponseModelMultipleAssignees MultipleAssignees { get; set; }
	}
}