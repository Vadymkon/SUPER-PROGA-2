using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SuperProga_2
{
    class Internet
    {
        public static bool OK()
        {
            try
            {
                Dns.GetHostEntry("youtube.com");//("dotnet.beget.tech");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
