import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {DashboardComponent} from "./components/dashboard/dashboard.component";
import {ViewRoomsComponent} from "./components/rooms/view-rooms/view-rooms.component";
import {AddRoomComponent} from "./components/rooms/add-room/add-room.component";
import {EditRoomComponent} from "./components/rooms/edit-room/edit-room.component";
import {ViewClassesComponent} from "./components/classes/view-classes/view-classes.component";
import {ViewLecturesComponent} from "./components/lectures/view-lectures/view-lectures.component";
import {EditLectureComponent} from "./components/lectures/edit-lecture/edit-lecture.component";
import { ViewCoursesComponent } from './components/courses/view-courses/view-courses.component';
import { EditCourseComponent } from './components/courses/edit-course/edit-course.component';
import {CoursePreferencesComponent} from "./components/courses/course-preferences/course-preferences.component";
import {ViewLecturersComponent} from "./components/lecturers/view-lecturers/view-lecturers.component";
import {LecturerPreferencesComponent} from "./components/lecturers/lecturer-preferences/lecturer-preferences.component";

const routes: Routes = [
  {path:"", component:DashboardComponent},
  {path:"*", component:DashboardComponent},
  {path:"rooms", component: ViewRoomsComponent},
  {path:"rooms/add", component:AddRoomComponent},
  {path:"rooms/edit/:id", component:EditRoomComponent},
  {path:"courses/edit/:id", component:EditCourseComponent},
  {path:"classes", component:ViewClassesComponent},
  {path:"courses", component:ViewCoursesComponent},
  {path:"courses/preferences", component:CoursePreferencesComponent},
  {path:"lectures", component:ViewLecturesComponent},
  {path:"lecturers", component:ViewLecturersComponent},
  {path:"lecturers/preferences", component:LecturerPreferencesComponent},
  {path:"lectures/edit/:id", component:EditLectureComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
