using SampleMauiMvvmApp.ModelWrappers;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SampleMauiMvvmApp.Models
{
    public class Invoice
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string InvoiceNo { get; set; }

        public DateTime InvoiceDate { get; set; }

        public Decimal InvoiceTotal { get; set; }


        // https://www.codejourney.net/sqlite-net-extensions-one-to-many-relationships/
        [ForeignKey(typeof(Supplier), Name = "Id")]
        public int SupplierId { get; set; }

        public static Invoice GenerateNewFromWrapper(InvoiceWrapper wrapper)
        {
            return new Invoice()
            {
                Id = wrapper.Id,
                InvoiceNo = wrapper.InvoiceNo,
                InvoiceDate = wrapper.InvoiceDate,
                InvoiceTotal = wrapper.InvoiceTotal,
                SupplierId = wrapper.SupplierId
            };
        }
    }
}
