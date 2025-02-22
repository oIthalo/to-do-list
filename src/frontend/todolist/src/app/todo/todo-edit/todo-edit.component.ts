import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TodoService } from '../todo.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { TaskResponse } from '../models/response/task-response';
import { UpdateTaskRequest } from '../models/request/edit-task-request';

@Component({
  selector: 'app-todo-edit',
  imports: [
    FormsModule,
    RouterLink
  ],
  templateUrl: './todo-edit.component.html',
  styleUrl: './todo-edit.component.css'
})
export class TodoEditComponent implements OnInit {
  task: TaskResponse = { id: "", title: "", description: "", createdOn: new Date(), done: false }
  request: UpdateTaskRequest = { title: "", description: "" }

  constructor(
    private _service: TodoService,
    private _route: Router,
    private _activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this._activatedRoute.params.subscribe(
      (params: any) => { this.task.id = params['id'] }
    )

    this._service.getById(this.task.id).pipe().subscribe(
      (response: TaskResponse) => { this.task = response }
    );
  }

  onEditTask() {
    this._service.editTask(this.task.id, this.request = { title: this.task.title, description: this.task.description }).subscribe()
    this._route.navigate(['/todo']).then(() => {
      window.location.reload()
    })
  }
}