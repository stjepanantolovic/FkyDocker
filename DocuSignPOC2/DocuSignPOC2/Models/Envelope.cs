namespace DocuSignPOC2.Models
{
    public class Envelope
    {
        public Guid Id{ get; set; }        
        public virtual List< Party>? Parties { get; set; }
        public virtual List<ESignDocument>? ESignDocuments { get; set; }
        public string? DocuSignId { get; set; }
        public DateTime? AgentSignTimeStamp { get; set; }
        public DateTime? ProducerSignTimeStamp { get; set; }
        public DateTime? EnvelopeCompletedTimeStamp { get; set; }
        public DateTime? EnvelopeSentToDocuSignTimeStamp { get; set; }
    }
}
