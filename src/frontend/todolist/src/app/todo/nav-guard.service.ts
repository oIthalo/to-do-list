import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NavigationGuardService {
    private canNavigate: boolean = false

    allowNavigation(){
        this.canNavigate = true
    }

    isAllowedNavigation(){
        return this.canNavigate
    }

    resetNavigation(){
        this.canNavigate = false
    }
}