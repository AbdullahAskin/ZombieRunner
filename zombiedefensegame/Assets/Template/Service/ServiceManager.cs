using System.Collections.Generic;

namespace Service
{
    public static class ServiceManager
    {
        private static List<ServiceBase> serviceBases = new List<ServiceBase>();

        public static T GetService<T>() where T : ServiceBase
        {
            return (T)serviceBases.Find(x => x.GetType() == typeof(T));
        }

        public static void AddService(ServiceBase serviceBase)
        {
            serviceBases.Add(serviceBase);
        }

        public static void RemoveService(ServiceBase serviceBase)
        {
            serviceBases.Remove(serviceBase);
        }
    }
}