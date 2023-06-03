import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AsideMenuComponent } from './components/widgets/aside-menu/aside-menu.component';
import { AsideToolbarComponent } from './components/widgets/aside-toolbar/aside-toolbar.component';
import { LoginUserComponent } from './components/widgets/login-user/login-user.component';
import { TopHeaderComponent } from './components/widgets/top-header/top-header.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AddRoomComponent } from './components/rooms/add-room/add-room.component';
import { EditRoomComponent } from './components/rooms/edit-room/edit-room.component';
import { ViewRoomsComponent } from './components/rooms/view-rooms/view-rooms.component';
import {CommonModule} from "@angular/common";
import {MetronicJs} from "./helpers/metronic-js";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";
import {HttpRequest} from "./services/http/base/http-request";
import {IHttpRequest} from "./services/http/base/ihttp-request";
import {ToastrModule} from "ngx-toastr";
import {NotificationService} from "./services/notification.service";
import { RoomService } from './services/http/room-service';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";

@NgModule({
  declarations: [
    AppComponent,
    AsideMenuComponent,
    AsideToolbarComponent,
    LoginUserComponent,
    TopHeaderComponent,
    DashboardComponent,
    AddRoomComponent,
    EditRoomComponent,
    ViewRoomsComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot()
  ],
  providers: [
    {provide:IHttpRequest, useClass: HttpRequest},
    MetronicJs, RoomService, NotificationService],
  bootstrap: [AppComponent]
})
export class AppModule {
  // Important. DON'T DELETE
  constructor(private metronicjs:MetronicJs){
    metronicjs.init()
  }
}
