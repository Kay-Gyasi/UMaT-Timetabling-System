import {Component, OnInit} from '@angular/core';
import {PaginatedList} from "../../../models/paginated-list";
import {RoomResponse} from "../../../models/responses/room-response";
import {RoomService} from "../../../services/http/room-service";
import {PaginatedQuery} from "../../../models/paginated-query";
import {NotificationService} from "../../../services/notification.service";

@Component({
  selector: 'app-view-rooms',
  templateUrl: './view-rooms.component.html',
  styleUrls: ['./view-rooms.component.css']
})
export class ViewRoomsComponent implements OnInit{
  rooms:PaginatedList<RoomResponse> = new PaginatedList<RoomResponse>();
  pages:Array<number> = [];
  query:PaginatedQuery = PaginatedQuery.Build(0, 1, 400);
  constructor(private roomService:RoomService,
              private toast: NotificationService) {
  }

  ngOnInit() {
    this.getRooms();
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
        this.toast.showSuccess("Rooms loaded successfully", "Success");
      },
      error: err => {
        this.toast.showError("Unable to load rooms", "Failed")
      }
    })
  }
}
