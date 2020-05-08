using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FindPasswordByHash
{
    /// <summary>
    /// Обход паролей
    /// </summary>
    public class SearcherPass
    {
        private List<byte> finderMass { get; set; }

        public SearcherPass()
        {
            this.finderMass = new List<byte>() { 0 };
        }

        /// <summary>
        /// Получение строки из массива байтов в текущем состоянии
        /// </summary>
        /// <returns>Результирующая строка</returns>
        public string GetString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in this.finderMass.ToArray().Reverse().ToArray())
            {
                stringBuilder.Append(c);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Инкрементация значения
        /// </summary>
        public void IncrementByteArray()
        {
            int index = 0;
            int couuntArray = Instrument.symbols.Length;
            bool isNeddAdd = false;

            while (true)
            {
                if (index == this.finderMass.Count)
                {
                    isNeddAdd = true;
                    break;
                }

                if (this.finderMass[index] == couuntArray - 1)
                {
                    this.finderMass[index] = 0;
                    index++;
                    continue;
                }
                else
                {
                    this.finderMass[index]++;
                    break;
                }
            }

            if (isNeddAdd)
            {
                this.finderMass.Add(0);
            }
        }

        /// <summary>
        /// Получение генерируемого массива байтов
        /// </summary>
        /// <returns>Массив байтов</returns>
        public byte[] GetByteArray()
        {
            return this.finderMass.ToArray().Reverse().ToArray();
        }
    }

    public enum TypeHashAlgoritm
    {
        MD5,
        SHA512,
        SHA256
    }

    public static class Instrument
    {
        public static char[] symbols = new char[] {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            ' ', '_', '$', '@'
        };

        public static void IncrementString(ref byte[] mass)
        {
            int i = symbols.Length - 1;

            while (true)
            {
                if (mass[i] == symbols.Length - 1)
                {
                    mass[i] = 0;
                }
                else
                {
                    mass[i]++;
                    break;
                }

                i--;
            }
        }
    }
}
