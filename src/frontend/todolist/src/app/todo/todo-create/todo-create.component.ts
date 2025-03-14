import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { Router, RouterLink } from '@angular/router';
import { catchError, of } from 'rxjs';

import { TodoService } from '../todo.service';
import { CreateTaskRequest } from '../models/request/create-task-request';
import { TaskResponse } from '../models/response/task-response';

@Component({
  selector: 'app-todo-create',
  imports: [
    FormsModule,
    RouterLink
  ],
  templateUrl: './todo-create.component.html',
  styleUrl: './todo-create.component.css'
})
export class TodoCreateComponent {
  request: CreateTaskRequest = {
    title: "",
    description: ""
  }

  taskResponse: TaskResponse = {
    id: "",
    title: "",
    description: "",
    createdOn: new Date(),
    done: false
  }

  changeInput: boolean = false

  constructor(
    private _service: TodoService
  ) { }

  createTask() {
    this._service.createTask(this.request).subscribe(
      (response: TaskResponse) => {
        this.taskResponse = response
        window.location.reload()
      })
  }
}