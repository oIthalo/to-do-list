import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { LoginFormComponent } from '../login-form/login-form.component';

@Component({
  selector: 'app-register',
  imports: [
    LoginFormComponent
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {
  email: string = ""
  password: string = ""

  constructor(private _service: AuthService) { }

  onLogin(event: { email: string, password: string }) {
    this.email = event.email
    this.password = event.password

    this.login();
  }

  login() {
    this._service.login(this.email, this.password);
  }
}