import { AlertifyService } from './alertify.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ManagementService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private alertify: AlertifyService) {}

  public suspend(accountId: string) {
    return this.http
      .patch(this.baseUrl + 'management/account/suspend', {
        AccountId: accountId,
      })
      .subscribe(
        () => {
          this.alertify.success('Account suspended successfully');
        },
        (error) => {
          this.alertify.error('Cannot suspend account');
        }
      );
  }

  public activate(accountId: string) {
    console.log(accountId)
    return this.http
      .patch(this.baseUrl + 'management/account/activate', {
        AccountId: accountId,
      })
      .subscribe(
        () => {
          this.alertify.success('Account activated successfully');
        },
        (error) => {
          this.alertify.error('Cannot activate account');
        }
      );
  }
}
