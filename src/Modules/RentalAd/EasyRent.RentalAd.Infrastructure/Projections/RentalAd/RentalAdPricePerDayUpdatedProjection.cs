using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdPricePerDayUpdatedProjection : DefaultProjection<RentalAdPricePerDayUpdated>
{
    public RentalAdPricePerDayUpdatedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdPricePerDayUpdated @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAdDocument = await documentRepository.FindAsync(r
            => r.RentalAdId == @event.RentalAdId.ToString());

        if (rentalAdDocument is not null)
        {
            rentalAdDocument.PricePerDay = @event.PricePerDay;

            await documentRepository.ReplaceAsync(rentalAdDocument);

            Logger.Trace("> Renal Ad price per day with ID: #{RentalAdId} updated in document store.", rentalAdDocument.RentalAdId);
        }
    }
}