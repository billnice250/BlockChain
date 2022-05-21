
namespace it.billnice.BlockChain;
public static class BlockChainService
{
    private static readonly List<Block> chain = new List<Block>();

    public static List<Block> GetChain()
    {
        return chain;
    }
    static BlockChainService() => Initialize();

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
                    newBlock.PreviousHash = GetLastBlock().Hash
                ?? "Error";

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

    public static Block? GetBlock(long index)
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
            return GetChain()[(int)index];
        }
        return null;
    }

    public static Block? GetLastBlock()
    {
        return GetChain().LastOrDefault();
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