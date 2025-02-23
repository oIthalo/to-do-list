import { Injectable } from '@angular/core';
import { RegisterRequest } from './models/register/register-request';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, Subscription, tap } from 'rxjs';

import { RegisterResponse } from './models/register/register-response';
import { LoginRequest } from './models/login/login-request';
import { LoginResponse } from './models/login/login-response';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly API_URL = "http://localhost:5143/user"

  private tokenSubject = new BehaviorSubject<boolean>(!!this.getToken());
  private tokenCheckSubscription!: Subscription;

  constructor(
    private _httpClient: HttpClient) {
      this.checkTokenExpiration()
    }

  register(request: RegisterRequest): Observable<RegisterResponse> {
    return this._httpClient.post<RegisterResponse>(`${this.API_URL}/register`, request)
      .pipe(
        tap((response: RegisterResponse) => {
          if (response.tokens.accessToken) {
            this.storeToken(response.tokens.accessToken);
          }
        })
      );

  }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this._httpClient.post<LoginResponse>(`${this.API_URL}/login`, request)
      .pipe(
        tap((response: LoginResponse) => {
          if (response.tokens.accessToken) {
            this.storeToken(response.tokens.accessToken);
          }
        })
      );
  }

  private isExpiredToken(token: string): boolean {
    const payload = JSON.parse(atob(token.split('.')[1]));
    const exp = payload.exp * 1000;

    return Date.now() >= exp
  }

  private checkTokenExpiration() {
    const token = this.getToken();
    if (token && this.isExpiredToken(token)) {
      this.logout();
    }
  }

  private logout() {
    localStorage.removeItem('accessToken');
    this.tokenSubject.next(false);
    this.tokenCheckSubscription.unsubscribe()
    window.location.reload()
  }

  private storeToken(token: string) {
    const tokenOnBrowser = this.getToken()
    if (tokenOnBrowser) {
      localStorage.removeItem('accessToken')
    } else {
      localStorage.setItem('accessToken', token)
      this.tokenSubject.next(true);
    }
  }

  getToken(): string | null {
    return localStorage.getItem('accessToken')
  }

  isAuth(): Observable<boolean> {
    return this.tokenSubject.asObservable();
  }
}