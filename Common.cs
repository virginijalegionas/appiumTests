public class Common
{
    public static string GenerateRandom()
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    
    public static void Wait(int waitSeconds)
    {
        Thread.Sleep(waitSeconds * 1000);
    }
}