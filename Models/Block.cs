

public class Block
{
    
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

