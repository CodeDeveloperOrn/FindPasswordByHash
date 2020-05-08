using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FindPasswordByHash
{
    public class Hasher
    {
        public TypeHashAlgoritm typeHashFunction { get; set; }
        private MD5 mD5 { get; set; }
        private SHA256 hA256 { get; set; }
        private SHA512 hA512 { get; set; }

        public Hasher(TypeHashAlgoritm type)
        {
            this.typeHashFunction = type;

            switch (type)
            {
                case TypeHashAlgoritm.MD5: this.mD5 = new MD5CryptoServiceProvider(); break;
                case TypeHashAlgoritm.SHA256: this.hA256 = new SHA256CryptoServiceProvider(); break;
                case TypeHashAlgoritm.SHA512: this.hA512 = new SHA512CryptoServiceProvider(); break;
                default: break;
            }
        }

        /// <summary>
        /// Вычисление хеша
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns>массив с вычисленным хешем</returns>
        public byte[] ComputeHash(byte[] buffer)
        {
            switch (this.typeHashFunction)
            {
                case TypeHashAlgoritm.MD5: return this.mD5.ComputeHash(buffer); break;
                case TypeHashAlgoritm.SHA256: return this.hA256.ComputeHash(buffer); break;
                case TypeHashAlgoritm.SHA512: return this.hA512.ComputeHash(buffer); break;
                default: return null;
            }
        }
    }
}
