using CommunityToolkit.Mvvm.Messaging.Messages;
using SampleMauiMvvmApp.Models;

namespace SampleMauiMvvmApp.Messages
{
    public class SupplierCreateMessage : ValueChangedMessage<Supplier>
    {
        public SupplierCreateMessage(Supplier value) : base(value)
        {

        }
    }
}
