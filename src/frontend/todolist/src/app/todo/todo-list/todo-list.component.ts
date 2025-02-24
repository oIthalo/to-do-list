import { TaskResponse } from './../models/response/task-response';
import { TasksResponse } from './../models/response/tasks-response';
import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { TodoService } from '../todo.service';
import { NavigationGuardService } from '../nav-guard.service';

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
  tasksResponse: TasksResponse = { tasks: [] }
  originalTasks: TaskResponse[] = []
  filteredTasks: TaskResponse[] = []

  selectFilter: string = ""
  selectOrder: string = ""

  constructor(
    private _service: TodoService,
    private _route: Router,
    private _navService: NavigationGuardService
  ) { }

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks() {
    this._service.getTasks().subscribe(
      (response: TasksResponse) => {
        this.tasksResponse.tasks = response.tasks;
        this.originalTasks = [...response.tasks]
      });
  }

  onDelete(id: string) {
    this._service.deleteTask(id).pipe().subscribe(
      () => {
        window.location.reload()
      }
    );
  }

  filter(query: string) {
    if (query.trim() !== "") {
      this.tasksResponse.tasks = this.tasksResponse.tasks.filter(x =>
        x.title.toLowerCase().includes(query.toLowerCase()) ||
        x.description.toLowerCase().includes(query.toLowerCase())
      );
    } else {
      this.tasksResponse.tasks = [...this.originalTasks]
    }
  }

  onChangeSelectFilter(event: Event) {
    this.selectFilter = (event.target as HTMLSelectElement).value

    if (this.selectFilter === "all") {
      this.tasksResponse.tasks = [...this.originalTasks];
    } else if (this.selectFilter === "todo") {
      this.tasksResponse.tasks = [...this.originalTasks].filter(x => x.done === false)
    } else if (this.selectFilter === "done") {
      this.tasksResponse.tasks = [...this.originalTasks].filter(x => x.done === true)
    }
  }

  onChangeSelectOrder(event: Event) {
    this.selectOrder = (event.target as HTMLSelectElement).value;

    switch (this.selectOrder) {
      case "anywhere":
        return
        break;
      case "currents":
        this.tasksResponse.tasks = [...this.originalTasks].sort((a, b) => new Date(a.createdOn).getTime() - new Date(b.createdOn).getTime());
        break;
      case "old":
        this.tasksResponse.tasks = [...this.originalTasks].sort((a, b) => new Date(b.createdOn).getTime() - new Date(a.createdOn).getTime());
        break;
    }
  }

  onChangeStatus(id: string, task: TaskResponse) {
    task.done = !task.done;
    this._service.changeStatus(id, task.done).subscribe();
  }

  toggleChecked(task: TaskResponse) {
    return task.done
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