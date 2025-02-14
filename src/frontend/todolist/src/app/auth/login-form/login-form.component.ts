import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { MatInputModule } from '@angular/material/input';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'app-login-form',
  imports: [
    MatInputModule,
    RouterLink,
    FormsModule
  ],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.css'
})

export class LoginFormComponent {
  email: string = ""
  password: string = ""

  @Output() loginEvent = new EventEmitter<{ email: string, password: string }>()

  onSubmit(event: Event) {
    event.preventDefault()

    this.loginEvent.emit({
      email: this.email,
      password: this.password
    })
  }

  getEmailInput(email: string) { this.email = email }
  getPasswordInput(password: string) { this.password = password }
}