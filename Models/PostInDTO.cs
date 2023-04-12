//using Nethereum.Web3;
using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

namespace donatETH.Models
{
    [Function("addPost")]
    public class PostInDTO : FunctionMessage
    {
        [Parameter("string", "title_", 1)]
        public string Title { get; set; }

        [Parameter("string", "text_", 2)]
        public string Text { get; set; }

        [Parameter("string", "imageUrl", 3)]
        public string ImageUrl { get; set; }

        [Parameter("address", "creator_", 4)]
        public string Creator { get; set; }

        [Parameter("uint256", "price_", 5)]
        public BigInteger Price { get; set; }

        [Parameter("uint256", "goal_", 6)]
        public BigInteger Goal { get; set; }
    }
}
