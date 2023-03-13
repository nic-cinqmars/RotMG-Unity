using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities.Encoders;

namespace RotmgClient.Cryptography
{
    public class RC4
    {
        private static RC4Engine rc4Send;
        private static RC4Engine rc4Recieve;

        private static RC4 instance;

        private RC4() 
        {
            rc4Send = new RC4Engine();
            rc4Send.Init(true, new KeyParameter(Hex.Decode("B1A5ED")));
            
            rc4Recieve = new RC4Engine();
            rc4Recieve.Init(true, new KeyParameter(Hex.Decode("612a806cac78114ba5013cb531")));
        }

        public static RC4 getInstance()
        {
            if (instance == null)
            {
                instance = new RC4();
            }
            return instance;
        }

        public void CryptSend(byte[] buf, int offset, int len)
        {
            rc4Send.ProcessBytes(buf, offset, len, buf, offset);
        }

        public void CryptRecieve(byte[] buf, int offset, int len)
        {
            rc4Recieve.ProcessBytes(buf, offset, len, buf, offset);
        }
    }
}
