import { Injectable } from '@angular/core';

import { RegisterRequest } from './models/register/register-request';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { RegisterResponse } from './models/register/register-response';
import { LoginRequest } from './models/login/login-request';
import { LoginResponse } from './models/login/login-response';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly API_URL = "http://localhost:5143/user"

  private tokenSubject = new BehaviorSubject<boolean>(!!localStorage.getItem('accessToken'));

  constructor(private _httpClient: HttpClient) { }

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

  storeToken(token: string){
    const tokenOnBrowser = localStorage.getItem('accessToken')
    if (tokenOnBrowser) {
      localStorage.removeItem('accessToken')
    } else {
      localStorage.setItem('accessToken', token)
      this.tokenSubject.next(true);
    }
  }

  isAuth(): Observable<boolean> {
    return this.tokenSubject.asObservable();
  }
}