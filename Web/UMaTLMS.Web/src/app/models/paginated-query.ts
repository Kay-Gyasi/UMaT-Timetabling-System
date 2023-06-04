export class PaginatedQuery {
  constructor(start:number, pageNumber:number, pageSize:number){
    this.Start = start;
    this.PageNumber = pageNumber;
    this.PageSize = pageSize;
  }

  static Build(start:number = 0, pageNumber:number = 1, pageSize:number = 20) : PaginatedQuery{
    return new PaginatedQuery(start, pageNumber, pageSize);
  }

  thenSearch(term:string){
    this.Search = term;
  }

  Skip!:number;
  PageNumber!:number;
  PageSize!:number;
  Start!:number;
  Sort!:string;
  Search!:string;
  OtherJson!:string;
}
