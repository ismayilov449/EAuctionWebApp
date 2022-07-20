namespace ESourcing.Sourcing.Settings
{
    public class SourcingDatabaseSettings : ISourcingDatabaseSettings
    {
        public string ConnectionString { get ; set; }
        public string DatabaseName { get; set; }
    }
}
