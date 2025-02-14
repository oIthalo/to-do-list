import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

import { Task } from './../task';
import { TodoService } from '../todo.service';

@Component({
  selector: 'app-todo-item',
  imports: [CommonModule],
  templateUrl: './todo-item.component.html',
  styleUrl: './todo-item.component.css'
})
export class TodoItemComponent {
  task: Task = { id: "", title: "", description: "", status: 0 }

  @Input() tasks: Task[] = []

  constructor(private _service: TodoService) {}

  onDelete(task: Task) {
    this.tasks = this._service.deleteTask(task.id)
  }

  onCheck(task: Task) {
    task.status = task.status === 1 ? 0 : 1
  }
}