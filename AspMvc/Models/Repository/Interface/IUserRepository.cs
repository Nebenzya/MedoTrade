namespace AspMvc.Models.Repository.Interface
{
    internal interface IUserRepository
    {
        void Create(User user);
        User GetById(int id);
    }
}
