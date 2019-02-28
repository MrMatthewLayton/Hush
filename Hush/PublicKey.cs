namespace Hush
{
    using System;
    using System.Linq;

    public sealed class PublicKey : IEquatable<PublicKey>
    {
        private readonly byte[] data;

        private PublicKey(byte[] data) => this.data = data;

        public static PublicKey FromByteArray(byte[] data) => new PublicKey(data);

        public static bool operator ==(PublicKey a, PublicKey b) => Equals(a, b);

        public static bool operator !=(PublicKey a, PublicKey b) => !Equals(a, b);

        public bool Equals(PublicKey other) => !(other is null) && data.SequenceEqual(other.data);

        public override bool Equals(object obj) => Equals(obj as PublicKey);

        public override int GetHashCode() => HashCode.Combine(data);

        public override string ToString() => BitConverter.ToString(data).Replace("-", string.Empty);

        public byte[] ToByteArray() => data;
    }
}
