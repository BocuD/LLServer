using System.Text;
using System.Text.Json;
using LLServer.Handlers;
using LLServer.Models;
using LLServer.Models.Requests;
using LLServer.Models.UserDataModel;
using Microsoft.AspNetCore.Mvc;

namespace LLServer.Controllers.Game;

[ApiController]
[Route("game")]
public class GameController : BaseController<GameController>
{
    public static UserDataContainer userDataContainer = UserDataContainer.GetDummyUserDataContainer();

    [HttpPost]
    public async Task<ActionResult<ResponseContainer>> BaseHandler()
    {
        var body = await Request.BodyReader.ReadAsync();
        var bodyString = Encoding.Default.GetString(body.Buffer).Replace("\0", string.Empty);

        // Parse body as json
        var request = JsonSerializer.Deserialize<RequestBase>(bodyString);
        if (request is null)
        {
            Logger.LogWarning("Request deserialize failed");
            return BadRequest();
        }
        
        Logger.LogInformation("Protocol: {Protocol}\nBody {Body}", request.Protocol, bodyString);

        ResponseContainer response;
        switch (request.Protocol)
        {
            case "unlock":
                response = new ResponseContainer
                {
                    Result = 200,
                    Response = new ResponseBase()
                };
                break;
            case "gameconfig":
                response = new ResponseContainer
                {
                    Result = 200,
                    Response = new ResponseBase()
                };
                break;
            case "information":
                response = InformationHandler.Handle(Request.HttpContext.Connection.LocalIpAddress.ToString());
                break;
            case "auth":
                response = new ResponseContainer
                {
                    Result = 200,
                    Response = new AuthResponse
                    {
                        AbnormalEnd = 0,
                        BlockSequence = 1,
                        Name = "",
                        SessionKey = "12345678901234567890123456789012",
                        Status = 0,
                        UserId = "1"
                    }
                };
                break;
            case "userdata.initialize":
            {
                if (request.Param == null) return BadRequest();
             
                //deserialize from param
                string paramJson = request.Param.Value.GetRawText();
                
                InitializeUserData initializeUserData = JsonSerializer.Deserialize<InitializeUserData>(paramJson);
                
                userDataContainer.InitializeUserData(initializeUserData);
                
                Logger.LogInformation("InitializeUserData {InitializeUserData}", JsonSerializer.Serialize(initializeUserData));
                Logger.LogInformation("UserDataContainer {UserDataContainer}", JsonSerializer.Serialize(userDataContainer));
                
                response = new ResponseContainer
                {
                    Result = 200,
                    Response = userDataContainer.GetUserData()
                };
            }
                break;

            #warning double check that gameentry actually expects the same response as userdata.get
            case "gameentry":
            case "userdata.get":
                response = new ResponseContainer()
                {
                    Result = 200,
                    Response = userDataContainer.GetUserData()
                };

                //log response json
                Logger.LogInformation("Response {Response}", JsonSerializer.Serialize(response));
                break;

            case "userdata.set":
            {
                if (request.Param == null) return BadRequest();
                
                string paramJson = request.Param.Value.GetRawText();
                
                Logger.LogInformation(paramJson);
                
                SetUserData setUserData = JsonSerializer.Deserialize<SetUserData>(paramJson);

                userDataContainer.SetUserData(setUserData);
                
                //log setuserdata json and userdatacontainer json
                Logger.LogInformation("SetUserData {SetUserData}", JsonSerializer.Serialize(setUserData));
                Logger.LogInformation("UserDataContainer {UserDataContainer}", JsonSerializer.Serialize(userDataContainer));

                response = new ResponseContainer()
                {
                    Result = 200,
                    Response = new ResponseBase()
                };
            }
                break;
            case "checkword":
                response = new ResponseContainer()
                {
                    Result = 200,
                    Response = new ResponseBase()
                };
                break;
            
            case "ranking":
                response = new ResponseContainer()    
                {
                    Result = 200,
                    Response = RankingResponse.DummyRankingResponse()
                };
                break;
            
            default:
                Logger.LogWarning("Unhandled protocol: {Protocol}", request.Protocol);
                response = new ResponseContainer
                {
                    Result = 200,
                    Response = new ResponseBase()
                };
                break;
        }

        return Ok(response);
    }
}