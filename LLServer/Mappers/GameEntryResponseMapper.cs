using LLServer.Models.Responses;
using LLServer.Models.UserData;
using Riok.Mapperly.Abstractions;

namespace LLServer.Mappers;

//this will prevent modifications to the cloned data from affecting the input data
[Mapper(UseDeepCloning = true)]
public partial class GameEntryResponseMapper
{
    public partial GameEntryResponse UserDataToGameEntryResponse(UserDataContainer input);
    public partial GameEntryResponse FromPersistentUserData(PersistentUserDataContainer input);
}