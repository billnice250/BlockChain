using System.Security.Cryptography;
using System.Text;

public class Block
{
    private string? previousHash;
    private string? data;
    private DateTime timeStamp;
    private int proof;
    private int nonce;
    private long index;
    private string? hash;

    public string? PreviousHash { get => previousHash; set => previousHash = value; }
    public string? Data { get => data; set => data = value; }
    public DateTime TimeStamp { get => timeStamp; set => timeStamp = value; }
    public int Proof { get => proof; set => proof = value; }
    public int Nonce { get => nonce; set => nonce = value; }
    public long Index { get => index; set => index = value; }
    public string? Hash { get => hash; set => hash = value; }

    public Block(string data, int proof ,int nonce)
    {
        this.Data = data;
        this.Proof = proof;
        this.Nonce = nonce;
    }

    public Block (){
        this.Data = "Genesis Block";
        this.PreviousHash = "0";
        this.Proof = 1;
        this.Nonce = 0;
        this.Index = 0;
        this.TimeStamp = DateTime.Now;
        this.Hash = this.ComputeHash();
    }

    public string ComputeHash()
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(this.Data + this.TimeStamp + this.PreviousHash + this.Proof + this.Nonce));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }


}

