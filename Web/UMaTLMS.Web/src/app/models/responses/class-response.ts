import {LectureResponse} from "./lecture-response";

export class ClassResponse{
  id:number;
  umatId:number;
  size:number;
  numOfSubClasses:number;
  name:string;
}

export class SubClassResponse{
  id:number;
  groupId:number;
  size:number;
  name:string;
  lectures:LectureResponse[];
}
