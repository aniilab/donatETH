using Nethereum.Web3;
using Nethereum.Contracts;
using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace donatETH.Models
{
    [FunctionOutput]
    public class PostOutDTO: IFunctionOutputDTO
    {
        [Parameter("bytes32", "id", 1)]
        public byte[] Id { get; set; }

        [Parameter("string", "title", 2)]
        public string Title { get; set; }

        [Parameter("string", "text", 3)]
        public string Text { get; set; }

        [Parameter("uint256", "likes", 4)]
        public BigInteger LikeCount { get; set; }

        [Parameter("string", "imageurl", 5)]
        public string ImageUrl { get; set; }

        [Parameter("uint256", "currentsum", 6)]
        public BigInteger CurrentSum { get; set; }

        [Parameter("uint256", "price", 7)]
        public BigInteger Price { get; set; }

        [Parameter("uint256", "goal", 8)]
        public BigInteger Goal { get; set; }

        [Parameter("address", "creator", 9)]
        public string Creator { get; set; }
    }
}
