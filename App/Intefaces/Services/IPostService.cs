using App.ViewModels.Post;
using Domain.Entities;

namespace App.Intefaces.Services
{
    public interface IPostService : IGenericService<Post, PostViewModel>
    {

    }
}