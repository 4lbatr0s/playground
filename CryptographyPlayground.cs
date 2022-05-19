using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Aes_Example;
using RSA;
namespace CryptoGraphyPlayGround
{
    class Program
    {
        public static void Main()
        {


            //AES
            string original = "Here is some data to encrypt!";

            // Create a new instance of the Aes
            // class.  This generates a new key and initialization
            // vector (IV).
            using (Aes myAes = Aes.Create())
            {
                Stopwatch stopWatch = new Stopwatch();
                // var copyKey = myAes.Key; 

                // Encrypt the string to an array of bytes.
                stopWatch.Start();
                byte[] encrypted = AesExample.EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);
                stopWatch.Stop();
                TimeSpan encryptionTime = stopWatch.Elapsed;
                // Decrypt the bytes to a string.

                stopWatch.Start();
                string roundtrip = AesExample.DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);
                stopWatch.Stop();
                TimeSpan decryptionTime = stopWatch.Elapsed;
                //Display the original data and the decrypted data.
                Console.WriteLine("Original:   {0}", original);
                Console.WriteLine("Round Trip: {0}", roundtrip);
                Console.WriteLine("Encryption Time: {0}", encryptionTime);
                Console.WriteLine("Decyption Time: {0}", decryptionTime);
                Console.WriteLine("Total Time: {0}",encryptionTime + decryptionTime);
            }





            //RSA
            try
        {
            //Create a UnicodeEncoder to convert between byte array and string.
            UnicodeEncoding ByteConverter = new UnicodeEncoding();

            //Create byte arrays to hold original, encrypted, and decrypted data.
            byte[] dataToEncrypt = ByteConverter.GetBytes("Data to Encrypt");
            byte[] encryptedData;
            byte[] decryptedData;

            //Create a new instance of RSACryptoServiceProvider to generate
            //public and private key data.
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                //Pass the data to ENCRYPT, the public key information 
                //(using RSACryptoServiceProvider.ExportParameters(false),
                //and a boolean flag specifying no OAEP padding.
                encryptedData = RSACSPSample.RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);

                //Pass the data to DECRYPT, the private key information 
                //(using RSACryptoServiceProvider.ExportParameters(true),
                //and a boolean flag specifying no OAEP padding.
                decryptedData = RSACSPSample.RSADecrypt(encryptedData, RSA.ExportParameters(true), false);

                //Display the decrypted plaintext to the console. 
                Console.WriteLine("Decrypted plaintext: {0}", ByteConverter.GetString(decryptedData));
            }
        }
            catch (ArgumentNullException)
            {
                //Catch this exception in case the encryption did
                //not succeed.
                Console.WriteLine("Encryption failed.");
            }
        }

    }
}