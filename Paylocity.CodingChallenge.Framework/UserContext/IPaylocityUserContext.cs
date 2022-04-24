namespace Paylocity.CodingChallenge.Framework
{
    public interface IPaylocityUserContext
    {
        int Id { get; set; }

        Guid? UserId { get; set; }

        string Email { get; set; }

        string? Name { get; set; }

        string? EmployeeId { get; set; }

        int? DepartmentId { get; set; }

    }
}
