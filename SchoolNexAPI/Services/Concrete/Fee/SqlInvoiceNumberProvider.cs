using SchoolNexAPI.Data;
using SchoolNexAPI.Services.Abstract.Fee;
using System.Data;

namespace SchoolNexAPI.Services.Concrete.Fee
{
    public class SqlInvoiceNumberProvider : IInvoiceNumberProvider
    {
        private readonly AppDbContext _db;

        public SqlInvoiceNumberProvider(AppDbContext db)
        {
            _db = db;
        }

        //public async Task<long> GetNextAsync(CancellationToken ct = default)
        //{
        //    // SQL Server: NEXT VALUE FOR dbo.InvoiceNumbers
        //    var conn = _db.Database.con.GetDbConnection();
        //    if (conn.State != ConnectionState.Open) await conn.OpenAsync(ct);

        //    using var cmd = conn.CreateCommand();
        //    cmd.CommandText = "SELECT NEXT VALUE FOR dbo.InvoiceNumbers;";
        //    // Ensure we execute under the same transaction if one exists
        //    var currentTx = _db.Database.CurrentTransaction?.GetDbTransaction();
        //    if (currentTx != null) cmd.Transaction = currentTx;

        //    var result = await cmd.ExecuteScalarAsync(ct);
        //    return Convert.ToInt64(result);
        //}

        //public async Task<string> GetNextFormattedAsync(string prefix = null, CancellationToken ct = default)
        //{
        //    var next = await GetNextAsync(ct);
        //    var prefixPart = string.IsNullOrWhiteSpace(prefix) ? DateTime.UtcNow.Year.ToString() : prefix;
        //    return $"{prefixPart}-{next:D6}";
        //}
    }
}
