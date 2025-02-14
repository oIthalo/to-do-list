import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isAuth: boolean = true

  constructor(private router: Router) { }

  register(username: string, email: string, password: string) {
    // chamar endpoint da api para registro
    // verificar resposta, se não for 200|OK retornar mensagem de erro
    // se ja existir um usuario com o mesmo email, é enviado para a pagina de login com a mensagem "Já existe um usuario com esse email, faça o login"
    // se for 200|OK redirecionar para a pagina home

    this.isAuth = false
    if (this.isAuth === false) { return }

    this.router.navigate(['/home'])
  }

  login(email: string, password: string) {
    // chamar endpoint da api para login
    // verificar resposta, se não for 200|OK retornar mensagem de erro
    // se nao existir um usuario com o mesmo email, é enviado a mensagem "Usuario invalido, digite um email ou senha diferente"
    // se for 200|OK redirecionar para a pagina home

    this.isAuth = true
    this.router.navigate(['/home'])
  }

  userIsAuth() {
    return this.isAuth
  }
}