using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;

namespace JBanzDevCommonTools.Encryption
{
    public static class JbanzDevEncryption
    {
        public static string EncodeString(string stringValue)
        {
            // stringValue max length = 42 characters

            if (string.IsNullOrWhiteSpace(stringValue) || stringValue.Length > 40)
            {
                throw new ArgumentException("String cannot be empty or greater than 40 characters");
            }

            string strOUT = "";

            try
            {
                byte[] bA = new byte[50];
                byte[] bB = new byte[50];

                for (int x2 = 0; x2 < 50; x2++)
                {
                    bA[x2] = 32;
                    bB[x2] = 0;
                }

                using (var RMCrypto = new RijndaelManaged())
                {
                    // create the keys
                    var iv = new Byte[] { 254, 78, 32, 210, 12, 59, 82, 132, 47, 252, 106, 188, 46, 221, 66, 109 };
                    var key = new Byte[] { 88, 52, 58, 212, 214, 33, 212, 54, 98, 110, 17, 157, 213, 124, 115, 15 };

                    using (var mS = new MemoryStream(bA, true))
                    {
                        using (var CryptStream = new CryptoStream(mS, RMCrypto.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                        {
                            using (var SWriter = new StreamWriter(CryptStream, System.Text.Encoding.UTF8))
                            {
                                SWriter.WriteLine(stringValue);

                                SWriter.Flush();
                            }
                        }
                    }

                    using (var mSa = new MemoryStream(bB, true))
                    {
                        using (var cS = new CryptoStream(mSa, RMCrypto.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                        {
                            using (var sW = new StreamWriter(cS, System.Text.Encoding.UTF8))
                            {
                                sW.WriteLine(stringValue);
                                sW.Flush();
                            }
                        }
                    }
                }

                int x = 0;

                while (bA[x] == bB[x] && x < 49)
                {
                    x += 1;
                }

                for (int n = 0; n < x; n++)
                {
                    strOUT += returnHEXfromBYTE(bA[n]);
                }
            }
            catch (Exception)
            {
                if (true)
                {

                }
            }

            return strOUT;
        }

        private static string returnHEXfromBYTE(byte value)
        {
            var hexString = BitConverter.ToString(new byte[] { value });

            return hexString;
        }

        public static string DecodeString(string stringInValue)
        {
            string decodedString = "";

            using (var RMCrypto = new RijndaelManaged())
            {
                try
                {
                    //'now convert string into byte array
                    byte[] byteB = new byte[(stringInValue.Length / 2)];

                    //'do not use asciiencoder as it doesn't handle nonprinting 
                    //'eight bit characters correctly
                    for (int x = 0; x < stringInValue.Length - 1; x += 2)
                    {
                        byteB[x / 2] = returnBYTEfromHEX(stringInValue.Substring(x, 2));
                    }

                    using (var mS = new MemoryStream(byteB))
                    {
                        var iv = new byte[] { 254, 78, 32, 210, 12, 59, 82, 132, 47, 252, 106, 188, 46, 221, 66, 109 };
                        var key = new byte[] { 88, 52, 58, 212, 214, 33, 212, 54, 98, 110, 17, 157, 213, 124, 115, 15 };

                        using (var cryptStream = new CryptoStream(mS, RMCrypto.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                        {
                            using (var SR = new StreamReader(cryptStream, System.Text.Encoding.UTF8))
                            {
                                //'Write to the stream.
                                decodedString = SR.ReadToEnd();
                                decodedString = decodedString.Substring(0, decodedString.Length - 2);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                }
            }

            return decodedString;
        }

        private static byte returnBYTEfromHEX(string hexValue)
        {
            try
            {
                byte result = byte.Parse(hexValue, NumberStyles.HexNumber);
                return result;
            }
            catch (Exception)
            {
            }
            return 0;
        }

        //  ***********************************************************************

        public static bool EncrytToFile(string filePath, ref byte[] byteArray)
        {
            try
            {
                var iv = new byte[] { 254, 78, 32, 210, 12, 59, 82, 132, 47, 252, 106, 188, 46, 221, 66, 109 };

                var key = new byte[] { 88, 52, 58, 212, 214, 33, 212, 54, 98, 110, 17, 157, 213, 124, 115, 15 };

                using (FileStream fs = new System.IO.FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (RijndaelManaged RMCrypto = new RijndaelManaged())
                    {
                        using (CryptoStream encSTREAM = new CryptoStream(fs, RMCrypto.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                        {
                            encSTREAM.Write(byteArray, 0, byteArray.Length);
                            return true;
                        }

                    }

                }
            }
            catch (Exception)
            {
                if (true)
                {

                }

            }
            return false;
        }

        public static bool DecryptFromFile(string filePath, out byte[] byteArrayOut)
        {
            byteArrayOut = null;

            try
            {
                var iv = new byte[] { 254, 78, 32, 210, 12, 59, 82, 132, 47, 252, 106, 188, 46, 221, 66, 109 };
                var key = new byte[] { 88, 52, 58, 212, 214, 33, 212, 54, 98, 110, 17, 157, 213, 124, 115, 15 };

                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    using (var RMCrypto = new RijndaelManaged())
                    {
                        using (CryptoStream encStream = new CryptoStream(fs, RMCrypto.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                        {
                            int bufferSize = 1024;

                            byte[] buffer = new byte[bufferSize];
                            int bytesReadCount = 0;

                            List<byte> readByteList = new List<byte>();

                            do
                            {
                                bytesReadCount = encStream.Read(buffer, 0, bufferSize);

                                if (bytesReadCount > 0)
                                {
                                    for (int i = 0; i < bytesReadCount; i++)
                                    {
                                        readByteList.Add(buffer[i]);
                                    }
                                }

                            } while (bytesReadCount > 0);

                            byteArrayOut = readByteList.ToArray();


                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {

                if (true)
                {

                }
            }

            return false;
        }
    }
}
