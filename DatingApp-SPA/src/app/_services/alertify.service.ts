import { Injectable } from '@angular/core';
// msg-wrapper service clas to create alertify methods; this service wuld be injected in to the class where evver we need to show some alert
import * as alertify from 'alertifyjs';

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

  constructor() { }

  confirm(message: string, okCallback: () => any) {
    alertify.confirm(message, (e: any) => {
      if (e) {
        okCallback();
      } else { }
    });
  }

  success(message: string) {
    alertify.success(message);
  }
  warning(message: string) {
    alertify.warning(message);
  }
  error(message: string) {
    alertify.error(message);
  }
  message(message: string) {
    alertify.message(message);
  }
}
