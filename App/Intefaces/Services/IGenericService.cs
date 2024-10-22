using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Intefaces.Services
{
    public interface IGenericService<TEntity, TViewModel>
        where TEntity : class
        where TViewModel : class
    {
        Task<IEnumerable<TViewModel>> GetAllAsync();
        Task<TViewModel> GetByIdAsync(int id);
        Task AddAsync(TViewModel viewModel);
        void Update(TViewModel viewModel);
        void Delete(int id);
    }

}
