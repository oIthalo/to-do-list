import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

import { Task } from '../task';

@Component({
  selector: 'app-todo-item',
  imports: [CommonModule],
  templateUrl: './todo-item.component.html',
  styleUrl: './todo-item.component.css'
})
export class TodoItemComponent {
  @Input() tasks: Task[] = []
}