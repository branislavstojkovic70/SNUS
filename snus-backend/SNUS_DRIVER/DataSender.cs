using System.ComponentModel;
using System.Text;
using System.Text.Json;

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

        using (var httpClient = new HttpClient())
        {

            // Serialize your data model to JSON
            var dataModelJson = JsonSerializer.Serialize(dataToSend);

            // Set the content type and make the POST request
            var content = new StringContent(dataModelJson, Encoding.UTF8, "application/json");
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
