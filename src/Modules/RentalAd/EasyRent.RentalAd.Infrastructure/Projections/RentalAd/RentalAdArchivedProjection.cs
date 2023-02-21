using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.States;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdArchivedProjection : DefaultProjection<RentalAdArchived>
{
    public RentalAdArchivedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdArchived @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAd = await documentRepository.FindAsync(r
            => r.RentalAdId == @event.RentalAdId.ToString());

        if (rentalAd is not null)
        {
            rentalAd.State = RentalAdState.Archived.ToString();
            await documentRepository.ReplaceAsync(rentalAd);

            Logger.Trace("> Renal Ad with ID: #{RentalAdId} has been deleted from document store.", rentalAd.RentalAdId);
        }
    }
}