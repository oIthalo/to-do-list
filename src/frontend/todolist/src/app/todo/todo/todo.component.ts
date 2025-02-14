import { Component, Input, OnInit } from '@angular/core';
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
  task: Task = { title: "", description: "" }
  tasks: Task[] = []
  query: string = ""

  constructor(private _service: TodoService) { }

  ngOnInit(): void {
    this.tasks = this._service.getTasks()
  }

  onCreateTask(event: Task) {
    this.task.title = event.title
    this.task.description = event.description

    this.addTask()
  }

  onDigitQuery(event: { query: string }) { 
    this.query = event.query 
    this.filterByQuery()
  }

  addTask() { 
    this._service.addTask(this.task) 
    this.tasks = this._service.getTasks();
    this.filterByQuery();
  }

  filterByQuery(){
    this.tasks = this._service.filterByQuery(this.query)
  }
}