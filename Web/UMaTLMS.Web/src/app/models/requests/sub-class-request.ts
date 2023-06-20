import {LectureRequest} from "./lecture-request";

export class SubClassRequest {
  id: number;
  groupId: number;
  size: number;
  name: string;
  lectures: LectureRequest[];
}
