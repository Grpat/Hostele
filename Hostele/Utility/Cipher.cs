using System.Text;

namespace Hostele.Utility;

public class Cipher
{
    public String Location { get; set; }

    public Cipher()
    {
        Location = Directory.GetCurrentDirectory();
    }

    public static void GenerateCipher(String input)
    {
        var cipher = new Cipher();
        var key = new Random().Next(1, 26);
        var output = cipher.Encipher(input, key);
        var keyPath = Path.Combine(cipher.Location, "key.txt");
        var outputPath = Path.Combine(cipher.Location, "output.txt");
        
        using FileStream fs1 = File.Create(keyPath);
        using FileStream fs2 = File.Create(outputPath);
        cipher.AddText(fs1, key.ToString());
        cipher.AddText(fs2, output);
    }

    public static bool Decode(String cipherString, int key)
    {
        var cipher = new Cipher();
        var keyPath = Path.Combine(cipher.Location, "key.txt");
        var outputPath = Path.Combine(cipher.Location, "output.txt");
        
        using FileStream fs1 = File.OpenRead(keyPath);
        using FileStream fs2 = File.OpenRead(outputPath);

        var keyFromFile = int.Parse(File.ReadAllText(keyPath));
        var outputFromFile = File.ReadAllText(outputPath);

        var message = cipher.Decipher(outputFromFile, keyFromFile);
        
        return cipher.Decipher(cipherString, key) == message ? true : false;
    }
    
    private char CesarCipher(char ch, int key)
    {
        if (!char.IsLetter(ch))
            return ch;

        char offset = char.IsUpper(ch) ? 'A' : 'a';
        return (char)((((ch + key) - offset) % 26) + offset);
    }
    
    public string Encipher(string input, int key)
    {
        string output = string.Empty;

        foreach (char ch in input)
            output += CesarCipher(ch, key);

        return output;
    }

    public string Decipher(string input, int key)
    {
        return Encipher(input, 26 - key);
    }
    
    private void AddText(FileStream fs, string value)
    {
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        fs.Write(info, 0, info.Length);
    }
}