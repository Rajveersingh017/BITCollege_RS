using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Utility
{
    public class Encryption
    {

        /// <summary>
        /// the method is used to encrypt the data in the xml file.
        /// </summary>
        /// <param name="plaintextFileName">Contains the name of the non encrypted file</param>
        /// <param name="encryptedFileName">Contains the name of the encrypted file</param>
        /// <param name="key">The key used to encrypt and decrypt the file.</param>
        public static void Encrypt(string plaintextFileName, string encryptedFileName, string key) {
            
            FileStream plainTextFileStream = new FileStream(plaintextFileName,
                                                            FileMode.Open,
                                                            FileAccess.Read);
            FileStream encrytedFileStream = new FileStream(encryptedFileName,
                                                            FileMode.Create,
                                                            FileAccess.Write);

            DESCryptoServiceProvider desCrypto = new DESCryptoServiceProvider();
            desCrypto.Key = ASCIIEncoding.ASCII.GetBytes(key);
            desCrypto.IV = ASCIIEncoding.ASCII.GetBytes(key);


            ICryptoTransform encrytor = desCrypto.CreateEncryptor();

            CryptoStream cryptoStreamEncr = new CryptoStream(encrytedFileStream,
                                                             encrytor,
                                                             CryptoStreamMode.Write);

            byte[] byteArray = new byte[plainTextFileStream.Length];
            plainTextFileStream.Read(byteArray, 0, byteArray.Length);
            cryptoStreamEncr.Write(byteArray, 0, byteArray.Length);


            cryptoStreamEncr.Close();
            plainTextFileStream.Close();
            encrytedFileStream.Close();
        }

        /// <summary>
        /// This method is used to decrypt the file.
        /// </summary>
        /// <param name="plaintextFileName">Contains the name of the file without the .encryption extension</param>
        /// <param name="encryptedFileName">Contains the name of the file with the .encryption extension</param>
        /// <param name="key">The key used to encrypt or decrypt the file.</param>
        public static void Decrypt(string plaintextFileName, string encryptedFileName, string key) {
            StreamWriter decrypt = new StreamWriter(plaintextFileName);

            FileStream EncryptedFileStream = new FileStream(encryptedFileName,
                                                            FileMode.Open,
                                                            FileAccess.Read);
            try
            {


                DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();
                desProvider.Key = ASCIIEncoding.ASCII.GetBytes(key);
                desProvider.IV = ASCIIEncoding.ASCII.GetBytes(key);

                ICryptoTransform descryptor = desProvider.CreateDecryptor();

                CryptoStream cryptoDescrypt = new CryptoStream(EncryptedFileStream,
                                                               descryptor,
                                                               CryptoStreamMode.Read);

                decrypt.Write(new StreamReader(cryptoDescrypt).ReadToEnd());
                EncryptedFileStream.Close();
                decrypt.Flush();
                decrypt.Close();
            }
            catch (Exception e)
            {
                EncryptedFileStream.Close();
                decrypt.Close();
                throw new Exception("Unable to decrypt the file.");
            }
        }
    }
}
