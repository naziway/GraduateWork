using System;

namespace Shared
{
    public class TransferData
    {
        public event EventHandler<object> OnTransferData;

        public void OnOnTransferData(object e)
        {
            OnTransferData?.Invoke(this, e);
        }
    }
}