export class PaginatedQuery {
  constructor(start:number, currentPage:number, pageSize:number){
    this.Start = start;
    this.CurrentPage = currentPage;
    this.PageSize = pageSize;
  }

  static Build(start:number = 0, currentPage:number = 1, pageSize:number = 20) : PaginatedQuery{
    return new PaginatedQuery(start, currentPage, pageSize);
  }

  thenSearch(term:string){
    this.Search = term;
  }

  Skip!:number;
  CurrentPage!:number;
  PageSize!:number;
  Start!:number;
  Sort!:string;
  Search!:string;
  OtherJson!:string;
}
