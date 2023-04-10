using Bogus;
using SampleMauiMvvmApp.Models;

namespace SampleMauiMvvmApp.Fakers
{
    public class InvoiceFaker : Faker<Invoice>
    {
        public InvoiceFaker()
        {
            RuleFor(x => x.InvoiceNo, x => "INV" + x.Finance.Random.String2(5, "0123456789"));
            RuleFor(x => x.InvoiceDate, x => x.Date.Recent());
            RuleFor(x => x.InvoiceTotal, x => x.Random.Decimal(100, 1000));
        }
    }
}
