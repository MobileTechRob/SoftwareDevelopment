using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Security;
using System.Security.Cryptography;

namespace DataEncryption
{
    /// <summary>
    /// Summary description for DataEncryption
    /// </summary>
    public class DataEncryption
    {
        public DataEncryption()
        {
            //
            // TODO: Add constructor logic here
            //
            Aes managedCrypt = AesManaged.Create();

            Aes crypt = Aes.Create("SHA-512");

            crypt.CreateEncryptor();



        }

        public static string EncryptString()
        {
            return "";
        }

        public static int EncryptInt()
        {
            return 0;
        }
    }
}
