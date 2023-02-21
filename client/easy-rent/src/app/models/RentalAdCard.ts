export interface RentalAdCard {
  rentalAdId: string;
  mainPhoto: string;
  title: string;
  placeAddress: PlaceAddress;
}

export interface PlaceAddress {
    country: string;
    city: string;
    street: string;
  }
