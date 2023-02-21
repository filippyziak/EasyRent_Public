import { IdentityService } from './../../../../services/identity.service';
import { ReservationService } from './../../../../services/reservation.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { GalleryItem, ImageItem } from 'ng-gallery';
import { RentalAdDetails } from 'src/app/models/RentalAdDetails';

@Component({
  selector: 'app-rentalAd-card',
  templateUrl: './rentalAd-card.component.html',
  styleUrls: ['./rentalAd-card.component.scss'],
})
export class RentalAdCardComponent implements OnInit {
  arrivalDate!: FormGroup;
  departureDate!: FormGroup;
  minDate!: Date;
  images: GalleryItem[] = [];
  rentalAd: any;
  placeReservations: any;
  reviewedPlaceReservations: any;

  dateFilter: (date: Date | null) => boolean = (date: Date | null) => {
    if (!date) {
      return false;
    }
    const day = date.getDay();
    return day == 1; // 1 means monday, 0 means sunday, etc.
  };

  myFilter: (d: Date | null) => boolean = (d: Date | null) => {
    if (!d) {
      return false;
    }
    const day = d.getDay();
    const blockedDates = this.rentalAd.reservedDates.map((d) =>
      new Date(d).valueOf()
    );
    return !blockedDates.includes(d.valueOf());
  };

  constructor(
    public route: ActivatedRoute,
    private reservationService: ReservationService,
    private identityService: IdentityService,
    private router: Router
  ) {
    this.arrivalDate = new FormGroup({
      arrivalDate: new FormControl(Validators.required),
    });
    this.departureDate = new FormGroup({
      departureDate: new FormControl(Validators.required),
    });
    this.minDate = new Date();
    this.minDate.setDate(this.minDate.getDate() + 1);
  }

  get departureDateProp() { return this.arrivalDate.get('arrivalDate'); }
  get arrivalDateProp() { return this.departureDate.get('departureDate'); }

  public reserve() {
    this.reservationService.reserve(
      this.rentalAd.rentalAdId,
      this.rentalAd.placeOwner.placeOwnerId,
      this.arrivalDate.value.arrivalDate,
      this.departureDate.value.departureDate
    );
  }

  public isOwner() {
    return (this.identityService.isLoggedIn() &&
      this.identityService.getAccountId() ==
      this.rentalAd.placeOwner.placeOwnerId
    );
  }

  public isArchived(){
    return this.rentalAd.state == 'Archived';
  }

  public isTenant() {
    return (this.identityService.isLoggedIn() &&
      this.identityService.isTenant()
    );
  }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.rentalAd = data.rentalAdResolver.rentalAd;
      this.placeReservations = data.reservationsResolver.placeReservations;
      this.reviewedPlaceReservations =
        data.reservationsResolver.placeReservations.filter(
          (r) => r.state == 'Reviewed'
        );


          console.log(this.rentalAd)

      if (Array.isArray(this.rentalAd.placePictures)) {
        this.images.push(
          new ImageItem({
            src: this.rentalAd?.mainPlacePicture?.pictureUrl,
            thumb: this.rentalAd?.mainPlacePicture?.pictureUrl,
          })
        );

        this.rentalAd.placePictures.forEach((p) => {
          if (p.placePictureId !== this.rentalAd.mainPlacePicture.placePictureId) {
            this.images.push(
              new ImageItem({ src: p?.pictureUrl, thumb: p?.pictureUrl })
            );
          }
        });
      }
    });
  }
}
