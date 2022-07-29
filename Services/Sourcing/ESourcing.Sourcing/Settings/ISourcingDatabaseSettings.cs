namespace ESourcing.Sourcing.Settings
{
    public interface ISourcingDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
