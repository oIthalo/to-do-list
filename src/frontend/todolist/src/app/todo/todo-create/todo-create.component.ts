import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroupDirective, NgForm, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Task } from '../task';

/** Classe para personalizar o estado de erro */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

/** Componente do formulário */
@Component({
  selector: 'app-todo-create',
  standalone: true,
  imports: [
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule
  ],
  templateUrl: './todo-create.component.html',
  styleUrl: './todo-create.component.css'
})
export class TodoCreateComponent {
  titleFormControl = new FormControl('', [Validators.required]);
  descriptionFormControl = new FormControl('', [Validators.required]);

  matcher = new MyErrorStateMatcher();

  task: Task = {
    title: "",
    description: ""
  }

  @Output() todoEvent = new EventEmitter<Task>()

  onSubmit(event: Event) {
    event.preventDefault()

    this.todoEvent.emit(this.task)
  }

  getInputTitle(title: string) { this.task.title = title }
  getInputDescription(description: string) { this.task.description = description }
}
