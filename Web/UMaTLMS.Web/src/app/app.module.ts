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
import { StoreModule } from '@ngrx/store';
import { metaReducers, reducers } from './state/store/reducers';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from 'src/environments/environment';
import { EffectsModule } from '@ngrx/effects';
import {CourseEffects} from "./state/effects/courses.effects";
import {TimetableService} from "./services/http/timetable.service";
import {ClassGroupEffects} from "./state/effects/class-groups.effects";
import {LectureEffects} from "./state/effects/lectures.effects";
import {RoomEffects} from "./state/effects/rooms.effects";
import { CoursePreferencesComponent } from './components/courses/course-preferences/course-preferences.component';
import { ViewLecturersComponent } from './components/lecturers/view-lecturers/view-lecturers.component';
import { LecturerPreferencesComponent } from './components/lecturers/lecturer-preferences/lecturer-preferences.component';
import {LecturerService} from "./services/http/lecturer.service";
import {LecturerEffects} from "./state/effects/lecturers.effects";
import {PreferenceService} from "./services/http/preference.service";
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
    EditCourseComponent,
    CoursePreferencesComponent,
    ViewLecturersComponent,
    LecturerPreferencesComponent
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
    StoreModule.forRoot(reducers, {
      metaReducers
    }),
    StoreDevtoolsModule.instrument({maxAge: 25, logOnly: environment.production}),
    EffectsModule.forRoot(CourseEffects, ClassGroupEffects, LectureEffects, RoomEffects,
      LecturerEffects),
    ToastrModule.forRoot(),
  ],
  providers: [
    { provide:IHttpRequest, useClass: HttpRequest },
    MetronicJs, CourseService, RoomService, ClassService, NotificationService,
    TimetableService, LecturerService, PreferenceService
  ],
  bootstrap: [AppComponent]
})

export class AppModule {
  constructor(private metronicJs: MetronicJs) {
    this.metronicJs.init();
  }

}
