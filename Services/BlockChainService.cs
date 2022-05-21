
namespace it.billnice.BlockChain;
public static class BlockChainService
{
    private static readonly List<Block> chain = new List<Block>();

    public static List<Block> GetChain()
    {
        return chain.AsEnumerable().ToList();
    }

    public static Block? AddBlock(string data ,int proof ,int nonce)
    {

        if (GetChain() == null)
        {
        //   _logger.LogError("Chain is null");
            return null;
        }
        Block block = new Block(data,proof,nonce);
        block.PreviousHash = GetChain().Last().Hash!;
        block.Index = GetChain().Count + 1;
        block.TimeStamp = DateTime.Now;
        block.Hash = block.ComputeHash();


        chain.Add(block);
        return block;
    }

     static BlockChainService(){
            Block genesisBlock = new Block();
            chain.Add(genesisBlock);

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

        if(index>=0 && index<GetChain().Count)
        {
            return GetChain()[(int)index];
        }
        return null;
    }

    public static Block? GetLastBlock()
    {
        if (GetChain() == null)
        {
            // _logger.LogError("Chain is null");
            return null;
        }
        return GetChain().Last();
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