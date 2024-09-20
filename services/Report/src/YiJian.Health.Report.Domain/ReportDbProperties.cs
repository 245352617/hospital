namespace YiJian.Health.Report
{
    public static class ReportDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Rp";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "Report";
    }
}
