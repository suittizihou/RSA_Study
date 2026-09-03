namespace RSA_Test;

using RSA_Study;

public class RSATest
{
    /// <summary>
    /// m の値が 0 <= m < n なので成功するパターン
    /// </summary>
    [Fact]
    public void DenryptoAndDecrypt_SuccessCase()
    {
        int p = 11;
        int q = 5;

        int n = p * q;
        int e = 3;
        int d = 27;

        int m = 7;

        var publicKey = new PublicKey(n, e);
        var privateKey = new PrivateKey(n, d);

        int c = publicKey.Encryption(m);
        int ans = privateKey.Decrypt(c);

        Assert.Equal(m, ans);
    }

    /// <summary>
    /// m の値が 0 <= m < n ではないので失敗するパターン
    /// </summary>
    [Fact]
    public void DenryptoAndDecrypt_FailedCase()
    {
        int p = 11;
        int q = 5;

        int n = p * q;
        int e = 3;
        int d = 27;

        int m = 55;

        var publicKey = new PublicKey(n, e);
        var privateKey = new PrivateKey(n, d);

        int c = publicKey.Encryption(m);
        int ans = privateKey.Decrypt(c);

        Assert.Equal(m, ans);
    }
}
