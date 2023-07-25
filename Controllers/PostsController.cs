using Microsoft.AspNetCore.Mvc;
using Nethereum.Web3;
using Nethereum.Contracts;
using Nethereum.Contracts.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using donatETH.Models;
using donatETH.Services;
using System.ComponentModel.DataAnnotations;

namespace donatETH.Controllers
{
    public class PostsController : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var posts = new List<Post>();
            int count = (int)await PostContractService.GetPostsLengthAsync();
            for (int i = 0; i < count; i++)
            {
                posts.Add(await PostContractService.GetPostAsync(i));
            }
            return View(posts);
            
        }
        public async Task<IActionResult> Details(int id)
        {   
            var post = await PostContractService.GetPostAsync(id);
            return View(post);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Loading()
        {
            return RedirectToAction("Loading", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {

                var task = PostContractService.AddPostAsync(post);

                while (!task.IsCompleted)
                {

                }

                return RedirectToAction(nameof(Index));
            }

            return View(post);
        }

        public async Task<IActionResult> Like(int index, string liked)
        {
            int isliked = Convert.ToInt32(liked);
            string receipt;
            if (isliked == 0)
            {
                receipt = await PostContractService.RemoveLike(index);
            }
            else if (isliked == 1)
            {
                receipt = await PostContractService.LikePost(index);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Donate(int index)
        {
            await PostContractService.DonateById(index);
            return RedirectToAction(nameof(Details));
        }

    }
}

