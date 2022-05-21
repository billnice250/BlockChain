using Microsoft.AspNetCore.Mvc;

namespace it.billnice.BlockChain.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BlockChainController : ControllerBase
{

    ILogger<BlockChainController> _logger;

    public BlockChainController(ILogger<BlockChainController> logger)
    {
        _logger = logger;
    }




    // GET: api/CurrentChain
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Block>>> GetCurrentChain()
    {
        _logger.LogInformation("GetCurrentChain");
        return await Task.FromResult(BlockChainService.GetChain());
    }

    // GET: api/block/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Block>> GetBlock(long id)
    {

        _logger.LogInformation("GetBlock id:", id);
        var block = BlockChainService.GetBlock(id);

        if (block == null)
        {
            _logger.LogError("Block is not found");
            return NotFound();
        }
        return await Task.FromResult(block);

    }
    // POST: api/TodoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Block>> CreateBlock(RequestData requestData)
    {

        _logger.LogInformation("CreateBlock");
        _logger.LogInformation("Data:", requestData.Data);
        var block = BlockChainService.AddBlock(requestData.Data ?? "", requestData.Proof, requestData.Nonce);

        if (block == null)
        {
            _logger.LogInformation("Block is null");
            return Unauthorized();
        }
        return await Task.FromResult(block);
    }

    // PUT: api/GetLastBlock
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("GetLastBlock")]
    public async Task<ActionResult<Block>> GetLastBlock()
    {
        _logger.LogInformation("GetLastBlock");
        var block = BlockChainService.GetLastBlock();
        if (block == null)
        {
            _logger.LogError("Chain is null");
            return NotFound();
        }
        return await Task.FromResult(block);
    }


    public class RequestData
    {
        public string Data { get; set; }
        public int Proof { get; set; }
        public int Nonce { get; set; }
    }



}
