using CommunityToolkit.Mvvm.Messaging.Messages;
using SampleMauiMvvmApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMauiMvvmApp.Messages
{
    public class InvoiceCreateMessage : ValueChangedMessage<Invoice>
    {
        public InvoiceCreateMessage(Invoice value) : base(value)
        {

        }
    }
}
