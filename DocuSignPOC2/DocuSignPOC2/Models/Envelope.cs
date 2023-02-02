namespace DocuSignPOC2.Models
{
    public class Envelope
    {
        public Guid Id{ get; set; }        
        public virtual List< Party>? Parties { get; set; }
        public virtual List<ESignDocument>? ESignDocuments { get; set; }
        public string? DocuSignId { get; set; }
    }
}
