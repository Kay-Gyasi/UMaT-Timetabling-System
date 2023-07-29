import { CourseResponse } from "./course-response";
import {SubClassResponse} from "./sub-class-response";
import {LecturerResponse} from "./lecturer-response";

export class LectureResponse{
  id:number;
  lecturerId:number;
  courseId:number;
  lecturer:LecturerResponse;
  course:CourseResponse;
  duration:number;
  preferredRoom:string;
  isPractical:boolean;
  isVLE:boolean;
  subClassGroups:SubClassResponse[];
}

export class LecturePageResponse{
  id:number;
  lecturer:string;
  course:string;
  isPractical:boolean;
  isVLE:boolean;
  classes:string[];
}
