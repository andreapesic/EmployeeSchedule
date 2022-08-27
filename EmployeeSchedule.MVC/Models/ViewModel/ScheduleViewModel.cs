using EmployeeSchedule.Data.Entities;
using System;

namespace EmployeeSchedule.MVC.Models.ViewModel
{
    public class ScheduleViewModel : Schedule
    {
        public CheckInStatus CheckInStatus { get; set; }
        public ScheduleViewModel()
        {

            if ((CheckInTime == DateTime.MinValue))
            {
                if(Date.Date < DateTime.Now.Date)
                {
                    CheckInStatus = CheckInStatus.NotRequired;
                    return;
                }

                CheckInStatus = Date > DateTime.Now ? CheckInStatus.Late : CheckInStatus.CheckIn;
                return;
            }
            else
            {
                CheckInStatus = ShiftWork == "Prva" && CheckInTime.Hour < 8 ? CheckInStatus.OnTime 
                : ShiftWork == "Druga" && CheckInTime.Hour < 15 ? CheckInStatus.OnTime : CheckInStatus.Late;
            }

        }
    }

    public enum CheckInStatus
    {
        Late, OnTime, CheckIn, NotRequired, FreeDay, WaitingSchedule,
        NotAvailableForCheckIn
    }
}
