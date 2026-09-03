namespace RSA_Study;

public static class MyMath
{
    /// <summary>
    /// 暗号・復号用の累乗計算
    /// 累乗の途中で毎回 n による余りを取り、値が巨大になって算術オーバーフローになるのを防ぐ
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int RSAPow(int a, int b, int n)
    {
        int ans = 1;
        for (int i = b; i > 0; i--)
        {
            ans = ans * a % n;
        }
        return ans;
    }
}

public class PrivateKey(int n, int d)
{
    private readonly int n = n;
    private readonly int d = d;

    /// <summary>
    /// 復号化
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public int Decrypt(int c)
    {
        return MyMath.RSAPow(c, d, n);
    }
}

public class PublicKey(int n, int e)
{
    private readonly int n = n;
    private readonly int e = e;

    /// <summary>
    /// 暗号化
    /// </summary>
    /// <param name="m"></param>
    /// <returns></returns>
    public int Encryption(int m)
    {
        return MyMath.RSAPow(m, e, n);
    }
}

class Program
{
    static void Main(string[] args)
    {
        int p = 11;
        int q = 5;
        
        int n = p * q;
        int nPhi = (p - 1) * (q - 1);
        int e = CalcE(nPhi);
        int d = CalcD(e, nPhi);

        var pubKey = new PublicKey(n, e);
        var priKey = new PrivateKey(n, d);

        int m = CreateMessage(n);
        System.Console.WriteLine($"m = {m}");
        int c = pubKey.Encryption(m);
        System.Console.WriteLine($"c = {c}");
        int Ans = priKey.Decrypt(c);
        System.Console.WriteLine($"Ans = {Ans}");
    }

    /// <summary>
    /// 暗号化したいメッセージを作成する
    /// メッセージは 0 <= m < n でなければならない
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    private static int CreateMessage(int n)
    {
        return Random.Shared.Next(0, n);
    }

    /// <summary>
    /// e * d を Φ(n) で割った余りが 1 になる d の値を求める
    /// </summary>
    /// <param name="e"></param>
    /// <param name="nPhi">Φ(n)</param>
    /// <returns></returns>
    private static int CalcD(int e, int nPhi)
    {
        int d = 1;
        for (; e * d % nPhi != 1; d++);
        return d;
    }

    /// <summary>
    /// Φ(n)と互いに素である数を最小値から順に求めていく
    /// 最初に見つかった値を e として採用する
    /// </summary>
    /// <param name="nPhi"></param>
    /// <returns></returns>
    private static int CalcE(int nPhi)
    {
        for(int i = 2; i < nPhi; i++)
        {
            if (Gcd(i, nPhi) == 1) return i;
        }
        return -1;
    }

    /// <summary>
    /// 最大公約数を求める
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private static int Gcd(int a, int b)
    {
        int mod = a % b;
        if (mod == 0) return b;
        return Gcd(b, mod);
    }
}