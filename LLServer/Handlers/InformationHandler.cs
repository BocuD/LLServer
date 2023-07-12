using LLServer.Models;

namespace LLServer.Handlers;

public class InformationHandler
{
    public static ResponseContainer Handle()
    {
        return new ResponseContainer
        {
            Result = 200,
            Response = new InformationResponse
            {
                BaseUrl = "http://127.0.0.1/game",
                EncoreExpirationDate = (DateTime.Today + TimeSpan.FromDays(3650)).ToString("yyyy-MM-dd")
            }
        };
    }
}