import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroupDirective, FormsModule, NgForm, ReactiveFormsModule, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';

import { MatInputModule } from '@angular/material/input';
import { RouterLink } from '@angular/router';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-register-form',
  imports: [
    FormsModule,
        MatFormFieldModule,
        MatInputModule,
        ReactiveFormsModule,
        RouterLink
  ],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.css'
})

export class RegisterFormComponent {
  username: string = ""
  email: string = ""
  password: string = ""

  usernameFormControl = new FormControl('', [Validators.required])
  emailFormControl = new FormControl('', [Validators.required, Validators.email]);
  passwordFormControl = new FormControl('', [Validators.required]);

  matcher = new MyErrorStateMatcher();

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