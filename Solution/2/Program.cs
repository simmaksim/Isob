using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Collections.Generic;

namespace ISOB_2_Kerberos
{
    public class Program
    {
        private static readonly string KDCMasterKey = GetHash("masterKey");
        private static readonly string serverMasterKey = GetHash("serverMasterKey1");
        private static readonly string keyK_cs = GetHash("keyK_css");
        private static string clientSessionKey;
        private static string KDCsessionKey;
        private static DateTime ClientTimeStamp { get; set; }
        private static readonly Dictionary<string, string> UserList = new()
        { 
            ["simmaksim"] = "MSlizh"
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Enter username:");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();
            Client clientUser = new Client(userName, password);
            /*
             * 1)
             * Компьютер пользователя обращается к службе KDC и передает ей 
             * имя пользователя, а также текущее время на рабочей станции 
             * пользователя, при этом имя пользователя передается в открытом виде, 
             * текущее время на рабочей станции пользователя передается в 
             * зашифрованном виде и является аутентификатором. 
             */

            ClientTimeStamp = DateTime.Now;
            var value = new StringBuilder();
            value.Append(userName + "/")
                 .Append(DES.Encrypt(ClientTimeStamp.ToString(), clientUser.PasswordHash));
            string message = value.ToString();

            Console.WriteLine($"Encripted message: { message }");

            /*
             * 2)
             * Служба KDC ищет пользователя в AS, 
             * выявляет мастер ключ пользователя, 
             * который основан на пароле пользователя и расшифровывает аутентификатор, 
             * т. е. получает время отправки запроса. Разница во времени отправки запроса и
             * текущего времени на контроллере домена не должно превышать определенного значения, 
             * установленного политикой протокола Kerberos
             */

            // Authentification server

            Console.WriteLine($"Trying to find client...");
            Thread.Sleep(2000);
            DateTime timeStamp;
            Client KDCclient;

            var userData = message.Split('/');
            if (UserList.TryGetValue(userData[0], out string userPass))
            {
                KDCclient = new Client(userName: userData[0], password: userPass);
                if (DateTime.TryParse(DES.Decipher(userData[1], KDCclient.PasswordHash), out timeStamp))
                {
                    if (timeStamp.AddMinutes(2) > DateTime.Now)
                    {
                        Console.WriteLine("Time doesn't exceed 2 mins...");
                    }
                    else
                    {
                        Console.WriteLine("Time exceeded 2 mins...");
                        Console.ReadLine();
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Your password is wrong...");
                    Console.ReadLine();
                    return;
                }
            }
            else
            {
                Console.WriteLine("Client not found...");
                Console.ReadLine();
                return;
            }


            /*
            * 3)
            * Затем KDC создает два объекта:
            * a. ключ сессии, посредством которого будет обеспечиваться зашифрование данных при обмене между клиентом и службой KDC,
            * b. билет на получение билета "Ticket-Granting Ticket" (TGT). 
            * TGT включает: вторую копию ключа сессии, имя пользователя, 
            * время окончания жизни билета. Билет на получение билета шифруется с использованием собственного мастер ключа службы KDC,
            * который известен только KDC, т. е. TGT может быть расшифрован только самой службой KDC.
            */

            // KDC -> Client (encripted TGT with KDC master key)
            Console.WriteLine($"Generating TGT...");
            Thread.Sleep(2000);

            KDCsessionKey = GetHash(new Random().Next(1000000, 9999999).ToString());
            var TGT = new StringBuilder();
            TGT.Append(KDCsessionKey + "/")
               .Append(KDCclient.UserName + "/")
               .Append(DateTime.Now.AddMinutes(30).ToString() + "/")
               .Append(DateTime.Now);
            string encriptedTGT = DES.Encrypt(TGT.ToString(), KDCMasterKey);
            Console.WriteLine($"TGT: {TGT}");
            Console.WriteLine($"Encripted TGT: {encriptedTGT}");

            /* 4)
            * Служба KDC зашифровывает аутентификатор пользователя (time stamp)
            * и ключ сессии с помощью ключа клиента.
              После этого эти данные отправляются клиенту. */


            // KDC -> Client (encripted client auth(time)+TGT ticket + session key (encr on pass) with KDC client key)
            Console.WriteLine($"Generating TGS ticket(session key) ...");
            Thread.Sleep(2000);

            var toUser = new StringBuilder();
            toUser.Append(encriptedTGT + "/")
                  .Append(timeStamp + "/")
                  .Append(KDCsessionKey);
            string encryptedToUser = DES.Encrypt(toUser.ToString(), KDCclient.PasswordHash);
            Console.WriteLine($"toUser: {toUser}");
            Console.WriteLine($"Encripted ToUser: {encryptedToUser}");
            
            /* 5)
            * Компьютер клиента получает информацию от службы KDC,
            * проверяет аутентификатор,
            * расшифровывает ключ сессии. */

            Console.WriteLine($"Sending TGT and TGS ticket to client...");
            Thread.Sleep(2000);

            Console.WriteLine($"Decripting data using client key...");
            Thread.Sleep(1000);

            string decryptToUser = DES.Decipher(encryptedToUser, clientUser.PasswordHash);
            var TGTtimeStampAndSessionKey = decryptToUser.Split('/');
            timeStamp = DateTime.Parse(TGTtimeStampAndSessionKey[1]);

            if (timeStamp.AddMinutes(2) > DateTime.Now)
            {
                clientSessionKey = TGTtimeStampAndSessionKey[2];
                Console.WriteLine($"Authentification passed!\n{new string('-', 40)}");
            }
            else
            {
                Console.WriteLine("Authentification failed!");
                Console.ReadLine();
                return;
            }
            /*
             * 6)
             * Теперь клиент обладает ключом сессии и TGT,
             * что предоставляет возможность безопасного 
             * взаимодействия со службой KDC.
             */

            /*
             * 1)
             * Клиент обращается к TGS. 
             * Клиент представляет KDC свой TGT и маркер времени,
             * которые зашифрованы с помощью ключа сессии, 
             * известного службе KDC.
             */

            // Client -> TGS
            Console.WriteLine($"Request to TGS...");
            Thread.Sleep(2000);

            var toKDC = new StringBuilder();
            toKDC.Append(TGTtimeStampAndSessionKey[0] + "/");
            toKDC.Append(DES.Encrypt(DateTime.Now.ToString(), clientSessionKey));
            Console.WriteLine($"Request to TGS (KDC): {toKDC}");

            /*
             * 2)
             * KDC расшифровывает TGT, используя свой KDC master key.
             * Маркер времени расшифровывается с помощью session key from TGT.
             * Теперь KDC может подтвердить, 
             * что запрос пришел от «правильного» пользователя, 
             * т.к. этот пользователь может использовать этот сессионный ключ.
             */

            // Ticket Granting Server
            Console.WriteLine($"Decripting TGT and Auth1 block...");
            Thread.Sleep(2000);

            var toTGSData = toKDC.ToString().Split('/');
            var decriptedTGT = DES.Decipher(toTGSData[0], KDCMasterKey);
            var tgtData = decriptedTGT.Split('/');
            timeStamp = DateTime.Parse(DES.Decipher(toTGSData[1], tgtData[0])); // tgtData[0] - session key from TGT
            var tgtTimeStamp = DateTime.Parse(tgtData[3]); //tgtData[3] - time stamp from TGT
            if (timeStamp.AddMinutes(2) > tgtTimeStamp) // Timestamp from auth block ~ equals TGT blocks` timestamp
            {
                Console.WriteLine("TGS Authentification passed!");
                Console.WriteLine($"Decripted TGT: {decriptedTGT}\n{new string('-', 40)}");
            }
            else
            {
                Console.WriteLine("TGS Authentification failed!");
                Console.ReadLine();
                return;
            }

            // TGS -> Client
            Console.WriteLine($"Preparing data for client...");
            Thread.Sleep(2000);

            var ticketToClient = new StringBuilder();
            var tgsBlock = new StringBuilder();         
            timeStamp = DateTime.Now;
            tgsBlock.Append(KDCclient.UserName + "/")
                    .Append("Read&write access" + "/")
                    .Append("ServerName" + "/")
                    .Append(timeStamp + "/")
                    .Append(timeStamp.AddMinutes(30) + "/")
                    .Append(keyK_cs);  // Key for interaction client with SS
            var ticketToServer = DES.Encrypt(tgsBlock.ToString(), serverMasterKey); // Ktgs_ss
            ticketToClient.Append(ticketToServer);
            ticketToClient.Append("/" + keyK_cs);
            Console.WriteLine("ticketToClient: " + ticketToClient);

            /*
             * 3)
             * Вся эта структура зашифровывается с помощью сессионного ключа,
             * который стал доступен пользователю при аутентификации.
             * После чего эта информация отправляется клиенту.
             */

            // TGS -> Client
            Console.WriteLine($"Encripting and sending data to client...");
            Thread.Sleep(2000);

            var encryptTicketToClient = DES.Encrypt(ticketToClient.ToString(), KDCsessionKey);
            Console.WriteLine("Encripted data from TGS: " + encryptTicketToClient);


            /*
             * 4)
             * Получив билет, клиент расшифровывает его с помощью сессионного ключа,
             * т. е. K_cs становится доступным клиенту, K_cs доступен также и серверу.
             * Клиент не может прочитать билет сервера, т. к. он зашифрован на ключе сервера.
             */

            var decryptedToClient = DES.Decipher(encryptTicketToClient, clientSessionKey);
            Console.WriteLine($"Decrypted TicketToClient: {decryptedToClient}");
            var clientK_cs = decryptedToClient.Split('/')[1];
            Console.WriteLine($"userK_cs: {clientK_cs}");

            /*
             * 5)
             * Клиент зашифровывает маркер времени с помощью ключа,
             * K_cs затем отправляет маркер времени и билет сервера
             * самому серверу, к ресурсам которого пытается получить
             * доступ клиент.
             */

            var toServer = new StringBuilder();
            ClientTimeStamp = DateTime.Now;
            toServer.Append(DES.Encrypt(ClientTimeStamp.ToString(), clientK_cs))
                    .Append("/" + decryptedToClient.Split('/')[0]); // Encripted TGS

            Console.WriteLine($"Data to Server: {toServer}");

             /* 6)
             *  Получив эту информацию, на первом этапе сервер расшифровывает
             *  свой билет, используя свой долговременный ключ. 
             *  Это предоставляет возможность получить доступ к K_cs ,
             *  с помощью которого будет на втором этапе расшифрован маркер времени,
             *  полученный от клиента.*/

            // SS checks that client can be trusted and gets 

            var toServerData = toServer.ToString().Split("/");
            var decryptedTicketToServer = DES.Decipher(toServerData[1], serverMasterKey);
            var ticketToServerData = decryptedTicketToServer.Split('/');
            Console.WriteLine("decryptTicketToServer: " + decryptedTicketToServer);
            var serverK_cs = ticketToServerData[5];
            var tgsTimeStamp = DateTime.Parse(ticketToServerData[3]);
            Console.WriteLine("Server K_cs: " + serverK_cs);
            timeStamp = DateTime.Parse(DES.Decipher(toServerData[0], serverK_cs));
            if (timeStamp.AddMinutes(2) > tgsTimeStamp)
            {
                Console.WriteLine("Server authentification passed!\n" +
                    $"Requested access: {ticketToServerData[1]}\n" +
                    $"Server name: {ticketToServerData[2]}");
            }
            else
            {
                Console.WriteLine("Server authentification failed!");
                Console.ReadLine();
                return;
            }

            // SS -> Client (Auth2.TimeStamp + 1)

            var encriptedServerTimeStamp = DES.Encrypt(timeStamp.AddMinutes(1).ToString(), serverK_cs);
            Console.WriteLine("Encripted timestamp SS->Client...");

            // Client checks that SS could be trusted

            var decipheredServerTimeStamp = DES.Decipher(encriptedServerTimeStamp, clientK_cs);

            if (ClientTimeStamp.AddMinutes(1).ToString().Equals(decipheredServerTimeStamp))
            {
                Console.WriteLine("Client can trust to the server!");
            }
            else
            {
                Console.WriteLine("Client can't trust to the server!");
                Console.ReadLine();
                return;
            }
        }

        private static string GetHash(string str)
        {
            var tmpSource = Encoding.ASCII.GetBytes(str);
            var tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            var value = new StringBuilder(tmpHash.Length);
            for (int i = 0; i < tmpHash.Length; i++)
            {
                value.Append(tmpHash[i].ToString("X2"));
            }
            return value.ToString();
        }
    }
}