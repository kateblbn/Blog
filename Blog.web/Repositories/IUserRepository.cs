using Microsoft.AspNetCore.Identity;

namespace Blog.web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
