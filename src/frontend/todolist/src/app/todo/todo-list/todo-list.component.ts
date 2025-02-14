import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TodoItemComponent } from "../todo-item/todo-item.component";
import { FormsModule } from '@angular/forms';
import { Task } from '../task';

@Component({
  selector: 'app-todo-list',
  imports: [TodoItemComponent, FormsModule],
  templateUrl: './todo-list.component.html',
  styleUrl: './todo-list.component.css'
})
export class TodoListComponent {
  query: string = ""
  select: string = ""

  @Input() tasks: Task[] = []
  @Output() eventQuery = new EventEmitter<{ query: string }>()
  @Output() eventSelect = new EventEmitter<{ select: string }>()

  onSelect(select: string) {
    this.select = select
    this.eventSelect.emit({ select: this.select })
  }

  onInput(query: string) {
    this.query = query
    this.eventQuery.emit({ query })
  }
}
