namespace DocuSignPOC2.Models
{
    public class AgentProducerEnvelope
    {
        public Party? Agent { get; set; }
        public Party? Producer { get; set; }
        public string DocumentBase64 { get; set; }

    }
}
