import { Routes } from '@angular/router';

import { AuthGuard } from './auth/auth.guard';
import { NotfoundComponent } from './notfound/notfound/notfound.component';
import { LoginGuard } from './login-guard';

export const APP_ROUTES: Routes = [
    {
        path: "", pathMatch: "full", redirectTo: "todo"
    },
    {
        path: "",
        loadChildren: () => import('./auth/auth.routes').then(x => x.AUTH_ROUTES),
        canActivate: [LoginGuard]
    }, 
    {
        path: "todo",
        loadChildren: () => import('./todo/todo.routes').then(x => x.TODO_ROUTES),
        canActivate: [AuthGuard]
    },
    {
        path: "**",
        component: NotfoundComponent
    }
];