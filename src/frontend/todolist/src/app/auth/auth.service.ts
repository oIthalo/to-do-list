import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private userIsAuthenticated: boolean = false

  constructor() {
    this.authUser()
  }

  authUser() {
    // ver se existe o token no local storage
    // se nao existir nao esta logado
    // this.userIsAuthenticated = false
    // se existir o usuario esta logado
    this.userIsAuthenticated = true
  }

  isAuthUser(){
    return this.userIsAuthenticated
  }
}