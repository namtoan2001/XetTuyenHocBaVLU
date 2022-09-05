namespace XetTuyenVLU.ViewModels.Schedule
{
    public class ScheduleCreateRequest
    {
        public string DotId { get; set; }
        public string HinhThucId { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
    }
}
