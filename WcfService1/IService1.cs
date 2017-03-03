using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Xml;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract(Namespace = "")]
    public interface IService1
    {

        [WebGet(ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Xml)]
        [OperationContract(Name = "GetImportResult")]
        FLK GetImportResult(String fileName);

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "InsertTfomsFile")]
        [OperationContract(Name = "InsertTfomsFile")]
        FLK InsertTfomsFile(TfomsZipFile tfomsZipFile);

        // TODO: Добавьте здесь операции служб
    }


    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
   

    [DataContract(Namespace = "", Name = "TfomsZipFile")]
       public class TfomsZipFile
    {
        [DataMember]
        public String filename { get; set; }
        [DataMember]
        public String file { get; set; }
        [DataMember]
        public String login { get; set; }
        [DataMember]
        public String password { get; set; }
    }

   
    public class TfomsAnswer
    {
        [DataMember]
        public String filename { get; set; }
        [DataMember]
        public String file { get; set; }
    }


}
