import { Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';

export const APP_ROUTES: Routes = [
    {
        path: "", pathMatch: "full", redirectTo: "home"
    },
    {
        path: "",
        loadChildren: () => import('./auth/auth.routes').then(x => x.AUTH_ROUTES) // rotas de autenticacao
    },
    {
        path: "home", 
        loadChildren: () => import('./todo/todo.routes').then(x => x.TODO_ROUTES), // rotas do todo list
        canActivate: [AuthGuard]
    }
];