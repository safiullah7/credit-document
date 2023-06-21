// SharedModels/Models.cs

using System.ComponentModel.DataAnnotations;

namespace SharedModels
{
    public class CreditDocument
    {
        public int Id { get; set; }
        public string? LoanNumber { get; set; }
        
        [Required(ErrorMessage="edit this message")]
        public string? CompanyName { get; set; }
        
        [Required(ErrorMessage="edit this message")]
        public string? OrganizationNumber { get; set; }
        public List<Person> Guarantors { get; set; }
        public List<Person> Signatories { get; set; }
        //public List<Person> Guarantors { get; set; } = new List<Person>();
        //public List<Person> Signatories { get; set; } = new List<Person>();
         //public List<DocumentAttachment> Attachments { get; set; } = new List<DocumentAttachment>();
        public DateTime Date { get; set; }
       public List<DocumentAttachment> Attachments { get; set; }
        public DocumentState State { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PersonalNumber { get; set; }
        public string? Email { get; set; }
    }

    public class DocumentAttachment
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? FileUrl { get; set; }
        public string? Status { get; set; }
    }

    public enum DocumentState
    {
        New,
        UnderProcessing,
        OnHold,
        Completed
    }
}