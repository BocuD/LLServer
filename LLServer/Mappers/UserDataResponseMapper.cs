using LLServer.Models.Responses;
using LLServer.Models.UserData;
using Riok.Mapperly.Abstractions;

namespace LLServer.Mappers;

[Mapper]
public partial class UserDataResponseMapper
{
    public partial UserDataResponse FromUserData(UserDataContainer input);
    public partial UserDataResponse FromPersistentUserData(PersistentUserDataContainer input);
}