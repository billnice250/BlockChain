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




    // GET: api/BlockChain/CurrentChain
    [HttpGet("CurrentChain")]
    public async Task<ActionResult<IEnumerable<Block>>> GetCurrentChain()
    {
        _logger.LogInformation("GetCurrentChain");
        return await Task.FromResult(BlockChainService.GetChain());
    }

    // GET: api/block/5
    [HttpGet("block/{id}")]
    public async Task<ActionResult<Block>> GetBlock(int id)
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
    // POST: api/CreateBlock
    [HttpPost("CreateBlock")]
    public async Task<ActionResult<Block>> CreateBlock(RequestData requestData)
    {

        _logger.LogInformation("CreateBlock");
        _logger.LogInformation("Data:", requestData.Data);


        try
        {

            var block = BlockChainService.AddBlock(requestData.Data, requestData.Nonce, requestData.PreviousHash, requestData.Hash);

            if (block == null)
            {
                _logger.LogInformation("Block is not created");
                return Unauthorized("Block with hash " + requestData.Hash + " is not accepted");
            }
            return await Task.FromResult(block);

        }
        catch (Exception ex)
        {

            _logger.LogInformation("Block is not created");

            _logger.LogInformation(ex.Message);

            return StatusCode(500, ex.Message);

        }




    }

    // PUT: api/GetLastBlock
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpGet("LatestBlock")]
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
        public int Nonce { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }

    }



}
