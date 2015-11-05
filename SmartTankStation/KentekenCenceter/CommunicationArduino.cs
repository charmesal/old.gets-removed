using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace CarCenter
{
    class CommunicationArduino
    {
        private SerialPort mySerialPort;
        private Fuelstation fuelstation;

        public CommunicationArduino(Fuelstation fuelstation, string compoort)
        {
            this.fuelstation = fuelstation;

            mySerialPort = new SerialPort(compoort);

            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.RtsEnable = true;

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            //mySerialPort.Open(); for testing 
        }

        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadLine();
           if (indata.Length == 9 && indata.Substring(2, 1) == "-" && indata.Substring(5, 1) == "-")
           {
               indata = indata.Substring(0, 8);
              SendMessage(string.Format("%{0}#",fuelstation.GetFuelType(indata).ToString()));
           }
           if (indata.StartsWith("ready"))
           {
               fuelstation.sendSerialMsg(1, "start");
               fuelstation.sendSerialMsg(2, "start");
           }


                        //pay:AA-00-AA,amountOfFuel:500
            if (indata.StartsWith("pay"))

            {
                string[] data = indata.Split(',');
                string[] kentekenData = data[0].Split(':');
                string kentenen = kentekenData[1];
                string[] amountOfFuel = data[1].Split(':');
                //string fuel = amountOfFuel[1];
                decimal fuel = 0m;
                decimal.TryParse(amountOfFuel[1], out fuel);
                fuelstation.Pay(kentenen, fuel);
            }
        }

        public void CloseConnection()
        {
            if (mySerialPort.IsOpen) 
            { 
                mySerialPort.Close(); 
            }
        }

        public void SendMessage(string message)
        {
            mySerialPort.WriteLine(message);
        }
    }
}
