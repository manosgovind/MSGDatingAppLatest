import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  // msg - declaring a variable named model of type 'any'
  mod: any = {};

  // injecting  auth service to access the methods created in the authservice

  constructor(public authservice: AuthService,
              private alertify: AlertifyService,
              private router: Router) { }

  ngOnInit() {
  }

  // msg - writing a method called login, this consumes login method from authservice
  login() {
    this.authservice.login(this.mod).subscribe(next => {
      this.alertify.success('Logged in successfully.');
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.router.navigate(['/members']); // this code can also be added in the next => section after the alertify success call
    });
  }

  isLoggedIn() {
    return this.authservice.loggedIn();

    // return !!token; // msg -- !! means if something is there in the token return true, else return false.
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.message('Logged out!');
    this.router.navigate(['/home']);
  }

}
