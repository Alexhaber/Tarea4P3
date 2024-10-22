using App.Dtos.Profile;
using App.Helpers;
using App.Intefaces.Repositories;
using App.Intefaces.Services;
using App.ViewModels.Post;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service
{
    public class PostService : IPostService
    {
        private readonly IGenericRepository<Post> _postRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse _user;

        public PostService(IGenericRepository<Post> postRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<IEnumerable<PostViewModel>> GetAllAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            var userpost = posts.Where(p => p.User == _user.UserName).ToList();

            return posts.Select(p => new PostViewModel
            {
                User = p.User,
                Description = p.Description,
                CreatedAt = p.CreatedAt
            }).ToList();
        }

        public async Task<PostViewModel> GetByIdAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            return _mapper.Map<PostViewModel>(post);
        }

        public async Task AddAsync(PostViewModel viewModel)
        {
            var post = _mapper.Map<Post>(viewModel);
            await _postRepository.AddAsync(post);
        }

        public void Update(PostViewModel viewModel)
        {
            var post = _mapper.Map<Post>(viewModel);
            _postRepository.Update(post);
        }

        public void Delete(int id)
        {
            var post = _postRepository.GetByIdAsync(id).Result;
            if (post != null)
            {
                _postRepository.Delete(post);
            }
        }
    }
}
