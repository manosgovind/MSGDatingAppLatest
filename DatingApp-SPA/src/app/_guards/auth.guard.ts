import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../_services/auth.service';
import { Route } from '@angular/compiler/src/core';
import { AlertifyService } from '../_services/alertify.service';

@Injectable({
  providedIn: 'root'
})
/**
 * msg -- route guard for preventing unauthorised access to the pages
 */
export class AuthGuard implements CanActivate {

  constructor(
    private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService) { }
  canActivate(): boolean {
    if (this.authService.loggedIn()) {
      return true;
    }

    this.alertify.error('You are not allowed to access the page with out loging in!!');
    this.router.navigate(['/home']);
    return false;
  }

}
