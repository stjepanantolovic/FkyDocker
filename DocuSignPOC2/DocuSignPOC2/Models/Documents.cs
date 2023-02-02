namespace DocuSignPOC2.Models
{
    public class ESignDocument
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? DocuSignId { get; set; }
        public Guid? EnvelopeId { get; set; }
        public Envelope? Envelope { get; set; }
    }
}
