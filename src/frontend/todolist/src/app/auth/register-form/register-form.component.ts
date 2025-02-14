import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { MatInputModule } from '@angular/material/input';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-register-form',
  imports: [
    MatInputModule,
    RouterLink,
    FormsModule
  ],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.css'
})

export class RegisterFormComponent {
  username: string = ""
  email: string = ""
  password: string = ""

  @Output() registerEvent = new EventEmitter<{ username: string, email: string, password: string }>()

  onSubmit(event: Event) {
    event.preventDefault()

    this.registerEvent.emit({
      username: this.username,
      email: this.email,
      password: this.password
    })
  }

  saveInputName(name: string) { this.username = name }
  saveInputEmail(email: string) { this.email = email }
  saveInputPassword(password: string) { this.password = password }
}