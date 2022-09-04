using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NewsProject.Core.Domain.Dtos.News;
using NewsProject.Core.Domain.Entities;
using NewsProject.Core.Domain.Entities.RequestParameters;
using NewsProject.Core.Domain.Services;
using NewsProject.Framework.Extensions;
using NewsProject.Infa.Data.Sql.Context;

namespace NewsProject.Infa.Data.Sql.Services
{
    public class NewsService : INewsService
    {
        private readonly NewsDbContext _context;

        public NewsService(NewsDbContext context)
        {
            _context = context;
        }

        public void CreateNews(NewsCreationDto news)
        {
            

            var category = _context.Categories.Find(news.CategoryId);
            if (category == null)
                throw new Exception("Category Not Found...!");

            News result = new News
            {
                Category = category,
                NewsId = Guid.NewGuid(),
                Author = news.Author,
                Title = news.Title,
                ShortDescription = news.ShortDescription,
                Content = news.Content,
                Image = news.Image,
                IsImportant = news.IsImportant,
                CreateTime = DateTime.Now.ToShamsiDateTime(),
                IsDeleted = false,
            };

            _context.Newses.Add(result);
            _context.SaveChanges();

        }

        public void DeleteNews(Guid id)
        {
            var news = _context.Newses.FirstOrDefault(c => c.NewsId == id);
            news.IsDeleted = true;
            _context.Newses.Update(news);
            _context.SaveChanges();
        }

        public PagedList<NewsWithCategoryDto> GetAllNewses(NewsParameters newsParameters)
        {
            var newses = _context.Newses.Include(c=>c.Category).Select(c => new NewsWithCategoryDto
            {
                NewsId = c.NewsId,
                Title = c.Title,
                ShortDescription = c.ShortDescription,
                Image = c.Image,
                CategoryId = c.Category.CategoryId,
                IsDeleted = c.IsDeleted,
                CreateTime = c.CreateTime,
            }).Where(c => c.IsDeleted == false).Search(newsParameters.SearchTerm).Sort(newsParameters.OrderBy);
            return PagedList<NewsWithCategoryDto>.ToPagedList(newses, newsParameters.PageNumber, newsParameters.PageSize);

        }

        public IEnumerable<NewsWithCategoryDto> GetLast8Newses()
        {
            var newses = _context.Newses.Include(c => c.Category).Take(8).Select(c => new NewsWithCategoryDto
            {
                NewsId = c.NewsId,
                Title = c.Title,
                CreateTime = c.CreateTime,
                Image = c.Image,
                View = c.View,
                CategoryId = c.Category.CategoryId,
                CategoryTitle = c.Category.Title,

            }).OrderByDescending(c => c.CreateTime);
            return newses;
        }

        public PagedList<NewsWithCategoryDto> GetLatestNewses(LatestNewsParameters newsParameters)
        {
            var newses = _context.Newses.Include(c => c.Category).Select(c => new NewsWithCategoryDto
            {
                NewsId = c.NewsId,
                Title = c.Title,
                ShortDescription = c.ShortDescription,
                Image = c.Image,
                CategoryId = c.Category.CategoryId,
                CategoryTitle = c.Category.Title,
                IsDeleted = c.IsDeleted,
                CreateTime = c.CreateTime,
            }).Where(c => c.IsDeleted == false).Search(newsParameters.SearchTerm).OrderByDescending(c=>c.CreateTime);
            newsParameters.PageSize = 8;
            return PagedList<NewsWithCategoryDto>.ToPagedList(newses, newsParameters.PageNumber, newsParameters.PageSize);
        }

        public IEnumerable<News> GetLastImportantNewses()
        {
            var news = _context.Newses.Where(c => c.IsImportant).Select(c => new News
            {
                Title = c.Title,
                CreateTime = c.CreateTime,
                ShortDescription = c.ShortDescription,
                Image = c.Image
            }).Take(2);
            return news;
        }

        public News GetNews(Guid id)
        {
            var news = _context.Newses
                .SingleOrDefault(c => c.NewsId == id);
            //News result = new News()
            //{
            //    NewsId = newses.NewsId,
            //    Author = newses.Author,
            //    Title = newses.Title,
            //    ShortDescription = newses.ShortDescription,
            //    Content = newses.Content,
            //    IsImportant = newses.IsImportant,
            //    CreateTime = newses.CreateTime,
            //    UpdateTime = newses.UpdateTime,
            //    IsDeleted = newses.IsDeleted
            //};
            return news;
        }

        public News GetNewsWithDependents(Guid id)
        {
            var newses = _context.Newses
                .Include(c => c.Tags)
                .Include(c=>c.Category)
                .Include(c => c.Comments.Where(o => o.CommentStatus == CommentStatus.Approved))
                .SingleOrDefault(c => c.NewsId == id);

            newses.View += 1;
            _context.Newses.Update(newses);
            _context.SaveChanges();

            return newses;
        }

        public void UpdateNews(Guid id, NewsUpdateDto news)
        {
            var category = _context.Categories.Find(news.CategoryId);
            if (category == null)
                throw new Exception("Category Not Found...!");

            var findNews = _context.Newses.FirstOrDefault(c => c.NewsId == id);
            findNews.Title = news.Title;
            findNews.ShortDescription = news.ShortDescription;
            findNews.UpdateTime = DateTime.Now.ToShamsiDateTime();
            findNews.Content = news.Content;
            findNews.Author = news.Author;
            findNews.IsImportant = news.IsImportant;
            findNews.Image = news.Image;
            findNews.Category = category;
            //if (newsImage != null)
            //{
            //    var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(newsImage.FileName);

            //    var res = newsImage.AddImageToServer(imageName, PathExtension.NewsImageServer, 450, 250,
            //        PathExtension.NewsThumbnailImageImageServer, findNews.Image);

            //    if (res)
            //    {
            //        findNews.Image = imageName;
            //    }
            //}
            _context.Newses.Update(findNews);
            _context.SaveChanges();
        }

        public IEnumerable<NewsWithCategoryDto> GetLastMostViewNewses()
        {
            var newses = _context.Newses.Include(c => c.Category).Select(c => new NewsWithCategoryDto
            {
                NewsId = c.NewsId,
                Title = c.Title,
                CreateTime = c.CreateTime,
                Image = c.Image,
                View = c.View,
                CategoryId = c.Category.CategoryId,
                CategoryTitle = c.Category.Title,

            }).OrderByDescending(c => c.View).Take(5);
            return newses;
        }

        public IEnumerable<News> GetLastMostCommentNewses()
        {
            var newses = _context.Newses.Include
                (c => c.Comments.Where(o => o.CommentStatus == CommentStatus.Approved))
                .OrderByDescending(c=>c.Comments.Count()).Take(4);
            return newses;
        }

        public PagedList<NewsWithCategoryDto> GetCategoryNewses(Guid categoryId, LatestNewsParameters newsParameters)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            var newses = _context.Newses.Include(c=>c.Category).Where(c=>c.Category.CategoryId==category.CategoryId).Select(c => new NewsWithCategoryDto
            {
                NewsId = c.NewsId,
                Title = c.Title,
                ShortDescription = c.ShortDescription,
                Image = c.Image,
                CategoryId = category.CategoryId,
                CategoryTitle = category.Title,
                IsDeleted = c.IsDeleted,
                CreateTime = c.CreateTime,
            }).Where(c => c.IsDeleted == false).Search(newsParameters.SearchTerm).OrderByDescending(c => c.CreateTime);
            newsParameters.PageSize = 8;
            return PagedList<NewsWithCategoryDto>.ToPagedList(newses, newsParameters.PageNumber, newsParameters.PageSize);
        }

        //public void UploadImage(IFormFile newsImage)
        //{
        //    string fileName = newsImage.FileName;
        //    //string extension = Path.GetExtension(fileName);

        //    var res = newsImage.AddImageToServer(fileName, PathExtension.NewsImageServer, 450, 250, PathExtension.NewsThumbnailImageImageServer);
        //}
    }
}
