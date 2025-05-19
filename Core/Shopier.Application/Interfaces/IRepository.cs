using Shopier.Application.Dtos.CityDtos;
using Shopier.Application.Dtos.TownDtos;
using Shopier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Application.Interfaces
{

    // kullanacagımız metodları olusturduk.
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();             // bütün kategorileri getirme işlemimiz.
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetTakeAsync(int count);
        Task<List<T>> GetByCategory(Expression<Func<T, bool>> categoryId);
        Task<List<Product>> GetFilterByPrice(decimal min, decimal max);
        Task<List<Product>> GetProductBysearch(string search);
        Task<List<City>> GetCity();
        Task<List<Town>> GetTownByCityId(int cityId);

    }
}

