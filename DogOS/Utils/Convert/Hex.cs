namespace DogOS.Utils.Convert
{
    public class Hex
    {
        // Based on https://stackoverflow.com/a/14333437/13617487
        public static string BytesToHex(byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];
            int b;

            for (int i = 0; i < bytes.Length; i++)
            {
                b = bytes[i] >> 4;
                c[i * 2] = (char)(55 + b + (((b - 10) >> 31) & -7));

                b = bytes[i] & 0xF;
                c[i * 2 + 1] = (char)(55 + b + (((b - 10) >> 31) & -7));
            }

            return new string(c);
        }
    }
}