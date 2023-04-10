using SampleMauiMvvmApp.Models;
using SQLite;

namespace SampleMauiMvvmApp.Services
{
    // https://www.youtube.com/watch?v=XFP8Np-uRWc&ab_channel=JamesMontemagno
    public class SupplierService : BaseService
    {
        readonly InvoiceService _invoiceService;

        public SupplierService(DbContext dbContext, InvoiceService invoiceService) : base(dbContext)
        {
            _invoiceService = invoiceService;
        }

        public async Task<List<Supplier>> GetAllSuppliers()
        {
            try
            {
                return await dbContext.Database.Table<Supplier>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return null;
        }

        public async Task<Supplier> InsertSupplier(Supplier supplier)
        {
            try
            {
                await dbContext.Database.InsertAsync(supplier);

                return supplier;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return null;
        }

        public async Task<Supplier> UpdateSupplier(Supplier supplier)
        {
            try
            {
                await dbContext.Database.UpdateAsync(supplier);

                return supplier;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return null;
        }

        public async Task DeleteSupplier(Supplier supplier)
        {
            try
            {
                await dbContext.Database.DeleteAsync(supplier);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }
        }

        public async Task<Supplier> GetSupplierDetails(int supplierId)
        {
            try
            {
                var theSupplier = await dbContext.Database.Table<Supplier>().FirstOrDefaultAsync(x => x.Id == supplierId);
                if (theSupplier != null)
                {
                    var invoices = await _invoiceService.GetInvoicesBySupplierId(theSupplier.Id);
                    theSupplier.Invoices = invoices;

                    return theSupplier;
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return null;
        }
    }
}
