import { Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';
import { NotfoundComponent } from './notfound/notfound/notfound.component';

export const APP_ROUTES: Routes = [
    {
        path: "", pathMatch: "full", redirectTo: "todo"
    },
    {
        path: "",
        loadChildren: () => import('./auth/auth.routes').then(x => x.AUTH_ROUTES)
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