import { Injectable } from '@angular/core';
import { Task } from './task';

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  private tasks: Task[] = [
    { title: "Estudar TypeScript", description: "Revisar conceitos básicos e avançados." },
    { title: "Praticar JavaScript", description: "Criar pequenos projetos para reforçar o aprendizado." },
    { title: "Ler sobre Node.js", description: "Entender como funciona o ambiente de execução." },
    { title: "Finalizar projeto React", description: "Implementar a última funcionalidade pendente." },
    { title: "Aprender sobre Express.js", description: "Criar uma API simples com Express." },
    { title: "Refatorar código do portfólio", description: "Melhorar a estrutura e otimizar o CSS." },
    { title: "Assistir tutorial de Docker", description: "Entender o funcionamento de containers." },
    { title: "Configurar banco de dados", description: "Criar tabelas e relacionamentos no PostgreSQL." },
    { title: "Revisar conceitos de CSS", description: "Praticar Flexbox e Grid." },
    { title: "Criar um mini projeto com Vue.js", description: "Fazer uma pequena aplicação para testar." },
    { title: "Melhorar desempenho do site", description: "Otimizar imagens e reduzir tempo de carregamento." },
    { title: "Aprender sobre Next.js", description: "Estudar SSR e SSG." },
    { title: "Testar uma API REST", description: "Fazer requisições com Postman e entender os métodos HTTP." },
    { title: "Criar uma função utilitária", description: "Fazer uma função genérica para manipular arrays." },
    { title: "Ler sobre boas práticas de código", description: "Estudar Clean Code e SOLID." }
  ];

  getTasks() {
    return this.tasks
  }

  addTask(task: Task) {
    this.tasks.push(task)
  }

  filterByQuery(query: string) {
    if (!query.trim()) {
      return this.tasks; 
    }

    return this.tasks.filter(x =>
      x.title.toLowerCase().includes(query.toLowerCase()) ||
      x.description.toLowerCase().includes(query.toLowerCase()))
  }
}
