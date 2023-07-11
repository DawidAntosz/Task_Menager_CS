using Microsoft.AspNetCore.Mvc;
using TaskMenagerApp.Models;
using TaskMenagerApp.Repositories;

namespace TaskMenagerApp.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //======================================= TODO:

        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        //=======================================

        // GET: UserController/MyAccount/5
        public ActionResult MyAccount(Guid UserId)
        {
            var user = _userRepository.Get(UserId);
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: UserController/Register
        public ActionResult Register()
        {
            return View(new MyUser());
        }

        // POST: UserController/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(MyUser newUser)
        {
            if (ModelState.IsValid)
            {
                _userRepository.Register(newUser);
                return RedirectToAction(nameof(Index));
            }
            return View(newUser);
        }

        // GET: UserController/EditAccount/5
        public ActionResult EditAccount(Guid UserId)
        {
            var user = _userRepository.Get(UserId);
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: UserController/EditAccount/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccount(MyUser editUser)
        {
            if (ModelState.IsValid)
            {
                _userRepository.EditAccount(editUser);
                return RedirectToAction(nameof(Index));
            }
            return View(editUser);
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(Guid UserId)
        {
            var DelateUser = _userRepository.Get(UserId);
            if (DelateUser != null)
            {
                return View(DelateUser);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(MyUser user)
        {
            _userRepository.Delete(user.UserId);
            return RedirectToAction(nameof(Index));
        }
    }
}
