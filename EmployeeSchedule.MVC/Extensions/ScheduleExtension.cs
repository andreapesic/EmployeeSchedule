using EmployeeSchedule.MVC.Models.ViewModel;
using System;

namespace EmployeeSchedule.MVC.Extensions
{
    public static class ScheduleExtension
    {
        public static void SetCheckInStatus(this ScheduleViewModel schedule)
        {
            if (string.IsNullOrEmpty(schedule.ShiftWork))
            {
                schedule.CheckInStatus = CheckInStatus.WaitingSchedule;
                return;
            }

            if (schedule.ShiftWork == "Slobodan dan")
            {
                schedule.CheckInStatus = CheckInStatus.FreeDay;
                return;
            }

            if ((schedule.CheckInTime == DateTime.MinValue))
            {
                if (schedule.Date.Date < DateTime.Now.Date)
                {
                    schedule.CheckInStatus = CheckInStatus.NotRequired;
                    return;
                }

                schedule.CheckInStatus = (schedule.Date.Date > DateTime.Now.Date || (schedule.Date.Date== DateTime.Now.Date && DateTime.Now.Hour <7 && schedule.ShiftWork=="Prva")
                    || (schedule.Date.Date == DateTime.Now.Date && DateTime.Now.Hour < 14 && schedule.ShiftWork == "Druga")) 
                    ? CheckInStatus.NotAvailableForCheckIn : CheckInStatus.CheckIn;
                return;
            }
            else
            {
                schedule.CheckInStatus = schedule.ShiftWork == "Prva" && schedule.CheckInTime.Hour < 8 ? CheckInStatus.OnTime
                : schedule.ShiftWork == "Druga" && schedule.CheckInTime.Hour < 15 ? CheckInStatus.OnTime : CheckInStatus.Late;
            }
        }
    }
}
