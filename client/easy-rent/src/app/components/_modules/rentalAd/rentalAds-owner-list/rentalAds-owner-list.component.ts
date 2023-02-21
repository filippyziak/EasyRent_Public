import { RentalAdService } from './../../../../services/rental-ad.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RentalAdCard } from 'src/app/models/RentalAdCard';

@Component({
  selector: 'app-rentalAds-owner-list',
  templateUrl: './rentalAds-owner-list.component.html',
  styleUrls: ['./rentalAds-owner-list.component.scss'],
})
export class RentalAdsOwnerListComponent implements OnInit {
  rentalAds: RentalAdCard[];

  constructor(
    private route: ActivatedRoute,
    public router: Router,
    private rentalAdService: RentalAdService
  ) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.rentalAds = data.rentalAdsResolver.rentalAds;
      console.log(this.rentalAds);
    });
  }

  public deleteRentalAd(rentalAdId: string) {
    console.log(rentalAdId);
    this.rentalAdService.removeRentalAd(rentalAdId);
  }
}
