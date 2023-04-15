using donatETH.Models;
using Nethereum.Web3;
using Nethereum.Contracts;
using System.Numerics;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Coinbase.Commerce;
using System.Text;
using System.Text.RegularExpressions;

namespace donatETH.Services
{
    public static class PostContractService
    {
        private static string endPoint = "https://rpc.sepolia.org/";
        private static string contractAddress = "0x1C35bFD8432d8cd212f435AB8D48f1344143DA78";
		private static string contractAbi = @"[
	{
		""inputs"": [
			{
				""internalType"": ""uint256"",
				""name"": ""postId"",
				""type"": ""uint256""
			}
		],
		""name"": ""addLike"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""string"",
				""name"": ""title_"",
				""type"": ""string""
			},
			{
	""internalType"": ""string"",
				""name"": ""text_"",
				""type"": ""string""
			},
			{
	""internalType"": ""string"",
				""name"": ""imageUrl"",
				""type"": ""string""
			},
			{
	""internalType"": ""address"",
				""name"": ""creator_"",
				""type"": ""address""
			},
			{
	""internalType"": ""uint256"",
				""name"": ""price_"",
				""type"": ""uint256""
			},
			{
	""internalType"": ""uint256"",
				""name"": ""goal_"",
				""type"": ""uint256""
			}
		],
		""name"": ""addPost"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
	""inputs"": [
			{
		""internalType"": ""uint256"",
				""name"": ""postId"",
				""type"": ""uint256""
			}
		],
		""name"": ""deletePost"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
	""inputs"": [
			{
		""internalType"": ""uint256"",
				""name"": ""postId"",
				""type"": ""uint256""
			}
		],
		""name"": ""donate"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
	""inputs"": [
			{
		""internalType"": ""uint256"",
				""name"": ""postId"",
				""type"": ""uint256""
			}
		],
		""name"": ""removeLike"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
	""inputs"": [
			{
		""internalType"": ""uint256"",
				""name"": ""postId"",
				""type"": ""uint256""
			}
		],
		""name"": ""getPost"",
		""outputs"": [
			{
		""internalType"": ""bytes32"",
				""name"": """",
				""type"": ""bytes32""
			},
			{
		""internalType"": ""string"",
				""name"": """",
				""type"": ""string""
			},
			{
		""internalType"": ""string"",
				""name"": """",
				""type"": ""string""
			},
			{
		""internalType"": ""uint256"",
				""name"": """",
				""type"": ""uint256""
			},
			{
		""internalType"": ""string"",
				""name"": """",
				""type"": ""string""
			},
			{
		""internalType"": ""uint256"",
				""name"": """",
				""type"": ""uint256""
			},
			{
		""internalType"": ""uint256"",
				""name"": """",
				""type"": ""uint256""
			},
			{
		""internalType"": ""uint256"",
				""name"": """",
				""type"": ""uint256""
			},
			{
		""internalType"": ""address"",
				""name"": """",
				""type"": ""address""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
	""inputs"": [],
		""name"": ""getPostsLength"",
		""outputs"": [
			{
		""internalType"": ""uint256"",
				""name"": """",
				""type"": ""uint256""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
	""inputs"": [
			{
		""internalType"": ""uint256"",
				""name"": """",
				""type"": ""uint256""
			}
		],
		""name"": ""Posts"",
		""outputs"": [
			{
		""internalType"": ""bytes32"",
				""name"": ""id"",
				""type"": ""bytes32""
			},
			{
		""internalType"": ""string"",
				""name"": ""title"",
				""type"": ""string""
			},
			{
		""internalType"": ""string"",
				""name"": ""text"",
				""type"": ""string""
			},
			{
		""internalType"": ""address"",
				""name"": ""creator"",
				""type"": ""address""
			},
			{
		""internalType"": ""uint256"",
				""name"": ""likes"",
				""type"": ""uint256""
			},
			{
		""internalType"": ""string"",
				""name"": ""imageurl"",
				""type"": ""string""
			},
			{
		""internalType"": ""uint256"",
				""name"": ""currentsum"",
				""type"": ""uint256""
			},
			{
		""internalType"": ""uint256"",
				""name"": ""price"",
				""type"": ""uint256""
			},
			{
		""internalType"": ""uint256"",
				""name"": ""goal"",
				""type"": ""uint256""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	}
]";

		private static string privateKey = "e652bdb841aca1d2d64c957b0a5f1ab27d0a7db8a28c4ff226064e31779c901c";

        private static readonly Account account = new Account(privateKey);
        private static readonly Web3 web = new Web3(account, endPoint);
        private static Contract contract = web.Eth.GetContract(contractAbi, contractAddress);


        public static async Task AddPostAsync(Post newpost)
        {
			PostInDTO postInDTO = new PostInDTO() { 
				Title = newpost.Title,
				Text = newpost.Text,
				ImageUrl = newpost.ImageUrl,
				Creator = newpost.Creator,
				Price = newpost.Price,
				Goal=newpost.Goal
			};
			var addPostFunction = contract.GetFunction("addPost");

			var transactionInput = addPostFunction.CreateTransactionInput(
				account.Address,
				new HexBigInteger(500000),
				null,
				postInDTO.Title,
				postInDTO.Text,
				postInDTO.ImageUrl,
				postInDTO.Creator,
				postInDTO.Price,
				postInDTO.Goal);

			await web.Eth.TransactionManager.SendTransactionAsync(transactionInput);
        }

        public static async Task<Post> GetPostAsync(int i)
        {
            var getPostFunction = contract.GetFunction("getPost");
            BigInteger index = i;
            var result = await getPostFunction.CallDeserializingToObjectAsync<PostOutDTO>(index);
            string id = "";
			for(int j = 0; j < result.Id.Length; j++)
            {
				id+=result.Id[j];
            }
            Post res = new Post()
			{
				Id = id,
                Title = result.Title,
                Text = result.Text,
                ImageUrl = result.ImageUrl,
                Creator = result.Creator,
                Price = (int)result.Price,
                Goal = (int)result.Goal,
                CurrentSum = (int)result.CurrentSum,
                LikeCount = (int)result.LikeCount,
				Index = i
            };

            return res;
        }

        public static async Task<int> GetPostsLengthAsync()
        {
            var getPostsLengthFunction = contract.GetFunction("getPostsLength");
            var result = await getPostsLengthFunction.CallAsync<int>();

            return result;
        }

        public static async Task<string> LikePost(int Id)
        {
			var addLikeFunction = contract.GetFunction("addLike");
			BigInteger index = Id;
			var gas = await addLikeFunction.EstimateGasAsync(index);
			//var gas = new HexBigInteger(2000000000);

			var transactionInput = addLikeFunction.CreateTransactionInput(account.Address, gas, null, index);
			var result = await web.Eth.TransactionManager.SendTransactionAsync(transactionInput);
			return result;
		}

		public static async Task<string> RemoveLike(int Id)
		{
			var removeLikeFunction = contract.GetFunction("removeLike");
			BigInteger index = Id;
			var gas = await removeLikeFunction.EstimateGasAsync(index);
			//var gas = new HexBigInteger(2000000000);

			var transactionInput = removeLikeFunction.CreateTransactionInput(account.Address, gas, null, index);
			var result = await web.Eth.TransactionManager.SendTransactionAsync(transactionInput);
			return result;
		}

		public static async Task DonateById(int id)
		{
			var donateFunction = contract.GetFunction("donate");
			BigInteger index = id;

			var gas = await donateFunction.EstimateGasAsync(index);

			var transactionInput = donateFunction.CreateTransactionInput(account.Address, gas, null, index);
			var result = await web.Eth.TransactionManager.SendTransactionAsync(transactionInput);
		}




	}
}
