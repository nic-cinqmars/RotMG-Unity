using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Encodings;
using System.IO;
using UnityEngine;
using System;

namespace RotmgClient.Cryptography
{
    public class RSA
    {
        private static readonly string RSA_PUBLIC_KEY =
            "-----BEGIN PUBLIC KEY-----\n" +
            "MFswDQYJKoZIhvcNAQEBBQADSgAwRwJAeyjMOLhcK4o2AnFRhn8vPteUy5Fux/cX" +
            "N/J+wT/zYIEUINo02frn+Kyxx0RIXJ3CvaHkwmueVL8ytfqo8Ol/OwIDAQAB\n" +
            "-----END PUBLIC KEY-----";

        private static RSA? instance;

        private RsaEngine? engine;
        private AsymmetricKeyParameter? key;

        private RSA()
        {
            StringReader stringReader = new StringReader(RSA_PUBLIC_KEY.Trim());
            PemReader pemReader = new PemReader(stringReader);
            key = pemReader.ReadObject() as AsymmetricKeyParameter;
            if (key != null)
            {
                engine = new RsaEngine();
                engine.Init(true, key);
            }
            else
            {
                Debug.Log("Fatal error in RSA.cs");
            }

        }

        public static RSA getInstance()
        {
            if (instance == null)
            {
                instance = new RSA();
            }

            return instance;
        }

        public string Encrypt(string dataStr)
        {
            if (string.IsNullOrEmpty(dataStr) || engine == null || key == null)
                return "";
          
            byte[] data = Encoding.UTF8.GetBytes(dataStr);
            Pkcs1Encoding encoding = new Pkcs1Encoding(engine);
            encoding.Init(true, key);

            return Convert.ToBase64String(encoding.ProcessBlock(data, 0, data.Length));
        }
    }
}
