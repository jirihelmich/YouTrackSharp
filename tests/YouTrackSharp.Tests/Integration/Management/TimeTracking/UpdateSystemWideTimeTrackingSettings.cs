using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using YouTrackSharp.Management;
using YouTrackSharp.Tests.Infrastructure;

namespace YouTrackSharp.Tests.Integration.Management.TimeTracking
{
    public partial class TimeTrackingManagementServiceTests
    {
        public class UpdateSystemWideTimeTrackingSettings
		{
			[Fact]
			public async Task Valid_Connection_Updates_Systemwide_TimeTracking_Settings()
			{
				// Arrange
				var connection = Connections.Demo3Token;
				var service = connection.CreateTimeTrackingManagementService();
				
				var random = new Random();
				var hoursADay = random.Next(1, 20);

				// Act
				var timeSettings = new SystemWideTimeTrackingSettings
				{
					HoursADay = hoursADay,
					WorkDays = new List<SubValue<int>>(5)
				};
				timeSettings.WorkDays.Add(new SubValue<int> { Value = 1 });
				timeSettings.WorkDays.Add(new SubValue<int> { Value = 2 });
				timeSettings.WorkDays.Add(new SubValue<int> { Value = 3 });
				timeSettings.WorkDays.Add(new SubValue<int> { Value = 4 });
				timeSettings.WorkDays.Add(new SubValue<int> { Value = 5 });

				await service.UpdateSystemWideTimeTrackingSettings(timeSettings);

				// Assert
				var result = await service.GetSystemWideTimeTrackingSettings();
				Assert.NotNull(result);
				Assert.Equal(hoursADay, result.HoursADay);
			}
		}
    }
}