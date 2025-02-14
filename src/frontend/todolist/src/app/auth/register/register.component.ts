import { Component } from '@angular/core';
import { RegisterFormComponent } from "../register-form/register-form.component";
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-register',
  imports: [
    RegisterFormComponent
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})

export class RegisterComponent {
  username: string = ""
  email: string = ""
  password: string = ""

  constructor(private _service: AuthService) { }

  onRegister(event: { username: string, email: string, password: string }) {
    this.username = event.username
    this.email = event.email
    this.password = event.password

    this.register();
  }

  register(){
    this._service.register(this.username, this.email, this.password);
  }
}