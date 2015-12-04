using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageService;
using System.ServiceModel;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CTrafficMessage : ITrafficMessage
    {
        private List<string> trafficMessages;

        CTrafficMessage()
        {
            trafficMessages = new List<string>();
        }

        public string GetServerName()
        {
            return "Smart Tankstation Server";

        }

        public void SendMessage(string message, int adress)
        {
                trafficMessages.Add(adress.ToString() + message);
        }

       public string RetrieveMessage(int adress)
        {
                int counter = 0;

                foreach (string item in trafficMessages)
	                {
		                if(item.Substring(0,1) == adress.ToString())
                        {
                              string returnstring = item.Substring(1,item.Length-1);
                              trafficMessages.RemoveAt(counter);
                              counter++;
                              return returnstring;              
                        }
	                }

                return "";                     
         }   
    }          
 }
