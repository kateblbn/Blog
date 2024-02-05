
namespace Blog.web.Models
{
    internal class ViewModels
    {
        internal class User : ViewModel.User
        {
            public Guid Id { get; set; }
            public string Username { get; set; }
            public string EmailAddress { get; set; }
        }
    }
}