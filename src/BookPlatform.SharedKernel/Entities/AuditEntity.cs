namespace BookPlatform.SharedKernel.Entities;

public class AuditEntity : Entity
{
    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}