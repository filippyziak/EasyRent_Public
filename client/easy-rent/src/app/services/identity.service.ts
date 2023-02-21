import { AlertifyService } from './alertify.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class IdentityService {
  baseUrl = environment.apiUrl;
  jwtHelper = new JwtHelperService();

  constructor(private http: HttpClient, private alertify: AlertifyService) {}

  public login(emailAddress: string, password: string) {
    return this.http
      .post(this.baseUrl + 'identity/account/login', {
        EmailAddress: emailAddress,
        Password: password,
      })
      .pipe(
        map((res: any) => {
          localStorage.setItem('token', res.jwtToken);
          localStorage.setItem('user', JSON.stringify(res.account));
        })
      );
  }

  public register(emailAddress: string, password: string, accountType: number) {
    console.log({
      EmailAddress: emailAddress,
      Password: password,
    });
    return this.http.post(this.baseUrl + 'identity/account', {
      EmailAddress: emailAddress,
      Password: password,
      AccountType: accountType,
    });
  }

  public updateEmailAddress(emailAddress: string) {
    return this.http
      .patch(this.baseUrl + 'identity/account/email', {
        NewEmailAddress: emailAddress,
      })
      .subscribe(
        () => {
          this.alertify.success('Email updated successfully');
        },
        (error) => {
          this.alertify.error('Cannot update email address');
        }
      );
  }

  public updatePassword(password: string) {
    return this.http
      .patch(this.baseUrl + 'identity/account/password', {
        NewPassword: password,
      })
      .subscribe(
        () => {
          this.alertify.success('Password updated successfully');
        },
        (error) => {
          this.alertify.error('Cannot update password');
        }
      );
  }

  public getAccounts(): Observable<any> {
    return this.http.get<Observable<any>>(this.baseUrl + 'identity/account');
  }

  public getAccount(accountId: string): Observable<any> {
    return this.http.get<Observable<any>>(this.baseUrl + 'identity/account/'+ accountId);
  }

  public logout() {
    localStorage.clear();
  }

  public isLoggedIn() {
    return !!localStorage.getItem('token');
  }

  public isPlaceOwner() {
    return (
      (this.jwtHelper.decodeToken(localStorage.getItem('token'))
        .role as string) == 'PlaceOwner'
    );
  }
  public isTenant() {
    return (
      (this.jwtHelper.decodeToken(localStorage.getItem('token'))
        .role as string) == 'Tenant'
    );
  }
  public isModerator() {
    return (
      (this.jwtHelper.decodeToken(localStorage.getItem('token'))
        .role as string) == 'Moderator'
    );
  }

  public getAccountId() {
    return (
      (this.jwtHelper.decodeToken(localStorage.getItem('token'))
        .nameid as string)
    );
  }
}
