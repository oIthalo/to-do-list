import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { TodoService } from '../todo.service';
import { Router } from '@angular/router';
import { Task } from '../task';

@Component({
  selector: 'app-todo-create',
  imports: [
    FormsModule
  ],
  templateUrl: './todo-create.component.html',
  styleUrl: './todo-create.component.css'
})
export class TodoCreateComponent {
  task: Task = {
    id: `${this.gerarStringAleatoria()}`,
    title: "",
    description: "",
    status: 0
  }

  changeInput: boolean = false

  constructor(
    private _service: TodoService,
    private _route: Router
  ) { }

  // gerar o id apenas em quanto nao esta conectado com a api
  gerarStringAleatoria(tamanho = 3) {
    const letras = "abcdefghijklmnopqrstuvwxyz";
    let resultado = "";
    for (let i = 0; i < tamanho; i++) {
      resultado += letras.charAt(Math.floor(Math.random() * letras.length));
    }
    return resultado;
  }

  onAddTask() {
    if (this.task.title.length == 0 || this.task.description.length == 0) {
      return
    }

    this._service.addTask(this.task)
    this._route.navigate(['/todo'])
  }

  onInput() {
    this.changeInput = true
  }
}