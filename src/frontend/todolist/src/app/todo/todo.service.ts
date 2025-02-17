import { Injectable } from '@angular/core';
import { Task } from './task';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private task: Task = { id: "", title: "", description: "", status: 0 }
  private tasks: Task[] = [
    {
      id: "T01",
      title: "Fazer compras",
      description: "Comprar ingredientes para a receita de bolo.",
      status: 0
    },
    {
      id: "T02",
      title: "Estudar JavaScript",
      description: "Revisar funções e objetos em JavaScript.",
      status: 1
    },
    {
      id: "T03",
      title: "Limpar a casa",
      description: "Fazer a limpeza geral no apartamento.",
      status: 0
    },
    {
      id: "T04",
      title: "Ler livro",
      description: "Ler o capítulo 5 do livro sobre desenvolvimento web.",
      status: 1
    },
    {
      id: "T05",
      title: "Exercícios físicos",
      description: "Fazer caminhada de 30 minutos no parque.",
      status: 0
    },
    {
      id: "T06",
      title: "Organizar escritório",
      description: "Arrumar a mesa e organizar os materiais de trabalho.",
      status: 0
    },
    {
      id: "T07",
      title: "Revisar código",
      description: "Revisar o código do projeto de ToDo List.",
      status: 1
    },
    {
      id: "T08",
      title: "Assistir tutorial",
      description: "Assistir tutorial sobre React Hooks.",
      status: 0
    }
  ];

  constructor() { }

  getTasks() {
    return this.tasks
  }

  filterByQuery(query: string) {
    if (!query.trim()) {
      return this.tasks;
    }

    return this.tasks.filter(x => 
      x.title.toLowerCase().includes(query.toLowerCase()) ||
      x.description.toLowerCase().includes(query.toLowerCase())
    )
  }

  filterBySelect(select: string) {
    if (select === "todo") {
      return this.tasks.filter(x => x.status === 0)
    } else if (select === "done") {
      return this.tasks.filter(x => x.status === 1)
    }
    return this.tasks
  }

  addTask(task: Task) {
    this.tasks.push(task)
  }

  getTaskById(id: string) {
    let foundTask = this.tasks.find(x => x.id.toLowerCase() === id.toLowerCase())
    return foundTask
  }

  editTask(id: string, title: string, description: string) {
    const foundTask = this.tasks.find(x => x.id.toLowerCase() === id.toLowerCase())
    if (foundTask) {
      this.task = foundTask
    }

    this.task.title = title
    this.task.description = description
  }

  deleteTask(id: string){
    this.tasks = this.tasks.filter(x => x.id.toLowerCase() !== id.toLowerCase())
    return this.tasks
  }
}