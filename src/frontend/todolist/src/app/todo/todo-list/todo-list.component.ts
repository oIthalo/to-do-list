import { TaskResponse } from './../models/response/task-response';
import { TasksResponse } from './../models/response/tasks-response';
import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { TodoService } from '../todo.service';
import { NavigationGuardService } from '../nav-guard.service';
import { catchError, filter, of } from 'rxjs';

@Component({
  selector: 'app-todo-list',
  imports: [
    CommonModule,
    RouterOutlet,
    FormsModule
  ],
  templateUrl: './todo-list.component.html',
  styleUrl: './todo-list.component.css'
})
export class TodoListComponent implements OnInit {
  tasks: TasksResponse = { tasks: [] }
  originalTasks: TaskResponse[] = []
  filteredTasks: TaskResponse[] = []

  select: string = ""

  constructor(
    private _service: TodoService,
    private _route: Router,
    private _navService: NavigationGuardService
  ) { }

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks() {
    this._service.getTasks().pipe(
      catchError(err => {
        console.log(err);
        return of({ tasks: [] });
      })
    ).subscribe((response: TasksResponse) => {
      this.tasks.tasks = response.tasks;
      this.originalTasks = [...response.tasks]
    });
  }

  onDelete(id: string) {
    this._service.deleteTask(id).pipe().subscribe(
      () => {
        this.tasks.tasks = this.tasks.tasks.filter(task => task.id !== id);
      }
    );
  }

  filter(query: string) {
    if (query.trim() !== "") {
      this.tasks.tasks = this.tasks.tasks.filter(x =>
        x.title.toLowerCase().includes(query.toLowerCase()) ||
        x.description.toLowerCase().includes(query.toLowerCase())
      );
    } else {
      this.tasks.tasks = [...this.originalTasks]
    }
  }

  onChangeSelect(event: Event) {
    this.select = (event.target as HTMLSelectElement).value

    if (this.select === "all") {
      this.tasks.tasks = [...this.originalTasks];
    } else if (this.select === "todo") {
      this.tasks.tasks = [...this.originalTasks].filter(x => x.done === false)
    } else if (this.select === "done") {
      this.tasks.tasks = [...this.originalTasks].filter(x => x.done === true)
    }
  }

  onChangeStatus(id: string, task: TaskResponse) {
    task.done = !task.done; // Alterna o status
    console.log(`Alterando status da task ${id} para ${task.done}`);

    this._service.changeStatus(id, task.done).subscribe();
  }

  toggleChecked(task: TaskResponse) {
    if (task.done === true) {
      return true
    }
    return false
  }

  navToTodoAdd() {
    this._navService.allowNavigation()
    this._route.navigate(['/todo/add'])
  }

  navToTodoEdit(id: string) {
    this._navService.allowNavigation()
    this._route.navigate([`/todo/edit/${id}`])
  }
}