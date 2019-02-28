namespace Hush
{
    using System;
    using System.Text;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            HushCipher alice = new HushCipher();
            HushCipher bob = new HushCipher();

            string plainText = "The quick brown fox jumps over the lazy dog";
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] encryptedBytes = alice.Compute(plainTextBytes, bob.PublicKey);
            byte[] decryptedBytes = bob.Compute(encryptedBytes, alice.PublicKey);

            WriteLine("Original Message (Plain Text)", plainText);
            WriteLine("Original Message (Hexadecimal)", BitConverter.ToString(plainTextBytes).Replace("-", string.Empty));
            WriteLine("Encrypted Message (Hexadecimal)", BitConverter.ToString(encryptedBytes).Replace("-", string.Empty));
            WriteLine("Decrypted Message (Hexadecimal)", BitConverter.ToString(decryptedBytes).Replace("-", string.Empty));
            WriteLine("Decrypted Message (Plain Text)", Encoding.UTF8.GetString(decryptedBytes));

            Console.Read();
        }

        private static void WriteLine(string title, string text)
        {
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(title);

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(text);

            Console.WriteLine();
        }
    }
}
