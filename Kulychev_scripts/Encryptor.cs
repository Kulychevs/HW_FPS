using System;


namespace ARPGFrame
{
    public static class Encryptor
    {
        #region Fields

        private const string SUM_FORMAT = "D6";
        private const int SUM_LENGTH = 6;

        #endregion


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

            var sumString = EncryptSum(result);

            foreach(var ch in sumString)
                result += (char)(ch ^ key);

            return result;
        }

        public static bool Decript(string text, out string result)
        {
            result = String.Empty;
            var validText = text.Substring(0, text.Length - SUM_LENGTH);
            var key = Key(text);            

            if (DecryptSum(validText) == SumFromEncryption(text, key))
            {                              
                for (int i = 2; i < validText.Length; i++)
                {
                    if (((i - 2) % 3) != 0)
                        result += (char)(validText[i] ^ key);
                }
                return true;
            }
            else
                return false;
        }

        private static int Key(string text)
        {
            var key = text.Substring(0, 2);
            return Int32.Parse(key);
        }

        private static int SumFromEncryption(string text, int key)
        {
            var checker = text.Substring(text.Length - SUM_LENGTH, SUM_LENGTH);
            var decriptChecker = String.Empty;
            foreach (var ch in checker)
            {
                decriptChecker += (char)(ch ^ key);
            }
            return Int32.Parse(decriptChecker);
        }

        private static int DecryptSum(string text)
        {           
            int sum = 0;
            foreach (var ch in text)
                sum += (int)ch;
            return sum;
        }

        private static string EncryptSum(string result)
        {
            int sum = 0;
            foreach (var ch in result)
                sum += (int)ch;
            return sum.ToString(SUM_FORMAT);
        }

        #endregion
    }
}
