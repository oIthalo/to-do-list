import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { AuthService } from '../auth.service';
import { LoginRequest } from '../models/login/login-request';
import { LoginResponse } from '../models/login/login-response';

@Component({
  selector: 'app-login',
  imports: [RouterLink, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {
  request: LoginRequest = {
    email: "",
    password: ""
  }

  responseData: LoginResponse = {
    name: "",
    tokens: {
      accessToken: "",
      refreshToken: ""
    }
  }

  constructor(
    private _route: Router,
    private _service: AuthService) { }

  onSubmit() {
    this._service.login(this.request).subscribe(
      (response: LoginResponse) => {
        this.responseData = response
        this._route.navigate(['/todo'])
      }
    )
  }
}