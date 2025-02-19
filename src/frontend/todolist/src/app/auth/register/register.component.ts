import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { RegisterRequest } from '../models/register/register-request';
import { RegisterResponse } from './../models/register/register-response';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-register',
  imports: [RouterLink, FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})

export class RegisterComponent {
  request: RegisterRequest = {
    name: "",
    email: "",
    password: ""
  }

  responseData: RegisterResponse = {
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
    this._service.register(this.request).subscribe(
      (response: RegisterResponse) => {
        this.responseData = response
        this._route.navigate(['/todo'])

      },
      (error) => {
        console.error('Erro ao enviar os dados:', error);
      }
    )
  }
}