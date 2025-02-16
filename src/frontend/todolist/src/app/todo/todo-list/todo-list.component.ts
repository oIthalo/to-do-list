import { Component, OnInit } from '@angular/core';

import { Task } from '../task';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
import { TodoService } from '../todo.service';
import { NavigationGuardService } from '../nav-guard.service';
import { FormsModule } from '@angular/forms';

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
  tasks: Task[] = []
  title: string = ""
  description: string = ""
  query: string = ""
  select: string = ""

  constructor(
    private _service: TodoService,
    private _route: Router,
    private _navService: NavigationGuardService) { }

  ngOnInit(): void {
    this.tasks = this._service.getTasks()
  }

  editOnClick(id: string){
    this._navService.allowNavigation()
    this._route.navigate([`/todo/edit/${id}`])
  }

  addOnClick(){
    this._navService.allowNavigation()
    this._route.navigate(['/todo/add'])
  }

  filterByQuery(){
    this.tasks = this._service.filterByQuery(this.query)
  }

  toggleStatusOnClick(task: Task){
    task.status = task.status === 1 ? 0 : 1
  }

  toggleChecked(task: Task){
    if (task.status === 1){
      return true
    } 
    return false
  }

  onChangeSelect(event: Event){
    this.select = (event.target as HTMLSelectElement).value
    this.tasks = this._service.filterBySelect(this.select)
  }

  onDeleteTask(id: string){
    this.tasks = this._service.deleteTask(id)
  }
}