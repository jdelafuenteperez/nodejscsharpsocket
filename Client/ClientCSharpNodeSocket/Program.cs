using System;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ClientCSharpNodeSocket
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //Creamos el objeto Recognition
            Image avatar = Image.FromFile("profile.png");

            Employee _employee = new Employee(1, "Empleado", "Uno", DateTime.Today, avatar);

            //Nos conectamos al servidor
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect("127.0.0.1", 8000);

            //Convertimos el objeto Employee a un documento JSON
            string strEmployee = JsonConvert.SerializeObject(_employee);
            Console.WriteLine(strEmployee);
            //Preparamos los datos para enviarlos
            byte[] data = Encoding.ASCII.GetBytes(strEmployee);

            //Enviamos los datos
            s.Send(data);

            s.Close();
            Console.WriteLine("Socket Connected : " + s.Connected);
            Console.WriteLine("Finish!");

            s.Dispose();
            Console.ReadKey();
        }
    }
}