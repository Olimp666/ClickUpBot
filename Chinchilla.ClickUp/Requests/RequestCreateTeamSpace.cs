using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Requests
{
	/// <summary>
	/// Request object for method CreateTeamSpace()
	/// </summary>
	[Serializable]
	[DataContract]
	public class RequestCreateTeamSpace
	{
		#region Attributes

		/// <summary>
		/// Name of the new list
		/// </summary>
		[JsonProperty("name")]
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[JsonProperty("features")]
		[DataMember(Name = "features")]
		public TeamSpaceFeatures Features { get; set; }

		#endregion


		#region Constructor

		/// <summary>
		/// The constructor of RequestCreateList
		/// </summary>
		/// <param name="name"></param>
		public RequestCreateTeamSpace(string name)
		{
			Name = name;
			Features = new TeamSpaceFeatures
			{
				DueDates = new TeamSpaceFeatures.DueDateFlags { Enabled = true, EnableStartDates = true, EnableRemapingOfDueDates = true },
				TimeTracking = new TeamSpaceFeatures.Flags { Enabled = true },
				Tags = new TeamSpaceFeatures.Flags { Enabled = true },
				TimeEstimates = new TeamSpaceFeatures.Flags { Enabled = true },
				Checklists = new TeamSpaceFeatures.Flags { Enabled = true },
				RemapDependencies = new TeamSpaceFeatures.Flags { Enabled = true },
				DependencyWarning = new TeamSpaceFeatures.Flags { Enabled = true },
				Portfolios = new TeamSpaceFeatures.Flags { Enabled = true }
			};
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Validation method of data
		/// </summary>
		public void ValidateData()
		{
			if (string.IsNullOrEmpty(Name))
			{
				throw new ArgumentNullException("Name");
			}
		}

		#endregion

		[Serializable]
		[DataContract]
		public class TeamSpaceFeatures
		{
			[JsonProperty("due_dates")]
			[DataMember(Name = "due_dates")]
			public DueDateFlags DueDates { get; set; }

			[JsonProperty("time_tracking")]
			[DataMember(Name = "time_tracking")]
			public Flags TimeTracking { get; set; }

			[JsonProperty("tags")]
			[DataMember(Name = "tags")]
			public Flags Tags { get; set; }

			[JsonProperty("time_estimates")]
			[DataMember(Name = "time_estimates")]
			public Flags TimeEstimates { get; set; }

			[JsonProperty("portfolios")]
			[DataMember(Name = "portfolios")]
			public Flags Portfolios { get; set; }

			[JsonProperty("checklists")]
			[DataMember(Name = "checklists")]
			public Flags Checklists { get; set; }

			[JsonProperty("remap_dependencies")]
			[DataMember(Name = "remap_dependencies")]
			public Flags RemapDependencies { get; set; }

			[JsonProperty("dependency_warning")]
			[DataMember(Name = "dependency_warning")]
			public Flags DependencyWarning { get; set; }

			[Serializable]
			[DataContract]
			public class Flags
			{
				[JsonProperty("enabled")]
				[DataMember(Name = "enabled")]
				public bool Enabled { get; set; }
			}

			[Serializable]
			[DataContract]
			public class DueDateFlags : Flags
			{
				[JsonProperty("start_date")]
				[DataMember(Name = "start_date")]
				public bool EnableStartDates { get; set; }

				[JsonProperty("remap_due_dates")]
				[DataMember(Name = "remap_due_dates")]
				public bool EnableRemapingOfDueDates { get; set; }
			}
		}
	}
}