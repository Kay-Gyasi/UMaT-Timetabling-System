export class ApiResponse<TResponse>{
  data:TResponse;
  message:string;
  statusCode:number;
}
