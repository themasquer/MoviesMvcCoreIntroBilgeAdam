using _038_MoviesMvcCoreIntroBilgeAdam.Models;

namespace _038_MoviesMvcCoreIntroBilgeAdam.Services.Bases
{
    public interface IReviewService
    {
        IQueryable<ReviewModel> Query();
        ResultStatus Add(ReviewModel model);
        ResultStatus Update(ReviewModel model);
        ResultStatus Delete(int id);
    }
}
