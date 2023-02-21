using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdPlaceOwnerDeletedProjection : DefaultProjection<RentalAdPlaceOwnerDeleted>
{
    public RentalAdPlaceOwnerDeletedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdPlaceOwnerDeleted @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAd = await documentRepository.FindAsync(r => r.RentalAdId == @event.RentalAdId.ToString());

        if (rentalAd is not null)
        {
            await documentRepository.DeleteAsync(rentalAd);

            Logger.Trace("> Renal Ad with ID: #{RentalAdId} has been deleted from document store.", rentalAd.RentalAdId);
        }
    }
}