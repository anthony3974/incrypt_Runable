using System;

// Version 1.1

class Incrypt
{
    readonly private char[] inAlpha = new char[] { '\r', '\n', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', ' ', '.', ',', '\'', '\"', '!', '@', '#', '$', '%', '^', '&', '<', '>', '?', '/', '*', '-', '+', '=', '_', '(',')',';', ':' };

    readonly string[] outAlpha;
    public Incrypt()
    {
        // init outAlpha
        outAlpha = new string[inAlpha.Length];
        for (int i = 0; i < inAlpha.Length; i++) outAlpha[i] = (i + 10).ToString();
    }
    public string Encrypt(string text)
    {
        string R = "";
        for (int i = 0; i < text.Length; i++)
        {
            try { R += outAlpha[Array.IndexOf(inAlpha, text[i])]; }
            catch (IndexOutOfRangeException) { }
        }
        return R;
    }
    public string EncryptLevel2(string text)
    {
        string R = "";
        string S = Encrypt(text);
        for (int i = 0; i < S.Length; i++) R += ((int.Parse(S[i].ToString()) / 2).ToString() + (int.Parse(S[i].ToString()) % 2).ToString());
        return R;
    }
    public string Decrypt(string text)
    {
        string R = "";
        for (int i = 0; i < text.Length; i += 2)
        {
            string txt = text[i].ToString() + text[i + 1].ToString();
            int num = Array.IndexOf(outAlpha, txt);
            R += inAlpha[num];
        }
        return R;
    }
    public string DecryptLevel2(string text)
    {
        string R = "";
        string S = text;
        for (int i = 0; i < S.Length; i += 2) R += ((int.Parse(S[i].ToString()) * 2) + int.Parse(S[i + 1].ToString())).ToString();
        return Decrypt(R);
    }
}
