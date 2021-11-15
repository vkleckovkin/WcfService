using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public Dictionary<string, int> Text_analis(string text)
        {
            //Путь к DLL
            string path = @"ClassLibrary_text.dll";
            //Имя метода
            string methodName = "Text_analis_parallel";
            Assembly assembly = Assembly.LoadFile(path);
            Type type = assembly.GetType("ClassLibrary_text.Class1");
            MethodInfo method = type.GetMethod(methodName);
            object[] param = { text };
            Dictionary<string, int> statistics = (Dictionary<string, int>)method.Invoke(null, param);
            return statistics;
        }
    }
}
