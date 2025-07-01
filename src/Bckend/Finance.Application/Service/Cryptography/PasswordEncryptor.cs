using System.Security.Cryptography;
using System.Text;


namespace Finance.Application.Service.Cryptography;
public class PasswordEncryptor {
    
    private readonly string _Encryptionkey;

    public PasswordEncryptor(string Encryptionkey) {
        _Encryptionkey = Encryptionkey;
    }
    public string Encrypt(string password) {
        var senhaChaveEnctiptacao = $"{password}{_Encryptionkey}";


        var bytes = Encoding.UTF8.GetBytes(password);
        var sha512 = SHA512.Create();
        byte[] hash = sha512.ComputeHash(bytes);
        return StringBytes(hash);
    }

    public static string StringBytes(byte[] bytes) {
        var sb = new StringBuilder();
        foreach (byte b in bytes) {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        return sb.ToString();
    }
}
