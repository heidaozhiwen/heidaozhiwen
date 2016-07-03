using Autofac;
using Autofac.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExtention
{
   /// <summary>
    /// 使用AutoFac的工厂类，通过配置
    /// </summary>
    public class AutoFactory
    {
        //普通局部变量
        private static object syncRoot = new Object();
        //工厂类的单例
        private static AutoFactory instance = null;
        //配置文件
        private const string configurationFile = "Autofac.config";

        /// <summary>
        /// IOC的容器，可调用来获取对应接口实例。
        /// </summary>
        public IContainer Container { get; set; }

        /// <summary>
        /// IOC容器工厂类的单例
        /// </summary>
        public static AutoFactory Instatnce
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new AutoFactory();

                            //初始化相关的注册接口
                            var builder = new ContainerBuilder();
                            //从配置文件注册相关的接口处理
                            builder.RegisterModule(new ConfigurationSettingsReader("autofac", configurationFile));
                            instance.Container = builder.Build();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
