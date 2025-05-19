using Microsoft.EntityFrameworkCore;
using Shopier.Application.Dtos.CityDtos;
using Shopier.Application.Dtos.TownDtos;
using Shopier.Application.Interfaces;
using Shopier.Domain.Entities;
using Shopier.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class 
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetByCategory(Expression<Func<T, bool>> categoryId)
        {
            return await _context.Set<T>().Where(categoryId).ToListAsync();
        }

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(filter);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }


        public async Task<List<Product>> GetFilterByPrice(decimal min, decimal max)
        {
            return await _context.Products.Where(x => x.Price >= min && x.Price <= max).ToListAsync();
        }

        public async Task<List<Product>> GetProductBysearch(string search)
        {
            return await _context.Products.Where(x => x.ProductName.Contains(search) || x.Description.Contains(search)).ToListAsync();
        }

        public async Task<List<T>> GetTakeAsync(int count)
        {
            return await _context.Set<T>().Take(count).ToListAsync();   // istenilen sayı kadar data dönderme 
        }

      

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTotalAmount(int cartId, decimal amount)
        {
            var value = await _context.Carts.Where(x => x.CartId == cartId).FirstOrDefaultAsync();
            if (value != null)
            {
                value.TotalAmount = amount;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<City>> GetCity()
        {
            var cities = await _context.Cities.ToListAsync();
            return cities;
        }

        public async Task<List<Town>> GetTownByCityId(int cityId)
        {
            var town = await _context.Towns.Where(x => x.CityId == cityId).ToListAsync();
            return town;
        }

      
    }
}
