using LLServer.Models.Responses;
using LLServer.Models.UserData;
using Riok.Mapperly.Abstractions;

namespace LLServer.Mappers;

[Mapper]
public partial class UserDataResponseMapper
{
    public partial UserDataResponse UserDataToUserDataResponse(UserDataContainer input);
}