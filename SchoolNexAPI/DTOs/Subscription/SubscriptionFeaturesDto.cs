namespace SchoolNexAPI.DTOs.Subscription
{
    public class SubscriptionFeaturesDto
    {
        public bool ManageStudents { get; set; }
        public bool ManageEmployees { get; set; }
        public bool ManageAttendance { get; set; }
        public bool ManageFees { get; set; }
        public bool ViewReports { get; set; }
        public bool SendNotifications { get; set; }
        public bool ManageExams { get; set; }
        public bool AccessLibrary { get; set; }
        public bool AccessTransport { get; set; }
        public bool AccessHostel { get; set; }
        public bool CustomizeSettings { get; set; }
    }
}
