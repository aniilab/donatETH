using Microsoft.AspNetCore.Mvc;
using donatETH.Models;
using donatETH.Services;

namespace donatETH.Controllers
{
    public class UserController : Controller
    {
        public async Task<IActionResult> GetUserInfo(int index)
        {
            ViewBag.PostIndex = index;
            Post post = await PostContractService.GetPostAsync(index);
            ViewBag.PostTitle = post.Title;
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetUserInfoConfirm([Bind("PostId,Address")] UserInfo info)
        {

            if (ModelState.IsValid)
            {
                await PostContractService.DonateById(info);
                return RedirectToAction("Index","Posts");
            }

            return View();
        }
    }
}
