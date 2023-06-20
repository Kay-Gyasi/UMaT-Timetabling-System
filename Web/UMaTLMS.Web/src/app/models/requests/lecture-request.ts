import {SubClassRequest} from "./sub-class-request";

export class LectureRequest {
  id: number;
  lecturerId: number;
  courseId: number;
  isPractical: boolean;
  isVLE: boolean;
  preferredRoom: string;
  subClassGroups: SubClassRequest[];
}
