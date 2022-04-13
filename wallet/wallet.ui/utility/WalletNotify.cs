using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallet.ui.utility
{
 
 

    public class ProcessWalletMoney
    {
        public event EventHandler UpdateCompleted;

        public void StartUpdate()
        {
             
            OnUpdateCompleted(EventArgs.Empty); //No event data
        }

        protected virtual void OnUpdateCompleted(EventArgs e)
        {
            UpdateCompleted?.Invoke(this, e);
        }
    }


}
