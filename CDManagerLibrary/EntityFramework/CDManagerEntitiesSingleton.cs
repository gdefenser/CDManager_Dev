using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDManagerLibrary.EntityFramework
{
    public class CDManagerEntitiesSingleton
    {
        private volatile static CDManagerDevEntities cde;
        private static readonly object lockHelper = new object();
        //单例模式
        public static CDManagerDevEntities GetCDManagerDevEntities()
        {
            if (cde == null)
            {
                //给实例化操作加上线程互斥锁
                lock (lockHelper)
                {
                    if (cde == null)
                    {
                        cde = new CDManagerDevEntities();
                    }
                }
            }
            return cde;
        }
    }
}
