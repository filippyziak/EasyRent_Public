import { AlertifyService } from './alertify.service';
import { RentalAdDetails } from './../models/RentalAdDetails';
import { RentalAdCard } from './../models/RentalAdCard';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable, Subscription } from 'rxjs';
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class RentalAdService {
  baseUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private datePipe: DatePipe,
    private alertfiy: AlertifyService,
    private route: Router
  ) {}

  public getRentalAd(rentalAdId: string): Observable<RentalAdDetails> {
    return this.http.get<RentalAdDetails>(
      this.baseUrl + 'rental-ad/rentalad/' + rentalAdId
    );
  }

  public getRentalAdCard(): Observable<RentalAdCard[]> {
    return this.http.get<RentalAdCard[]>(this.baseUrl + 'rental-ad/rentalad', {
      params: new HttpParams().set('PageSize', 5),
    });
  }

  public getRentalAdForPlaceOwner(): Observable<RentalAdCard[]> {
    return this.http.get<RentalAdCard[]>(
      this.baseUrl + 'rental-ad/rentalad/owner'
    );
  }

  public getFilteredRentalAdCard(
    city: string,
    country: string,
    arrivalDate: string,
    departureDate: string,
    pageNumber: number
  ): Observable<any> {
    return this.http.get<RentalAdCard[]>(this.baseUrl + 'rental-ad/rentalad', {
      params: new HttpParams()
        .set('PageSize', 5)
        .set('Country', country)
        .set('City', city)
        .set('ArrivalDate', this.datePipe.transform(arrivalDate) ?? '')
        .set('DepartureDate', this.datePipe.transform(departureDate) ?? '')
        .set('PageNumber', pageNumber),
    });
  }

  public createRentalAd(
    description: string,
    title: string,
    pricePerDay: number,
    country: string,
    city: string,
    street: string,
    photos: File[]
  ) {
    const formData = new FormData();

    for (var i = 0; i < photos.length; i++) {
      formData.append('pictureFiles', photos[i]);
    }
    formData.append('description', description);
    formData.append('title', title);
    formData.append('country', country);
    formData.append('city', city);
    formData.append('street', street);

    return this.http
      .post(this.baseUrl + 'rental-ad/rentalad', formData, {
        params: new HttpParams().set('pricePerDay', pricePerDay),
      })
      .toPromise()
      .then(
        (res) => {
          this.alertfiy.success('Rental ad successfully created');
          this.route.navigate(['/owner']);
        },
        (error) => {
          console.log(error)
          this.alertfiy.error('Creating rental ad failed'+ error.error.error.message);
        }
      );
  }

  public async updateRentaldDescription(rentalAdId: string, description: string) {
    await this.http
      .patch(this.baseUrl + 'rental-ad/rentalad/description', {
        NewDescription: description,
        RentalAdId: rentalAdId,
      })
      .toPromise()
      .then(
        (res) => {
          this.alertfiy.success('Rental ad description updated');
        },
        (error) => {
          this.alertfiy.error('Updating rental ad descrption failed');
        }
      );
  }

  public async updateRentaldTitle(rentalAdId: string, title: string) {
    await this.http
      .patch(this.baseUrl + 'rental-ad/rentalad/title', {
        NewTitle: title,
        RentalAdId: rentalAdId,
      })
      .toPromise()
      .then(
        (res) => {
          this.alertfiy.success('Rental ad title updated');
        },
        (error) => {
          this.alertfiy.error('Updating rental ad title failed');
        }
      );
  }

  public async updateRentaldPrice(rentalAdId: string, pricePerDay: number) {
    await this.http
      .patch(this.baseUrl + 'rental-ad/rentalad/price', {
        NewPricePerDay: pricePerDay,
        RentalAdId: rentalAdId,
      })
      .toPromise()
      .then(
        (res) => {
          this.alertfiy.success('Rental ad price updated');
        },
        (error) => {
          this.alertfiy.error('Updating rental ad price failed');
        }
      );
  }

  public async updateRentaldAddress(
    rentalAdId: string,
    country: string,
    city: string,
    street: string
  ) {
    await this.http
      .patch(this.baseUrl + 'rental-ad/rentalad/address', {
        NewCountry: country,
        NewCity: city,
        NewStreet: street,
        RentalAdId: rentalAdId,
      })
      .toPromise()
      .then(
        (res) => {
          this.alertfiy.success('Rental ad address updated');
        },
        (error) => {
          this.alertfiy.error('Updating rental ad address failed');
        }
      );
  }

  public async updateRentaldPhotos(rentalAdId: string, photos: File[]) {
    const formData = new FormData();

    for (var i = 0; i < photos.length; i++) {
      formData.append('pictures', photos[i]);
    }

    await this.http
      .post(
        this.baseUrl + 'rental-ad/rentalad/' + rentalAdId + '/pictures',
        formData,
        {
          headers: {
            Accept: '*/*',
          },
        }
      )
      .toPromise()
      .then(
        (res) => {
          this.alertfiy.success('Rental ad pictures updated');
        },
        (error) => {
          this.alertfiy.error('Updating rental ad pictures failed');
        }
      );
  }

  public removePhoto(rentalAdId: string, pictureId: string) {
    this.http
      .delete(this.baseUrl + 'rental-ad/rentalad/picture', {
        params: new HttpParams()
          .set('PictureId', pictureId)
          .set('RentalAdId', rentalAdId),
      })
      .toPromise()
      .then(
        (res) => {
          this.alertfiy.success('Rental ad pictures removed');
        },
        (error) => {
          this.alertfiy.error('Removing rental ad pictures failed');
        }
      );
  }

  public removeRentalAd(rentalAdId: string){
    this.http
      .delete(this.baseUrl + 'rental-ad/rentalad/', {
        params: new HttpParams()
          .set('RentalAdId', rentalAdId),
      })
      .toPromise()
      .then(
        (res) => {
          this.alertfiy.success('Rental ad removed');
        },
        (error) => {
          this.alertfiy.error('Removing rental ad failed');
        }
      );
  }
}
