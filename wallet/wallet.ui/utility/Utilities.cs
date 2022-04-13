using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallet.ui.utility
{
    public class Utilities
    { 
        public static Image LoadBase64(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }
            return image;
        }
    }




}
