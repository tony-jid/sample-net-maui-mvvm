using SampleMauiMvvmApp.Models;
using SQLite;

namespace SampleMauiMvvmApp.Services
{
    public class InvoiceService : BaseService
    {
        public InvoiceService(DbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<List<Invoice>> GetInvoicesBySupplierId(int supplierId)
        {
            try
            {
                return await dbContext.Database.Table<Invoice>().Where(x => x.SupplierId == supplierId).ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return null;
        }

        public async Task<int> CountInvoicesBySupplierId(int supplierId)
        {
            try
            {
                return await dbContext.Database.Table<Invoice>().Where(x => x.SupplierId == supplierId).CountAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return 0;
        }

        public async Task<Invoice> InsertInvoice(Invoice invoice)
        {
            try
            {
                await dbContext.Database.InsertAsync(invoice);

                return invoice;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return null;
        }

        public async Task<Invoice> UpdateInvoice(Invoice invoice)
        {
            try
            {
                await dbContext.Database.UpdateAsync(invoice);

                return invoice;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return null;
        }

        public async Task<Invoice> DeleteInvoice(Invoice invoice)
        {
            try
            {
                await dbContext.Database.DeleteAsync(invoice);

                return invoice;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return null;
        }
    }
}
