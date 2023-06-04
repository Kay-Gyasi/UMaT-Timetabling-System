import {Component, OnInit} from '@angular/core';
import {PaginatedList} from "../../../models/paginated-list";
import {RoomResponse} from "../../../models/responses/room-response";
import {RoomService} from "../../../services/http/room-service";
import {PaginatedQuery} from "../../../models/paginated-query";
import {NotificationService} from "../../../services/notification.service";
import {Navigations} from "../../../helpers/navigations";

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
  query:PaginatedQuery = PaginatedQuery.Build(0, 1, 400);
  constructor(private roomService:RoomService,
              private toast: NotificationService) {
  }

  ngOnInit() {
    this.initialize();
  }

  private getRooms(){
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
      },
      error: err => {
        this.toast.showError("Unable to load rooms", "Failed")
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

  private initialize(){
    KTMenu.init();
    KTMenu.init();
    this.getRooms();
  }
}
