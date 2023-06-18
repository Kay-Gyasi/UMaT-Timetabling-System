export class RoomRequest{
  id:number;
  name:string;
  capacity:number;
  isLab:boolean;
  isWorkShop:boolean;
}

export class LectureRequest{
  id:number;
  lecturerId:number;
  courseId:number;
  isPractical:boolean;
  isVLE:boolean;
  subClassGroups:SubClassRequest[];
}

export class SubClassRequest{
  id:number;
  groupId:number;
  size:number;
  name:string;
  lectures:LectureRequest[];
}
