using FluentValidation;
namespace WebApi.Application.BookOperations.Queries.GetBooksDetail
{
    public class GetbooksDetailQueryValidator:AbstractValidator<GetbooksDetailQuery>
    {
      public  GetbooksDetailQueryValidator()
      {
RuleFor(query=> query.BookId ).GreaterThan(0);
      }
    }
}