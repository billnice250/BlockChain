
namespace it.billnice.BlockChain.Models;
public class Block
{
    
    // data can be a complex structure to house a group of transactions
    public string Data { get; set; }
    public string Hash { get; set; } 
    public string PreviousHash { get; set; }
    public int Nonce { get; set; }
    public int Index { get; set; }
    public DateTime TimeStamp { get; set; }
    public Block(){
        this.Data = "";
        this.Hash = "";
        this.PreviousHash = "";
        this.Nonce = 0;
    }






}

