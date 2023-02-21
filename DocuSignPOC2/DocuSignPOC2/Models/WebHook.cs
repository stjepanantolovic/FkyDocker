namespace DocuSignPOC2.Models
{
    public class WebHook
    {
        public Guid Id { get; set; }
        public string Json { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
