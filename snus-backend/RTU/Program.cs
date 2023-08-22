class Program
{
    static async Task Main(string[] args)
    {
        using (var httpClient = new HttpClient())
        {
            var timer = new System.Threading.Timer(
                async _ =>
                {
                    Random random = new Random();
                    int id = random.Next(1, 4);
                    int value = (int)ReturnValue();
                    int aiOrDi = random.Next(1, 3);
                    string path = "https://localhost:8081/api/Tag/changeValue/" + id + "/" + value + "/" + aiOrDi;
                    var response = await httpClient.PostAsync(path, null);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Data sent successfully on: "+path);
                    }
                    else
                    {
                        Console.WriteLine($"Failed to send data on: " + path + $". Status code: {response.StatusCode}");
                    }
                },
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(60));
            
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

            timer.Dispose();
        }
    }
    
    public static double ReturnValue()
    {
        Random r = new Random();
        int choice = r.Next(1, 4);
        switch (choice)
        {
            case 1:
                return Sine();
                break;
            case 2: 
                return Cosine();
                break;
            case 3:
                return Ramp();
                break;
            default:
                return 1000;
        }
    }

    private static double Sine()
    {
        return 100 * Math.Sin((double)DateTime.Now.Second / 60 * Math.PI);
    }

    private static double Cosine()
    {
        return 100 * Math.Cos((double)DateTime.Now.Second / 60 * Math.PI);
    }

    private static double Ramp()
    {
        return 100 * DateTime.Now.Second / 60;
    }
}
