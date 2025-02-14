import { Injectable } from '@angular/core';
import { Task } from './task';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private task: Task = { id: "", title: "", description: "", status: 0 }

  private tasks: Task[] = [
    { id: "NWV", title: "Estudar TypeScript", description: "Revisar conceitos básicos e avançados.", status: 0 },
    { id: "XJL", title: "Praticar React", description: "Criar um projeto com hooks e context API.", status: 1 },
    { id: "BQT", title: "Revisar JavaScript", description: "Estudar closures, promises e async/await.", status: 0 },
    { id: "MZP", title: "Aprender Node.js", description: "Explorar Express e conexão com banco de dados.", status: 1 },
    { id: "KRD", title: "Estudar Banco de Dados", description: "Revisar SQL e aprender NoSQL.", status: 0 }
  ];

  getTasks() {
    return this.tasks
  }

  deleteTask(id: string){
    this.tasks = this.tasks.filter(x => x.id.toLowerCase() !== id.toLowerCase())
    return this.tasks
  }

  addTask(task: Task) {
    this.tasks.push(task)
  }

  editTask(id: string, title: string, description: string){
    const foundTask = this.tasks.find(x => x.id.toLowerCase() === id.toLowerCase())
    if (foundTask){
      this.task = foundTask
    }

    this.task.title = title
    this.task.description = description
  }

  filterByQuery(query: string) {  
    if (!query.trim()) {
      return this.tasks;
    }

    return this.tasks.filter(x =>
      x.title.toLowerCase().includes(query.toLowerCase()) ||
      x.description.toLowerCase().includes(query.toLowerCase()))
  }

  filterBySelect(select: string){
    if (select === "all"){
      return this.tasks
    } else if (select === "todo"){
      return this.tasks.filter(x => x.status !== 1)
    } else if (select === "done"){
      return this.tasks.filter(x => x.status !== 0)
    }
    
    return this.tasks
  }
}
