using SampleMauiMvvmApp.Fakers;
using SampleMauiMvvmApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMauiMvvmApp.Services
{
    public class BaseService
    {
        protected readonly DbContext dbContext;

        public string StatusMessage;

        public BaseService(DbContext dbContext)
        {
            this.dbContext = dbContext;
            _ = Init(this.dbContext);
        }

        public async Task Init(DbContext dbContext)
        {
            if (dbContext.Database is not null)
                return;

            dbContext.Database = new SQLiteAsyncConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);

            var migrationResult = await dbContext.Database.CreateTablesAsync(CreateFlags.None
                , typeof(Invoice)
                , typeof(Supplier));

            if (migrationResult.Results != null && migrationResult.Results.Count > 0)
            {
                bool isNewDatabase = migrationResult.Results.Any(x => x.Value.ToString().ToUpper() == "CREATED");
                if (isNewDatabase)
                {
                    await SeedData(dbContext);
                }
            }
        }

        async Task SeedData(DbContext dbContext)
        {
            var supplierFaker = new SupplierFaker();
            var suppliers = supplierFaker.Generate(5);
            var invoiceFaker = new InvoiceFaker();

            for (int i = 0; i < suppliers.Count; i++)
            {
                await dbContext.Database.InsertAsync(suppliers[i]);

                var invoices = invoiceFaker.Generate(i + 1);
                foreach (var inv in invoices)
                {
                    inv.SupplierId = suppliers[i].Id;
                    await dbContext.Database.InsertAsync(inv);
                }
            }
        }
    }
}
