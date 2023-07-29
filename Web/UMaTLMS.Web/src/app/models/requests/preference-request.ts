export class PreferenceRequest{
  type: number;
  values: string;
  dayForTimeNotAvailable?:string;
  timetableType: number;
  lecturers?: number[];
  courses?:number[];
}
