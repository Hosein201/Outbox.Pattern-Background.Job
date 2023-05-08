namespace Info.Model
{
    public class AppSetting
    {
        public RediSearchConfig RediSearchConfig { get; set; }
    }
    public class RediSearchConfig
    {
        public string Conn { get; set; }
    }
}