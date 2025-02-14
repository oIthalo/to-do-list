import { Component, OnInit } from '@angular/core';
import { TodoCreateComponent } from "../todo-create/todo-create.component";
import { TodoListComponent } from "../todo-list/todo-list.component";
import { Task } from '../task';
import { TodoService } from '../todo.service';

@Component({
  selector: 'app-todo',
  imports: [TodoCreateComponent, TodoListComponent],
  templateUrl: './todo.component.html',
  styleUrl: './todo.component.css'
})

export class TodoComponent implements OnInit {
  task: Task = { id: "", title: "", description: "", status: 0 }
  tasks: Task[] = []
  query: string = ""
  select: string = ""

  constructor(private _service: TodoService) { }

  ngOnInit(): void {
    this.tasks = this._service.getTasks()
  }

  onCreateTask(event: Task) {
    this.task.id = event.id
    this.task.title = event.title
    this.task.description = event.description
    this.task.status = event.status

    this.addTask()
  }

  onDigitQuery(event: { query: string }) {
    this.query = event.query

    this.tasks = this._service.filterByQuery(this.query)
  }

  onChangeSelect(event: { select: string }) {
    this.select = event.select

    this.tasks = this._service.filterBySelect(this.select)
  }

  addTask() {
    this._service.addTask(this.task)
    this.tasks = this._service.filterByQuery(this.query)
  }
}