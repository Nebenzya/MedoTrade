using AspMvc.Models.Repository;
using AspMvc.Models.Repository.Interface;
using System.Web.Mvc;

namespace AspMvc.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController()
        {
            //нужен DI
            _userRepository = new UserRepository();
        }

        public ActionResult NewUser()
        {
            return View();
        }


        [HttpPost]
        public ContentResult AddUser(string name, string phone)
        {
            _userRepository.Create(new Models.User { Name = name, Phone = phone });
            return Content("success");
        }
    }
}