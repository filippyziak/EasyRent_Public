import { RentalAdService } from './../../../../services/rental-ad.service';
import { RentalAdCard } from './../../../../models/RentalAdCard';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-rentalAd-browser',
  templateUrl: './rentalAd-browser.component.html',
  styleUrls: ['./rentalAd-browser.component.scss'],
})
export class RentalAdBrowserComponent implements OnInit {
  arrivalDate: FormGroup;
  departureDate: FormGroup;
  minDate: Date;
  rentalAds: RentalAdCard[];
  pageNumber: number;
  totalPages: number;
  city: string = '';
  country: string  = '';

  constructor(
    private route: ActivatedRoute,
    public rentalAdService: RentalAdService
  ) {
    this.arrivalDate = new FormGroup({
      arrivalDate: new FormControl(),
    });
    this.departureDate = new FormGroup({
      departureDate: new FormControl(),
    });
    this.minDate = new Date();
  }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.rentalAds = data.rentalAdsResolver.rentalAds;
      this.totalPages = data.rentalAdsResolver.totalPages;
      console.log(data.rentalAdsResolver)
    });
    this.pageNumber = 1;
  }

  previousPage() {
    if (this.pageNumber > 1) {
      this.pageNumber--;

      this.rentalAdService
        .getFilteredRentalAdCard(
          this.city,
          this.country,
          this.arrivalDate.value.arrivalDate,
          this.departureDate.value.departureDate,
          this.pageNumber
        )
        .subscribe((res) => {
          this.rentalAds = res.rentalAds;
          this.totalPages = res.totalPages;
        });
        console.log(this.pageNumber);

    }
  }

  nextPage() {
    if (this.pageNumber < this.totalPages) {
      this.pageNumber++;
      this.rentalAdService
        .getFilteredRentalAdCard(
          this.city,
          this.country,
          this.arrivalDate.value.arrivalDate,
          this.departureDate.value.departureDate,
          this.pageNumber
        )
        .subscribe((res) => {
          this.rentalAds = res.rentalAds;
          this.totalPages = res.totalPages;
        });
    }
  }

  lastPage() {
    this.pageNumber = this.totalPages;

    this.rentalAdService
      .getFilteredRentalAdCard(
        this.city,
        this.country,
        this.arrivalDate.value.arrivalDate,
        this.departureDate.value.departureDate,
        this.pageNumber
      )
      .subscribe((res) => {
        this.rentalAds = res.rentalAds;
        this.totalPages = res.totalPages;
      });
  }

  firstPage() {
    this.pageNumber = 1;
    this.rentalAdService
      .getFilteredRentalAdCard(
        this.city,
        this.country,
        this.arrivalDate.value.arrivalDate,
        this.departureDate.value.departureDate,
        this.pageNumber
      )
      .subscribe((res) => {
        this.rentalAds = res.rentalAds;
        this.totalPages = res.totalPages;
      });
  }

  moveToPage(pageNumber: number) {
    if (pageNumber <= this.totalPages) {
      this.pageNumber = pageNumber;
      this.rentalAdService
        .getFilteredRentalAdCard(
          this.city,
          this.country,
          this.arrivalDate.value.arrivalDate,
          this.departureDate.value.departureDate,
          this.pageNumber
        )
        .subscribe((res) => {
          this.rentalAds = res.rentalAds;
          this.totalPages = res.totalPages;
        });
    }
  }

  search(addressData: NgForm) {
    this.city = addressData.value.city;
    this.country = addressData.value.country;

    this.rentalAdService
      .getFilteredRentalAdCard(
        this.city,
        this.country,
        this.arrivalDate.value.arrivalDate,
        this.departureDate.value.departureDate,
        this.pageNumber
      )
      .subscribe((res) => {
        this.rentalAds = res.rentalAds;
        this.pageNumber = 1;
        this.totalPages = res.totalPages;
      });
  }
}
