using SampleMauiMvvmApp.ModelWrappers;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SampleMauiMvvmApp.Models
{
    public class Supplier
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abn { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        // https://www.codejourney.net/sqlite-net-extensions-one-to-many-relationships/
        [OneToMany]
        public List<Invoice> Invoices { get; set; }

        public static Supplier GenerateNewFromWrapper(SupplierWrapper wrapper)
        {
            return new Supplier()
            {
                Id = wrapper.Id,
                Name = wrapper.Name,
                Abn = wrapper.Abn,
                Phone = wrapper.Phone,
                Email = wrapper.Email,
                Address = wrapper.Address,
            };
        }
    }
}
