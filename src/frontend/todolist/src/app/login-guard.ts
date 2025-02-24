import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { map, Observable, tap } from 'rxjs';
import { AuthService } from './auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class LoginGuard implements CanActivate {
  constructor(
    private _authService: AuthService,
    private _router: Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | boolean {
    return this.verifyAccess()
  }

  private verifyAccess(): Observable<boolean> {
    return this._authService.isAuth().pipe(
      map((isAuthenticated) => {
        if (isAuthenticated) {
          this._router.navigate(['/todo']);
          return false;
        }
        return true; 
      })
    );
  }
}
