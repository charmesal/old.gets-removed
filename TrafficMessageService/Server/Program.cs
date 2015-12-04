using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using MessageService;
using System.Net;

namespace Server
{

    class Program
    {          
        static void Main(string[] args)
        {
            string baseAddress = GetIpAdress();
            while (baseAddress == "")
            {
                Console.WriteLine("No Connection with local network, Connect and press <enter>");
                Console.ReadLine();
                baseAddress = GetIpAdress();
            }


            // creeer een host proces voor de TrafficMessageService
            ServiceHost host = new ServiceHost(typeof(CTrafficMessage));
            

            // creeer een zgn. end-point voor de service
            Type contract = typeof(ITrafficMessage);
            BasicHttpBinding binding = new BasicHttpBinding();
            Uri address = new Uri(baseAddress + "/MessageService");
            host.AddServiceEndpoint(contract, binding, address);

            // creeer een zgn. mex endpoint om de wsdl van de service te hosten
            host.Description.Behaviors.Add(new ServiceMetadataBehavior());
            EndpointAddress endPointAddress = new EndpointAddress(baseAddress + "/MEX");
            ServiceEndpoint mexEndpoint = new ServiceMetadataEndpoint(endPointAddress);
            host.AddServiceEndpoint(mexEndpoint);
            try
            {
            // start de service
            host.Open();     
            // hou het proces in de lucht tot de gebruiker op enter drukt
            Console.WriteLine("Service ITrafficMessage successfully hosted at address: ");
            Console.WriteLine(baseAddress + "/MEX");
            Console.WriteLine("\nPress <enter> to end the service...");
       
            Console.ReadLine();

            }
            catch (TimeoutException timeProblem)
            {
                Console.WriteLine(timeProblem.Message);
                Console.ReadLine();
            }
            catch (CommunicationException commProblem)
            {
                Console.WriteLine(commProblem.Message + "\n");
                Console.WriteLine("Are you running this as an administrator?");
                Console.ReadLine();
            }
        }

        public static string GetIpAdress()
        {
            IPHostEntry hosting;
            string localIP = "?";

            hosting = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in hosting.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                    string[] bytes = localIP.Split('.');

                    if (Convert.ToInt32(bytes[3]) > 1) //ik vind ook adressen die eindigen op .1 van mijn VMware
                    {
                        return "http://" + localIP;
                    }
                }
            }
            return "";
        }
    }
}
