using CommunityToolkit.Mvvm.Messaging.Messages;
using SampleMauiMvvmApp.Models;

namespace SampleMauiMvvmApp.Messages
{
    public class SupplierUpdateMessage : ValueChangedMessage<Supplier>
    {
        public SupplierUpdateMessage(Supplier value) : base(value)
        {

        }
    }
}
