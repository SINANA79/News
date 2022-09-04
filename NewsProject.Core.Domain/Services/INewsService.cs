using Microsoft.AspNetCore.Http;
using NewsProject.Core.Domain.Dtos.News;
using NewsProject.Core.Domain.Entities;
using NewsProject.Core.Domain.Entities.RequestParameters;

namespace NewsProject.Core.Domain.Services
{
    public interface INewsService
    {
        PagedList<NewsWithCategoryDto> GetAllNewses(NewsParameters newsParameters);
        PagedList<NewsWithCategoryDto> GetCategoryNewses(Guid categoryId, LatestNewsParameters newsParameters);
        IEnumerable<NewsWithCategoryDto> GetLast8Newses();
        PagedList<NewsWithCategoryDto> GetLatestNewses(LatestNewsParameters newsParameters);
        IEnumerable<News> GetLastImportantNewses();
        IEnumerable<NewsWithCategoryDto> GetLastMostViewNewses();
        IEnumerable<News> GetLastMostCommentNewses();
        News GetNews(Guid id);
        News GetNewsWithDependents(Guid id);
        void CreateNews(NewsCreationDto news);
        void UpdateNews(Guid id, NewsUpdateDto news);
        void DeleteNews(Guid id);
        //void UploadImage(IFormFile newsImage);
    }
}
