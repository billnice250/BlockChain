
using it.billnice.BlockChain;
using it.billnice.BlockChain.Models;

var builder = WebApplication.CreateBuilder(args);

try
{
    // check if the certificate exists
    if (!File.Exists(Utils.pfxFileName))
    {
        //create a self signed certificate
        Utils.GenerateSelfSignedCertificate();
        Console.WriteLine("Certificate created.");
    }else
    {
        Console.WriteLine("Certificate already exists.");
    }

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine("Press any key to exit.");
    Console.ReadKey();
    Environment.Exit(1);
}

//configure  kerstrel and add a certificate
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.UseHttps(Utils.pfxFileName, Utils.pfxPassword);
    });
});
// Add db context to the container.
//builder.Services.AddDbContext<BlockChainDbContext>(options => UseFileContextDatabase<EXCELStoreManager>(password: "<password>"));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
