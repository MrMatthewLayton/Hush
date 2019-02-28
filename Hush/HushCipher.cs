namespace Hush
{
    using Core.Security.Cryptography;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;

    public sealed class HushCipher : IDisposable
    {
        private readonly ECDiffieHellmanCng diffieHellman;

        public HushCipher()
        {
            diffieHellman = new ECDiffieHellmanCng()
            {
                KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash,
                HashAlgorithm = CngAlgorithm.Sha256
            };

            PublicKey = PublicKey.FromByteArray(diffieHellman.PublicKey.ToByteArray());
        }

        public PublicKey PublicKey { get; }

        public byte[] Compute(byte[] data, PublicKey publicKey)
        {
            byte[] pad = GetOneTimePad(data.Length, publicKey);

            IEnumerable<byte> Compute()
            {
                for (int index = 0; index < data.Length; index++)
                {
                    yield return (byte)(data[index] ^ pad[index]);
                }
            }

            return Compute().ToArray();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (diffieHellman != null)
                {
                    diffieHellman.Dispose();
                }
            }
        }

        private byte[] GetOneTimePad(int length, PublicKey publicKey)
        {
            using (SHA3 hashAlgorithm = new Shake256Managed(length))
            {
                CngKey key = CngKey.Import(publicKey.ToByteArray(), CngKeyBlobFormat.EccPublicBlob);
                byte[] derivedKey = diffieHellman.DeriveKeyMaterial(key);

                return hashAlgorithm.ComputeHash(derivedKey);
            }
        }
    }
}
