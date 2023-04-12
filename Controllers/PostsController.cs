using Microsoft.AspNetCore.Mvc;
using Nethereum.Web3;
using Nethereum.Contracts;
using Nethereum.Contracts.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using donatETH.Models;
using donatETH.Services;

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

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Text,ImageUrl,Creator,Price,Goal")] Post post)
        {

            if (ModelState.IsValid)
            {
                await PostContractService.AddPostAsync(post);
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

    }
}

