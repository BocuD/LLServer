using LLServer.Database;
using LLServer.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Controllers.Nesys;

[ApiController]
[Route("service/card")]
public class CardController : ControllerBase
{
    private readonly ApplicationDbContext dbContext;

    public CardController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpPost("cardn.cgi")]
    public async Task<IActionResult> Card([FromForm(Name = "card_no")] string cardNo,
        [FromForm(Name = "cmd_str")]                                   int    command)
    {
        switch (command)
        {
            case CardCommandCodes.READ:
            {
                var card = await dbContext.Users.FirstOrDefaultAsync(user => user.CardId == cardNo);
                return Ok(card is null ? $"{CardReturnCodes.NOT_REGISTERED}" : $"{CardReturnCodes.OK}\n1,1\n{cardNo}");
            }
            case CardCommandCodes.REISSUE:
                return Ok($"{CardReturnCodes.NOT_REISSUE}");
            case CardCommandCodes.REGISTER:
                var exists = await dbContext.Users.AnyAsync(user1 => user1.CardId == cardNo);
                if (exists)
                {
                    return Ok($"{CardReturnCodes.ERROR}");
                }

                var user = new User
                {
                    CardId = cardNo
                };
                dbContext.Add(user);
                await dbContext.SaveChangesAsync();
                return Ok($"{CardReturnCodes.OK}\n1,1\n{cardNo}");
            default:
                throw new ArgumentOutOfRangeException(nameof(command));
        }
    }
}

internal static class CardCommandCodes
{
    public const int READ = 256;

    public const int REGISTER = 512;

    public const int REISSUE = 1536;
}

internal static class CardReturnCodes
{
    public const int OK = 1;

    public const int NOT_REGISTERED = 23;

    public const int NOT_REISSUE = 27;

    public const int ERROR = 9999;
}