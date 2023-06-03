export class PaginatedList<T> {
  data: T[] = [];
  totalPages!:number;
  pageSize!:number;
  totalCount!:number;
  hasPrevious: boolean = false;
  hasNext: boolean = false;
  from!:number;
  to!:number;
  currentPage!:number;
}
