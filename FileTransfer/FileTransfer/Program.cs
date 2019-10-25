using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSCP;

namespace FileTransfer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Setup session options
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Sftp,
                    HostName = "168.61.165.13",
                    UserName = "found405",
                    Password = "found405@123",
                    SshHostKeyFingerprint = "ssh-ed25519 256 ihQkFYnNzmp+HPZhcqRg8yzyZ72uFVyt6lQ/9xA6rec="
                };

                using (Session session = new Session())
                {
                    DateTime dt1 = DateTime.Now;
                    StreamWriter sw = new StreamWriter(@"F:\Prudhvi\logs.txt");
                    // Connect
                    DateTime temp;// = DateTime.Now;
                    session.Open(sessionOptions);

                    // Upload files
                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;

                    TransferOperationResult transferResult;
                    //Console.WriteLine("Give the path: ");
                    //string folderName = Console.ReadLine();
                    string folderName = @"F:\Prudhvi\check";
                    DirectoryInfo d = new DirectoryInfo(folderName);//Assuming Test is your Folder
                    FileInfo[] Files = d.GetFiles("*.*"); //Getting Text files
                    sw.WriteLine("Started uploading. Time:" + dt1.TimeOfDay);
                    foreach (FileInfo file in Files)
                    {
                        temp = DateTime.Now;
                        transferResult =
                      session.PutFiles(folderName + "\\" + file.Name, "/home/found405/tifs/", false, transferOptions);

                        transferResult.Check();

                        // Print results
                        foreach (TransferEventArgs transfer in transferResult.Transfers)
                        {
                            Console.WriteLine("Upload of {0} succeeded", transfer.FileName);
                            sw.WriteLine("Upload of {0} of size {1} MB succeeded in {2}", transfer.FileName, (file.Length / 1048576).ToString("0.000"), DateTime.Now.Subtract(temp));
                        }
                        sw.Flush();
                    }
                    DateTime dt2 = DateTime.Now;
                    Console.WriteLine("Uploaded time: " + dt2.Subtract(dt1));
                    sw.WriteLine("completed uploading. Time:" + dt2.TimeOfDay);
                    sw.WriteLine("Total time for uploading :" + dt2.Subtract(dt1));
                    sw.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
                Console.ReadLine();
            }


            Console.WriteLine("Completed..");
            Console.ReadKey();
        }
    
    }
}
