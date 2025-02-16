import { Routes } from "@angular/router";

import { TodoEditComponent } from "./todo-edit/todo-edit.component"
import { TodoCreateComponent } from "./todo-create/todo-create.component";
import { TodoListComponent } from "./todo-list/todo-list.component";
import { TodoNavGuard } from "./todo-nav.guard";

export const TODO_ROUTES: Routes = [
    {
        path: "", component: TodoListComponent, children: [
            { path: "add", component: TodoCreateComponent, canActivate: [TodoNavGuard] },
            { path: "edit/:id", component: TodoEditComponent, canActivate: [TodoNavGuard] }
        ]
    },
];