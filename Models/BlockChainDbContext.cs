using Microsoft.EntityFrameworkCore;

namespace it.billnice.BlockChain.Models;


public class BlockChainDbContext : DbContext
{
    public BlockChainDbContext(DbContextOptions<BlockChainDbContext> options) : base(options)
    {
    }
    

}