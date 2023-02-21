import { Injectable } from '@angular/core';
declare let alertify: any;

@Injectable({
  providedIn: 'root',
})
export class AlertifyService {
  constructor() {}

  public confirm(message: string, okCallback: () => any) {
    alertify.confirm(message, (e: any) => {
      if (e) {
        okCallback();
      } else {
      }
    });
  }

  public success(message: any) {
    alertify.success(message);
  }

  public error(message: any) {
    alertify.error(message);
  }

  public warning(message: any) {
    alertify.warning(message);
  }

  public message(message: any) {
    alertify.message(message);
  }
}