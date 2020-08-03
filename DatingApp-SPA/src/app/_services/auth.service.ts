import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';

  // msg -  injecting httpClient module to the service
  constructor(private http: HttpClient) { }

  // creating a login method 
  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('token', user.token); // storing the returned token in the localstorage for future use
          }
        })
      );

  }
  register(model: any)
  {
    return this.http.post(this.baseUrl + 'register', model);
  }

}
