using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperProga_2
{
    class MyClass
    {

        #region Static
        static volatile MyClass Class;
        static object SyncObject = new object();
        public static MyClass GetInstance
        {
            get
            {
                if (Class == null)
                {
                    lock (SyncObject)
                    {
                        if (Class == null)
                            Class = new MyClass();
                    }
                }
                return Class;
            }
        }
        #endregion

        public void Method()
        {
            MessageBox.Show("");
        }

    }
}
