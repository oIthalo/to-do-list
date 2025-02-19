import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { TasksResponse } from './models/response/tasks-response';
import { CreateTaskRequest } from './models/request/create-task-request';
import { TaskResponse } from './models/response/task-response';
import { UpdateTaskRequest } from './models/request/edit-task-request';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private readonly API_URL = "http://localhost:5143/task"

  constructor(private _httpClient: HttpClient) { }

  getTasks(): Observable<TasksResponse> {
    return this._httpClient.get<TasksResponse>(`${this.API_URL}/get-all`)
  }

  getById(id: string): Observable<TaskResponse> {
    return this._httpClient.get<TaskResponse>(`${this.API_URL}/get/${id}`)
  }

  createTask(request: CreateTaskRequest): Observable<TaskResponse> {
    return this._httpClient.post<TaskResponse>(`${this.API_URL}/create`, request)
  }

  editTask(id: string, request: UpdateTaskRequest): Observable<void>{
    return this._httpClient.put<void>(`${this.API_URL}/update/${id}`, request)
  }

  deleteTask(id: string): Observable<void> {
    return this._httpClient.delete<void>(`${this.API_URL}/delete/${id}`)
  }

  changeStatus(id: string, status: boolean): Observable<void> {
    return this._httpClient.put<void>(`${this.API_URL}/change-status/${id}`, { done: status })
  }
}