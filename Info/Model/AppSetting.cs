namespace Info.Model
{
    public class AppSetting
    {
        public DbContextConfiguration DbContextConfiguration { get; set; }
        public RediSearchConfiguration RediSearchConfiguration { get; set; }
    }
    public class RediSearchConfiguration
    {
        public string Conn { get; set; }
    }

    public class DbContextConfiguration
    {
        public string Conn { get; set; }
    }
}