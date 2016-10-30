using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using  System.Security.Cryptography;
using  System.IO;
using System.Text;
using Ionic.Zip;
using Ionic.Zlib;

namespace Globally
{
    public static class vars
    {
        public static string path_for_backup;
        public static bool autoBackup;

        public static class  mysql
        {
            public static string database;
            public static string host;
            public static string login;
            public static string password;
            public static string pp;
        }

        public  static class  ftp
        {
            public static string host;
            public static string login;
            public static string password;
            public static int port;
            public static string mm;
        }
    }

    public static class function
    {
        public static class ArchiveOperations
        {
            static string DefaultFolderForSaveFiles = @"\Temp";
            static string currentDirectory = Environment.CurrentDirectory;
            private static string dir = currentDirectory + DefaultFolderForSaveFiles;

            /// <summary>
            /// Функция для выгрузки из архива
            /// </summary>
            /// <param name="filename">Наименование файла (содержит путь)</param>
            public static void Extract(string filename)
            {
                Encoding enc = Encoding.GetEncoding(1251);
                Encoding cp866 = Encoding.GetEncoding("cp866");
                using (ZipFile zip = new ZipFile(filename, cp866))
                {
                    zip.ExtractAll(dir);
                    zip.Dispose();
                }
                /* using(ZipFile zip = ZipFile.Read(filename))
                 {
                     zip.ExtractAll(Environment.CurrentDirectory);
                     zip.Dispose();
                 }*/

            }

            /// <summary>
            /// Функция для архивирования
            /// </summary>
            /// <param name="filename">Наименование файла (содержит путь)</param>
            /// <param name="short_filename">Короткое наименование файла</param>
            public static void Pack(string filename, string short_filename)
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(filename);
                    zip.Save(/*Environment.CurrentDirectory + "\\" +*/ short_filename.Remove(short_filename.Length - 4, 4) + ".zip");
                    zip.Dispose();
                }
            }

            /// <summary>
            /// Функция для архивирования
            /// </summary>
            /// <param name="filename">Наименование файла (содержит путь)</param>
            /// <param name="short_filename">Короткое наименование файла</param>
            /// <returns>Возвращает путь по которому лежит созданный архив </returns>
            public static string Pack2(List<FileName> filenames)
            {
                string DefaultFolderForSaveFiles = @"\dump";
                string currentDirectory = Environment.CurrentDirectory;
                string dir = vars.path_for_backup;
                //Console.WriteLine(filenames[0].short_filename);
                string newPath = "";
                if (filenames[0].short_filename.IndexOf(".") != -1)
                {
                    newPath = dir + "\\" + filenames[0].short_filename.Remove(filenames[0].short_filename.Length - 4, 4) + ".zip";
                }
                else
                {
                    newPath = dir + "\\" + filenames[0].short_filename + ".zip";
                }

                //Console.WriteLine("newPath:" + newPath);
                // Добавление файлов в архив:
                
                using (ZipFile zip = new ZipFile())
                {
                    zip.AlternateEncodingUsage = ZipOption.Always;
                    zip.AlternateEncoding = Encoding.GetEncoding(866); 
                    foreach (FileName f in filenames)
                    {
                        Console.WriteLine(f.full_filename);
                        zip.AddFile(f.full_filename, "");
                    }
                    zip.CompressionLevel = CompressionLevel.Level5;
                    zip.Save(newPath);
                }
                return newPath;
            }

            /// <summary>
            /// Получение имени файла из архива
            /// </summary>
            /// <param name="archivePath">Путь к архиву</param>
            /// <returns>Имя файла</returns>
            public static string SendRFilename(string archivePath)
            {
                string rfilename = "";
                using (ZipFile zip = ZipFile.Read(archivePath))
                {
                    foreach (ZipEntry e in zip)
                    {
                        if (e.FileName.IndexOf(".sql") != -1)
                        {
                            rfilename = e.FileName;
                        }
                    }
                }
                return rfilename;
            }
        }

        public static class AESSha1Crypter
        {
            public static string Encrypt(string plainText, string password,
                string salt = "Kosher", string hashAlgorithm = "SHA1",
                int passwordIterations = 2, string initialVector = "OFRna73m*aze01xY",
                int keySize = 256)
            {
                if (string.IsNullOrEmpty(plainText))
                    return "";

                byte[] initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                PasswordDeriveBytes derivedPassword = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIterations);
                byte[] keyBytes = derivedPassword.GetBytes(keySize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;

                byte[] cipherTextBytes = null;

                using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initialVectorBytes))
                {
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            cipherTextBytes = memStream.ToArray();
                            memStream.Close();
                            cryptoStream.Close();
                        }
                    }
                }

                symmetricKey.Clear();
                return Convert.ToBase64String(cipherTextBytes);
            }

            public static string Decrypt(string cipherText, string password,
            string salt = "Kosher", string hashAlgorithm = "SHA1",
            int passwordIterations = 2, string initialVector = "OFRna73m*aze01xY",
            int keySize = 256)
            {
                if (string.IsNullOrEmpty(cipherText))
                    return "";

                byte[] initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

                PasswordDeriveBytes derivedPassword = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIterations);
                byte[] keyBytes = derivedPassword.GetBytes(keySize / 8);

                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;

                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int byteCount = 0;

                using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initialVectorBytes))
                {
                    using (MemoryStream memStream = new MemoryStream(cipherTextBytes))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read))
                        {
                            byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                            memStream.Close();
                            cryptoStream.Close();
                        }
                    }
                }

                symmetricKey.Clear();
                return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
            }
        }
    }

    public class FileName
    {
        public FileName()
        {

        }

        public FileName(string full_filename, string short_filename)
        {
            this.full_filename = full_filename;
            this.short_filename = short_filename;
        }

        public string full_filename;
        public string short_filename;
    }

    /// <summary>
    /// Класс для хранения информации о файле на фтп
    /// </summary>
    public class FtpFile
    {
        public string name;
        public int size;

        public FtpFile()
        {
            
        }
        public FtpFile(string name, int size)
        {
            this.name = name;
            this.size = size;
        }
    }

    /// <summary>
    /// Класс для хранения информации о директории на фтп
    /// </summary>
    public class FtpDirectory 
    {
        public string Name;
        public string fullName;
        public string Parent;

        public FtpDirectory()
        {
            
        }

        public FtpDirectory(string name)
        {
            this.Name = name;
        }

        public FtpDirectory(string fullName, string name)
        {
            this.fullName = fullName;
            this.Name = name;
        }


        public FtpDirectory(string fullName, string name, string parent)
        {
            this.fullName = fullName;
            this.Name = name;
            this.Parent = parent;
        }

        public void setParent(string Parent)
        {
            this.Parent = Parent;
        }
    }
}
