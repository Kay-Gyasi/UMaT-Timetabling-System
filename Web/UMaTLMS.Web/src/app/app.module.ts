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
import { RoomService } from './services/http/room.service';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { ViewClassesComponent } from './components/classes/view-classes/view-classes.component';
import {ClassService} from "./services/http/class.service";
import { ViewLecturesComponent } from './components/lectures/view-lectures/view-lectures.component';
import { EditLectureComponent } from './components/lectures/edit-lecture/edit-lecture.component';
import {NgSelectModule} from "@ng-select/ng-select";
import { LoadingScreenComponent } from './components/widgets/loading-screen/loading-screen.component';
import { ViewCoursesComponent } from './components/courses/view-courses/view-courses.component';
import { CourseService } from './services/http/course.service';
import { EditCourseComponent } from './components/courses/edit-course/edit-course.component';
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
    ViewRoomsComponent,
    ViewClassesComponent,
    ViewLecturesComponent,
    EditLectureComponent,
    LoadingScreenComponent,
    ViewCoursesComponent,
    EditCourseComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgSelectModule,
    ToastrModule.forRoot()
  ],
  providers: [
    { provide:IHttpRequest, useClass: HttpRequest },
    MetronicJs, CourseService, RoomService, ClassService, NotificationService],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private metronicJs: MetronicJs) {
    this.metronicJs.init();
  }

}
