import { ReservationService } from './../../../../services/reservation.service';
import { IdentityService } from './../../../../services/identity.service';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-rentalAd-reservations',
  templateUrl: './rentalAd-reservations.component.html',
  styleUrls: ['./rentalAd-reservations.component.scss'],
})
export class RentalAdReservationsComponent implements OnInit {
  @Input() reservations: any;
  emailAddresses: string[] = [];

  constructor(
    private identityService: IdentityService,
    private reservationService: ReservationService
  ) {}

  ngOnInit() {
    console.log(this.reservations);
  }

  public toDate(date: string): string {
    return new Date(date).toDateString();
  }

  public showDetails(accountId: string, i: number) {
    this.identityService.getAccount(accountId).subscribe((res) => {
      this.emailAddresses[i] = res.account.emailAddress;
    });
  }

  public hasEmail(i: number) {
    return typeof this.emailAddresses[i] !== 'undefined';
  }

  public isOnGoing(reservation: any) {
    return reservation.state == 'Ongoing';
  }

  public cancelReservation(reservationId: string, i: number) {
    this.reservationService.cancelReservation(reservationId);
    if (Array.isArray(this.reservations)) {
      this.reservations.splice(i, 1);
    }
  }
}
