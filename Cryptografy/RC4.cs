using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cryptografy
{
    public class RC4
    {
        byte[] S = new byte[256];
        int x = 0;
        int y = 0;

        //для удобства обмена
        private void Swap(byte[] array, int index1, int index2)
        {
            byte temp     = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }
        
        // Алгоритм ключевого расписания
        private void init(byte[] key)
        {
            int keyLength = key.Length;

            for (int i = 0; i < 256; i++)
            {
                S[i] = (byte)i;
            }

            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + key[i % keyLength]) % 256;
                Swap(S, i, j);
            }
        }

        public RC4(byte[] key)
        {
            init(key);
        }

        // Pseudo-Random Generation Algorithm 
        // Генератор псевдослучайной последовательности 
        private byte keyItem()
        {
            x = (x + 1) % 256;
            y = (y + S[x]) % 256;

            Swap(S, x, y);

            return S[(S[x] + S[y]) % 256];
        }

        //функция шифрования/расшифрования
        public byte[] Encode(byte[] dataB, int size)
        {
            byte[] data   = dataB.Take(size).ToArray();
            byte[] cipher = new byte[data.Length];

            for (int m = 0; m < data.Length; m++)
            {
                cipher[m] = (byte)(data[m] ^ keyItem());
            }

            return cipher;
        }
    }
}
