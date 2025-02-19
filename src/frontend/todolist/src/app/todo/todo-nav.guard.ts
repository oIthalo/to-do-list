import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { NavigationGuardService } from './nav-guard.service';

@Injectable({
  providedIn: 'root'
})
export class TodoNavGuard implements CanActivate {

  constructor(
    private _route: Router,
    private _navService: NavigationGuardService) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | boolean {
    if (this._navService.isAllowedNavigation()) {
      this._navService.resetNavigation()
      return true
    }

    this._route.navigate(['/todo'])
    return false
  }
}
