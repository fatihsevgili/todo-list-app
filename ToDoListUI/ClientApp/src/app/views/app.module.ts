import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import { AppComponent } from './app.component';
import {DxButtonModule, DxCheckBoxModule, DxDataGridModule, DxTextBoxModule} from "devextreme-angular";
import {NavMenuComponent} from "./nav-menu/nav-menu.component";
import {TodoListComponent} from "./todo-list/todo-list.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    TodoListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: TodoListComponent, pathMatch: 'full'},
    ]),
    DxButtonModule,
    DxTextBoxModule,
    DxDataGridModule,
    DxCheckBoxModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
