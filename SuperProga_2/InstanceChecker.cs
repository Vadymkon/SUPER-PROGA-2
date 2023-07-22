using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperProga_2
{
    class InstanceChecker
    {

        #region Static
        static volatile InstanceChecker Class;
        static object SyncObject = new object();
        public static InstanceChecker GetInstance
        {
            get
            {
                if (Class == null)
                {
                    lock (SyncObject)
                    {
                        if (Class == null)
                            Class = new InstanceChecker();
                    }
                }
                return Class;
            }
        }
        #endregion

        static readonly Mutex mutex = new Mutex(false,Application.ProductName);
        bool taken;
        public bool TakeMemory() => taken = mutex.WaitOne(0,true);
        public void ReleaseMemory() { if (taken) try { mutex.ReleaseMutex(); } catch { } }

        /*
         * Закинь после инициализации
         if (!InstanceChecker.GetInstance.TakeMemory()) { MessageBox.Show("Already working. Ещё раз запустишь, пока работает копия, получишь пизды."); Close(); }
            FormClosing += (a, e) => { InstanceChecker.GetInstance.ReleaseMemory(); };
            SizeChanged += (a,e) => { if (WindowState == FormWindowState.Minimized) InstanceChecker.GetInstance.ReleaseMemory(); }; //для сворачивания 
         */
    }
}
