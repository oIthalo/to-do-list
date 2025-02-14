import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isAuth: boolean = false

  constructor(private router: Router) { }

  // implementar com a API depois
  register(username: string, email: string, password: string) {
    // chamar endpoint da api para registro
    // verificar resposta, se não for 200|OK retornar mensagem de erro
    // se for 200|OK redirecionar para a pagina home
    this.isAuth = true
    this.router.navigate(['/home'])
  }

  // implementar com a API depois
  login(email: string, password: string) {
    // chamar endpoint da api para login
    // verificar resposta, se não for 200|OK retornar mensagem de erro
    // se for 200|OK redirecionar para a pagina home
    this.isAuth = true
    this.router.navigate(['/home'])
  }

  userIsAuth(){
    return this.isAuth
  }
}