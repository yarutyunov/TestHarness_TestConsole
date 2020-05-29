using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.OrderStatusReference;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:4716/TestHarnessService/OrderStatus/TestHarnessOrderStatus.svc";
            var binding = getHttpBinding(url);
            var address = new EndpointAddress(url);
            //var service = new Service1Client(binding, address);
            //var request = new GetOrderStatusDetailsRequest();
            var service = new StarLineReference.OrderStatusServiceClient(binding, address);

            var request = new StarLineReference.GetOrderStatusDetailsRequest
            {
                id = "user",
                password = "password",
                referenceNumber = "ABC201",
                wsVersion = "1.0.0"
            };

            var result = service.getOrderStatusDetails(request);



            var typesRequest = new StarLineReference.GetOrderStatusTypesRequest
            {
                id = "user",
                password = "password",
                wsVersion = "1.0.0"
            };
            var typesResult = service.getOrderStatusTypes(typesRequest);
        }

        public static BasicHttpBinding getHttpBinding(string url)
        {
            var binding = new BasicHttpBinding
            {
                Security = !string.IsNullOrEmpty(url) && url.Contains("https")
                    ? new BasicHttpSecurity { Mode = BasicHttpSecurityMode.Transport }
                    : new BasicHttpSecurity { Mode = BasicHttpSecurityMode.None },
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647,
            };

            return binding;
        }
    }
}
