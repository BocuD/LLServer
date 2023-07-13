using LLServer.Models;

namespace LLServer.Handlers;

public class InformationHandler
{
    public static ResponseContainer Handle(string serveripaddress)
    {
        return new ResponseContainer
        {
            Result = 200,
            Response = new InformationResponse
            {
                BaseUrl = $"http://{serveripaddress}/game",
                EncoreExpirationDate = (DateTime.Today + TimeSpan.FromDays(3650)).ToString("yyyy-MM-dd")
            }
        };
    }
}