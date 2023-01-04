
using it.billnice.BlockChain.Models;

namespace it.billnice.BlockChain;
public static class BlockChainService
{
    private static readonly List<Block> chain = new List<Block>();

    public static List<Block> GetChain()
    {
        return chain;
    }
    static BlockChainService() => Initialize();

    /// <summary>
    ///  for adding a new block to the block chain.
    /// </summary>
    /// <param name="data"> the data of the new block</param>
    /// <param name="nonce"> the once choosen for the new block</param>
    /// <param name="previousHash"> the prev block hash</param>
    /// <param name="hash"> the hash of the new block </param>
    /// <returns> null if not accepted or the created block if accepted</returns>
    /// <exception cref="Exception"> when the previous block couldn't be retreived</exception>

    public static Block? AddBlock(string data, int nonce, string previousHash, string hash)
    {
        var newBlock = new Block();
        newBlock.Data = data;
        newBlock.Nonce = nonce;
        newBlock.PreviousHash = previousHash;
        newBlock.Hash = hash;

        // for showcase only
        // only in test mode
        if (Constants.RUNTIME_prod && Utils.ValidateProofOfWork(newBlock) != CheckProofResult.Valid)
        {
            return null;
        }{
            newBlock.PreviousHash = GetLastBlock()!.Hash?? throw new Exception ("Last Block Hash couldn't be retreived.");
            newBlock.Hash = newBlock.ComputeHash();

        }

        newBlock.Index = chain.Count + 1;
        newBlock.TimeStamp = DateTime.Now;
        chain.Add(newBlock);
        return newBlock;
    }



    public static void Initialize()
    {
        if (chain.Count == 0)
        {

            Block genesisBlock = new Block();
            genesisBlock.Data = "Genesis Block";
            genesisBlock.Hash = "0";
            genesisBlock.PreviousHash = "0";
            genesisBlock.Nonce = 0;
            genesisBlock.Index = 0;
            genesisBlock.TimeStamp = DateTime.Now;
            genesisBlock.Hash = genesisBlock.ComputeHash();

            genesisBlock.Hash = "0000" + genesisBlock.Hash.Substring(4);
            chain.Add(genesisBlock);

        }

    }

    public static Block? GetBlock(int index)
    {
        if (GetChain() == null)
        {
            // _logger.LogError("Chain is null");
            return null;
        }
        if (index < 0 || index > GetChain().Count)
        {
            // _logger.LogError("Index is out of range");
            return null;
        }

        if (index >= 0 && index < GetChain().Count)
        {
            return GetChain()[index];
        }
        return null;
    }

    public static Block? GetLastBlock()
    {
        return GetChain().Last()??null;
    }

    public static int GetChainLength()
    {
        if (GetChain() == null)
        {
            // _logger.LogError("Chain is null");
            return 0;
        }
        return GetChain().Count;
    }

}