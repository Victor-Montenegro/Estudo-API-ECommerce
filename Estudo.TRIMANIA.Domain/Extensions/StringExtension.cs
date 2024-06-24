using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Estudo.TRIMANIA.Domain.Extensions
{
    public static class StringExtension
    {
        public static string Encrypt(this string? passworld)
        {
            using (Aes myAes = Aes.Create())
            {

                byte[] encrypted = EncryptStringToBytes_Aes(passworld);
                return Convert.ToBase64String(encrypted);
            }
        }

        private static byte[] EncryptStringToBytes_Aes(string plainText)
        {
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.IV = Convert.FromBase64String("JnIIyj8Xiz9P4JjuU+cx/Q==");
                aesAlg.Key = Convert.FromBase64String("UU4h9tqOiPM/xE+c3i8hXSG9i8W08eVF7o3ZCFmBlSc=");

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        public static bool IsCpfValid(this string cpf)
        {
            int sum = 0;

            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = cpf.FormatCpf();

            if (cpf.Length != 11)
                return false;

            if (new string(cpf[0], cpf.Length) == cpf)
                return false;

            for (int i = 0; i < 9; i++)
                sum += (cpf[i] - '0') * (10 - i);

            int rest = sum % 11;
            int checkDigit = rest < 2 ? 0 : 11 - rest;

            if ((cpf[9] - '0') != checkDigit)
                return false;

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += (cpf[i] - '0') * (11 - i);

            rest = sum % 11;
            int secondCheckDigit = rest < 2 ? 0 : 11 - rest;

            if ((cpf[10] - '0') != secondCheckDigit)
                return false;

            return true;
        }

        public static string FormatCpf(this string cpf)
        {
            string formattedCpf = Regex.Replace(cpf, @"[\D]", string.Empty);

            return formattedCpf;
        }

        public static bool IsEmailValid(this string email)
        {
            if (email is null)
                return false;

            var ValidatedEmail = Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

            return ValidatedEmail;
        }
    }
}
