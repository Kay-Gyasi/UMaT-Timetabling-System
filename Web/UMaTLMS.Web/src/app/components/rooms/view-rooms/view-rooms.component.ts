import {Component, OnInit} from '@angular/core';
import {PaginatedList} from "../../../models/paginated-list";
import {RoomResponse} from "../../../models/responses/room-response";
import {RoomService} from "../../../services/http/room.service";
import {PaginatedQuery} from "../../../models/paginated-query";
import {NotificationService} from "../../../services/notification.service";
import {Navigations} from "../../../helpers/navigations";
import { FormBuilder, FormControl, UntypedFormGroup, Validators } from '@angular/forms';

declare var KTMenu:any;
@Component({
  selector: 'app-view-rooms',
  templateUrl: './view-rooms.component.html',
  styleUrls: ['./view-rooms.component.css']
})
export class ViewRoomsComponent implements OnInit{
  rooms:PaginatedList<RoomResponse> = new PaginatedList<RoomResponse>();
  pages:Array<number> = [];
  navigator = new Navigations();
  isLoading:boolean = false;
  searchForm:UntypedFormGroup;
  query:PaginatedQuery = PaginatedQuery.Build(0, 1, 20);
  constructor(private roomService:RoomService, private fb:FormBuilder,
              private toast: NotificationService) {
      this.searchForm = this.fb.group({
        "term": ["", [Validators.maxLength(30)]]
      });
  }

  ngOnInit() {
    this.initialize();
  }

  public getRooms(pageNumber:number = 1){
    this.isLoading = true;
    this.buildQuery(pageNumber);
    this.roomService.getPage(this.query).subscribe({
      next: data => {
        if (data == undefined){
          this.toast.showError("Unable to load rooms", "Failed")
          return;
        }
        this.rooms = data;
        for (let i = 1; i <= data.totalPages; i++){
          this.pages.push(i);
        }
        this.isLoading = false;
      },
      error: err => {
        this.toast.showError("Unable to load rooms", "Failed");
        this.isLoading = false;
      }
    })
  }

  deleteRoom(room:RoomResponse){
    let isConfirmed = window.confirm(`Are you sure you want to delete ${room.name}?`);
    if (!isConfirmed) return;
    this.roomService.delete(room.id).subscribe({
      next: response => {
        if (response == undefined){
          this.toast.showError("Unable to delete room", "Failed");
          return;
        }

        this.toast.showSuccess("Room deleted", "Succeeded");
        this.rooms.data = this.rooms.data.filter(x => x.id != room.id);
      },
      error: err => {
        this.toast.showError("Unable to delete room", "Failed");
      }
    })
  }

  get term(){
    return this.searchForm.get('term') as FormControl;
  }

  private buildQuery(pageNumber:number){
    if (pageNumber == -1) this.query.PageNumber -= 1;
    else if (pageNumber == -2) this.query.PageNumber += 1;
    else this.query.PageNumber = pageNumber;
    this.query.thenSearch(this.searchForm.get('term')?.value);
  }

  private initialize(){
    KTMenu.init();
    KTMenu.init();
    this.getRooms();
  }
}
