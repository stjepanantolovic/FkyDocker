namespace DocuSignPOC2.Models
{
    public class Party
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid? DocuSignId { get; set; }
        public virtual List<Envelope>? Envelopes { get; set; }

    }
}
