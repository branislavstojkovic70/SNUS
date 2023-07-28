using System.ComponentModel;
using System.Text;

namespace SNUS_DRIVER;
public class DataSender
{
    public async Task SendDataAsync()
    {
        var dataToSend = new
        {
            Username = "username",
            Password = "password",
        };

        var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dataToSend);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.PostAsync("https://localhost:8081/api/User/new", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Data sent successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to send data. Status code: {response.StatusCode}");
            }
        }
    }
}
