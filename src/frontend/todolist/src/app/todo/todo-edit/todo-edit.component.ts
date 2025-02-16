import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TodoService } from '../todo.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Task } from '../task';

@Component({
  selector: 'app-todo-edit',
  imports: [FormsModule],
  templateUrl: './todo-edit.component.html',
  styleUrl: './todo-edit.component.css'
})
export class TodoEditComponent implements OnInit {
  task: Task = { id: "", title: "", description: "", status: 0 }

  constructor(
    private _service: TodoService,
    private _route: Router,
    private _activatedRoute: ActivatedRoute
  ) {
    console.log(_activatedRoute)
  }

  ngOnInit(): void {
    this._activatedRoute.params.subscribe(
      (params: any) => { this.task.id = params['id'] }
    )

    this.task = this._service.getTaskById(this.task.id) || { id: "", title: "", description: "", status: 0 }
  }

  onEditTask() {
    this._service.editTask(this.task.id, this.task.title, this.task.description)
    this._route.navigate(['/todo'])
  }
}