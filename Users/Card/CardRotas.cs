using BackendSharp.Users;
using Microsoft.EntityFrameworkCore;

namespace BackendSharp.Cards
{
    public static class CardRotas
    {
        public static void AddRotasCard(this WebApplication app)
        {
            var rotasCard = app.MapGroup("card");

            // POST Card
            rotasCard.MapPost("register", async (AddCardRequest request, AppDbContext context, CancellationToken ct) =>
            {
                var user = await context.Users.SingleOrDefaultAsync(u => u.Id == request.UserId, ct);
                if (user == null) return Results.NotFound("User not found");

                var newCard = new Card(request.NumberCard, request.CVC, request.Validity, request.PasswordCard, request.UserId);
                await context.Cards.AddAsync(newCard, ct);
                await context.SaveChangesAsync(ct);

                return Results.Ok(newCard);
            });

            // GET Cards by User
            rotasCard.MapGet("user/{userId:guid}", async (Guid userId, AppDbContext context, CancellationToken ct) =>
            {
                var cards = await context.Cards
                    .Where(c => c.UserId == userId)
                    .ToListAsync(ct);

                return Results.Ok(cards);
            });

            // PUT Card
            rotasCard.MapPut("{id:guid}", async (Guid id, UpdateCardRequest request, AppDbContext context, CancellationToken ct) =>
            {
                var card = await context.Cards.SingleOrDefaultAsync(c => c.Id == id, ct);
                if (card == null) return Results.NotFound("Card not found");

                card.AtualizarCartao(request.NumberCard, request.CVC, request.Validity, request.PasswordCard);
                await context.SaveChangesAsync(ct);

                return Results.Ok(card);
            });

            // DELETE Card
            rotasCard.MapDelete("{id:guid}", async (Guid id, AppDbContext context, CancellationToken ct) =>
            {
                var card = await context.Cards.SingleOrDefaultAsync(c => c.Id == id, ct);
                if (card == null) return Results.NotFound("Card not found");

                context.Cards.Remove(card);
                await context.SaveChangesAsync(ct);

                return Results.Ok();
            });
        }
    }
}
