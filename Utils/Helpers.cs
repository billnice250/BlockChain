using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using it.billnice.BlockChain.Models;

namespace it.billnice.BlockChain;

static class Utils
{
    public const string pfxFileName = "blockchain.billnice.it.pfx";
    public const string pfxPassword = "password@123";

    #region validate proof of work 
    public static CheckProofResult ValidateProofOfWork(Block newPotentialBlock)
    {
        string hash = newPotentialBlock.ComputeHash();

        if (newPotentialBlock.Hash != hash)
        {
            // _logger.LogError("Hash is invalid");
            return CheckProofResult.Invalid;
        }

        var lastBlock = BlockChainService.GetLastBlock();
        if (lastBlock == null)
        {
            return CheckProofResult.Invalid;
        }

        if (newPotentialBlock.PreviousHash != lastBlock.Hash)
        {
            // _logger.LogError("PreviousHash is not valid");
            return CheckProofResult.Invalid;
        }

        if (newPotentialBlock.Hash.Substring(0, 4) == "0000")
        {
            // _logger.LogInformation("Proof of work failed");
            return CheckProofResult.Valid;
        }
        else
        {
            return CheckProofResult.Invalid;
        }
    }

    #endregion
    public static string ComputeGenesisBlockHash(string input)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public static string ComputeHash(this Block block)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(block.Data + block.PreviousHash + block.Nonce));
            return bytes.BytesToString();
        }
    }


    public static string BytesToString(this byte[] bytes)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }
        return builder.ToString();
    }

    // generate  a self signed a pfx file
    public static X509Certificate2 GenerateSelfSignedCertificate()
    {
        X509Certificate2 cert;
        var keyPair = RSA.Create(2048);
        var req = new CertificateRequest("CN=blockchain.billnice.it",
         keyPair, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        req.CertificateExtensions.Add(new X509SubjectKeyIdentifierExtension(req.PublicKey, false));
        req.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, false, 0, true));
        req.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature|X509KeyUsageFlags.KeyCertSign|X509KeyUsageFlags.KeyAgreement|X509KeyUsageFlags.NonRepudiation, true));

        req.CertificateExtensions.Add(
            new X509EnhancedKeyUsageExtension(
                new OidCollection { 
                    new Oid("1.3.6.1.5.5.7.3.1")
                     }, true));

        cert = req.CreateSelfSigned(DateTimeOffset.Now, DateTimeOffset.Now.AddYears(2));
        cert.Export(X509ContentType.Pfx, pfxPassword);

        // save the pfx file
        File.WriteAllBytes(pfxFileName, cert.Export(X509ContentType.Pfx, pfxPassword));
        return cert;
    }
   
}

public enum CheckProofResult
{
    Valid,
    Invalid,
    NotEnoughBlocks
}