using System;


namespace ARPGFrame
{
    public static class Encryptor
    {
        #region Methods

        public static string Encript(string text)
        {
            var result = String.Empty;
            Random random = new Random();
            int key = random.Next(10, 99);

            result += key.ToString();

            for (int i = 0; i < text.Length; i++)
            {
                if (i % 2 == 0)
                    result += (char)random.Next(3, 126);
                result += (char)(text[i] ^ key);
            }            
            return result;
        }

        public static string Decript(string text)
        {
            var k = String.Empty;
            k += text[0];
            k += text[1];
            var key = Int32.Parse(k);

            var result = String.Empty;
            for (int i = 2; i < text.Length; i++)
            {
                if (((i - 2) % 3) != 0)
                    result += (char)(text[i] ^ key);
            }            
            return result;
        }

        #endregion
    }
}
