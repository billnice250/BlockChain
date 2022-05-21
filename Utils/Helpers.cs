using System.Security.Cryptography;
using System.Text;

namespace it.billnice.BlockChain;

class Utils{
    // public static string ComputeGenesisBlockHash(string input)
    // {
    //     using (SHA256 sha256Hash = SHA256.Create())
    //     {
    //         byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

    //         StringBuilder builder = new StringBuilder();
    //         for (int i = 0; i < bytes.Length; i++)
    //         {
    //             builder.Append(bytes[i].ToString("x2"));
    //         }
    //         return builder.ToString();
    //     }
    // }

    public static CheckProofResult ValidateProofOfWork(Block newPotentialBlock)
    {
        string hash = newPotentialBlock.ComputeHash();

        if(newPotentialBlock.Hash!=hash)
        {
            // _logger.LogError("Hash is invalid");
            return CheckProofResult.Invalid;
        }
 
        var lastBlock = BlockChainService.GetLastBlock();
        if (lastBlock == null)
        {
            return CheckProofResult.Invalid;
        }

        if(newPotentialBlock.PreviousHash != lastBlock.Hash )
        {
            // _logger.LogError("PreviousHash is not valid");
            return CheckProofResult.Invalid;
        }

        if (newPotentialBlock.Hash.Substring(0, 4) == "0000")
        {
            // _logger.LogInformation("Proof of work failed");
            return CheckProofResult.Valid;
        }
        else
        {
            return CheckProofResult.Invalid;
        }
    }


    public static string ComputeGenesisBlockHash(string input)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}

public enum CheckProofResult
{
    Valid,
    Invalid,
    NotEnoughBlocks
}