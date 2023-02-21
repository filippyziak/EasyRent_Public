import { NgForm } from '@angular/forms';
import { ReservationService } from './../../../../services/reservation.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-reservation-tenant-list',
  templateUrl: './reservation-tenant-list.component.html',
  styleUrls: ['./reservation-tenant-list.component.scss'],
})
export class ReservationTenantListComponent implements OnInit {
  reservations: any;

  constructor(private reservationService: ReservationService) {}

  ngOnInit() {
    this.reservationService.reservationsForTenant().subscribe((res) => {
      this.reservations = res.placeReservations;
      console.log(this.reservations)
    });
  }

  public toDate(date: string): string {
    return new Date(date).toDateString();
  }

  public isFinished(reservation: any) {
    return reservation.state === 'Finished';
  }

  public review(
    description: string,
    reviewScore: number,
    reservationId: string,
    rentalAdId: string,
    reservation: any
  ) {
    this.reservationService.review(
      description,
      reviewScore,
      reservationId,
      rentalAdId
    );

    reservation.state = 'Reviewed'
  }

  public cancelReservation(reservationId: string, i: number) {
    this.reservationService.cancelReservation(reservationId)
    if (Array.isArray(this.reservations)) {
      this.reservations.splice(i, 1);
    }
  }

  public isOnGoing(reservation: any){
    return reservation.state == 'Ongoing';
  }
}
