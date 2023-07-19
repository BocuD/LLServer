using LLServer.Database.Models;
using LLServer.Models.UserData;
using Riok.Mapperly.Abstractions;

namespace LLServer.Mappers;

[Mapper]
public partial class LiveDataMapper
{
    public partial LiveData FromPersistentLiveData(PersistentLiveData input);
}