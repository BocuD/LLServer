using LLServer.Models.Responses;
using LLServer.Models.UserData;
using Riok.Mapperly.Abstractions;

namespace LLServer.Mappers;

[Mapper]
public partial class GameEntryResponseMapper
{
    public partial GameEntryResponse UserDataToGameEntryResponse(UserDataContainer input);
    public partial GameEntryResponse FromPersistentUserData(PersistentUserDataContainer input);
}