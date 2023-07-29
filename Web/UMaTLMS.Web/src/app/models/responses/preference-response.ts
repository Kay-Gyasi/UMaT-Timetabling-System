import {LecturerResponse} from "./lecturer-response";
import {CourseResponse} from "./course-response";

export class PreferenceResponse{
  id: number;
  type: string;
  value:string;
  timetableType: string;
  lecturer?: string;
  course?:string;
}
