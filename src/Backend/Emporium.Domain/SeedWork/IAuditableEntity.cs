namespace Emporium.Domain.SeedWork;
public interface IAuditableEntity
{
    string CreatedBy { get; set; }
    DateTime CreatedAt { get; set; }
    string? UpdatedBy { get; set; }
    DateTime? UpdatedAt { get; set; }
    string? DeletedBy { get; set; }
    DateTime? DeletedAt { get; set; }
}
