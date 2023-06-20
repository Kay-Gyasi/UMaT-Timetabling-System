import {LectureResponse} from "./lecture-response";

export class SubClassResponse {
  id: number;
  groupId: number;
  size: number;
  name: string;
  lectures: LectureResponse[];
}
