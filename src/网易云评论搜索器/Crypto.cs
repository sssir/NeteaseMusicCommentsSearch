using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Linq;
using System.Numerics;
using System.Collections.Specialized;

namespace 网易云评论搜索器
{
    public static class Crypto
    {
        private const string modulus = "00e0b509f6259df8642dbc35662901477df22677ec152b5ff68ace615bb7b725152b3ab17a876aea8a5aa76d2e417629ec4ee341f56135fccf695280104e0312ecbda92557c93870114af6c9d05c4f7f0c3685b7a46bee255932575cce10b424d813cfe4875d3e82047b97ddef52741d546b8e289dc6935b3ece0462db0a22b8e7";
        private const string nonce = "0CoJUm6Qyw8W8jud";
        private const string pubKey = "010001";

        private static string CreateSecretKey(int size)
        {
            var r = new Random();
            const string keys = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var key = string.Empty;
            for (int i = 0; i < size; i++)
            {
                var pos = r.Next(0, keys.Length);
                key = key + keys[pos];
            }
            return key;
        }


        private static string AesEncrypt(string plaintextData, string key, string iv = "0102030405060708")
        {
            if (string.IsNullOrEmpty(plaintextData)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(plaintextData);
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                IV = Encoding.UTF8.GetBytes(iv),
                Mode = CipherMode.CBC
            };
            ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);

        }

        private static string RsaEncrypt(string text, string pubKey = pubKey, string modulus = modulus)
        {
            text = new String(text.Reverse().ToArray());
            string hex = String.Concat(text.Select(x => ((int)x).ToString("x")));
            var biText = BigInteger.Parse(hex, System.Globalization.NumberStyles.AllowHexSpecifier);
            var biEx = BigInteger.Parse(pubKey, System.Globalization.NumberStyles.AllowHexSpecifier);
            var biMod = BigInteger.Parse(modulus, System.Globalization.NumberStyles.AllowHexSpecifier);
            var biRet = BigInteger.ModPow(biText, biEx, biMod);
            var str = biRet.ToString("x");
            str = str[0] == '0' ? str.Remove(0, 1) : str;
            while (str.Length < 256) str = "0" + str;
            return str;
        }

        private static FormParams Encrypt(object obj)
        {
            var text = JsonConvert.SerializeObject(obj);
            var secKey = CreateSecretKey(16);
            var encText = AesEncrypt(AesEncrypt(text, nonce), secKey);
            var encSecKey = RsaEncrypt(secKey, pubKey, modulus);
            return new FormParams()
            {
                @params = encText,
                encSecKey = encSecKey
            };
        }

        public static string GetRawForm(object data)
        {
            var encrypt = Encrypt(data);
            return $"{nameof(encrypt.@params)}={encrypt.@params}&{encrypt.encSecKey}={encrypt.encSecKey}";
        }

        public static NameValueCollection GetNameValue(object data)
        {
            var encrypt = Encrypt(data);
            return new NameValueCollection()
            {
                [nameof(encrypt.@params)] = encrypt.@params,
                [nameof(encrypt.encSecKey)] = encrypt.encSecKey
            };
        }

        public class FormParams
        {
            public string @params { get; set; }
            public string encSecKey { get; set; }
        }
    }
}
