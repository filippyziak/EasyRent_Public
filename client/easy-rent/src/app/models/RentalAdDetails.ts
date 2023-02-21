export interface RentalAdDetails {
  rentalAdId: string;
  mainPhoto: string;
  title: string;
  description: string;
  placeAddress: PlaceAddress;
  pricePerDay: number;
  placeOwner: PlaceOwner;
  reservedDates: Date[];
  placePictures: PlacePicture[];
}

export interface PlaceAddress {
  country: string;
  city: string;
  street: string;
}

export interface PlaceOwner {
  emailAddress: string;
}

export interface PlacePicture{
    pictureUrl: string;
}
