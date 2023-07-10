using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using System.Linq.Expressions;

namespace NLayer.Service.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _repo;
        private readonly IUnitOfWork _unitOfWork;

        public Service(IGenericRepository<T> repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _repo.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;

        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _repo.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repo.AnyAsync(expression);

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repo.GetAll().ToListAsync();

        }

        public async Task<T> GetByIdAsync(int id)
        {

            var hasId = await _repo.GetByIdAsync(id);
            if (hasId == null)
            {

                throw new NotFoundExcepiton($"{typeof(T).Name} not found");
            }
            return hasId;

        }

        public async Task RemoveAsync(T entity)
        {
            _repo.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _repo.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _repo.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repo.Where(expression);
        }
    }
}
