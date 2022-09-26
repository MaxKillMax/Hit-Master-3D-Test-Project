using System.Collections.Generic;
using System.Linq;

namespace HitMaster3DTestProject
{
    public class ServiceLocator
    {
        private static readonly List<IService> Services = new List<IService>(2);

        public static void RegisterService(IService service)
        {
            if (!Services.Contains(service))
                Services.Add(service);
        }

        public static void UnRegisterService(IService service)
        {
            if (Services.Contains(service))
                Services.Remove(service);
        }

        public static T GetService<T>() where T : IService
        {
            return Services.OfType<T>().FirstOrDefault();
        }
    }
}
