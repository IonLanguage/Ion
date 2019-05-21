using Ion.NoticeReporting;

namespace Ion.Parsing
{
    public struct DriverResult
    {
        public ReadOnlyNoticeStack Notices { get; set; }

        public string OutputIr { get; set; }
    }
}
