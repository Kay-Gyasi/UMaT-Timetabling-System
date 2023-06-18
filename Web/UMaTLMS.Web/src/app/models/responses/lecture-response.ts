import {SubClassResponse} from "./class-response";

export class LectureResponse{
  id:number;
  lecturerId:number;
  courseId:number;
  lecturer:LecturerResponse;
  course:CourseResponse;
  duration:number;
  courseNo:number;
  isPractical:boolean;
  isVLE:boolean;
  subClassGroups:SubClassResponse[];
}

export class LecturerResponse{
  id:number;
  umatId:number;
  name:string;
}

export class CourseResponse{
  id:number;
  name:string;
}

export class LecturePageResponse{
  id:number;
  lecturer:string;
  course:string;
  isPractical:boolean;
  isVLE:boolean;
  classes:string[];
}