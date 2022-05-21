using System.Security.Cryptography;
using System.Text;

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



    public string ComputeHash()
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(this.Data + this.PreviousHash + this.Nonce));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }


}

