import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, UntypedFormGroup, Validators} from "@angular/forms";
import {ActivatedRoute} from "@angular/router";
import {RoomService} from "../../../services/http/room.service";
import {NotificationService} from "../../../services/notification.service";
import {RoomRequest} from "../../../models/requests/room-request";

@Component({
  selector: 'app-edit-room',
  templateUrl: './edit-room.component.html',
  styleUrls: ['./edit-room.component.css']
})
export class EditRoomComponent implements OnInit {
  roomForm:UntypedFormGroup;
  roomId:number;
  isLoading:boolean = false;
  constructor(private fb:FormBuilder, private route:ActivatedRoute,
              private roomService:RoomService, private toast:NotificationService) {
  }

  ngOnInit() {
    this.initialize()
  }

  editRoom(){
    this.isLoading = true;
    let request = new RoomRequest();
    request.id = this.roomId;
    request.name = this.roomForm.get('name')?.value;
    request.capacity = this.roomForm.get('capacity')?.value;
    request.isLab = this.roomForm.get('isLab')?.value;
    request.includeInGeneralAssignment = this.roomForm.get('includeInGeneralAssignment')?.value;
    request.isExaminationCenter = this.roomForm.get('isExaminationCenter')?.value;
    return this.roomService.save(request).subscribe({
      error: err => {
        this.toast.showError("Unable to edit room", "Failed");
      },
      complete: () => {
        this.isLoading = false;
      }
    })
  }

  getControl(name: string){
    return this.roomForm.get(name) as FormControl;
  }

  private initialize(){
    this.roomForm = this.fb.group({
      name: ["", [Validators.required]],
      capacity: [0, []],
      isLab: [false, []],
      isExaminationCenter: [false, []],
      includeInGeneralAssignment: [false, []]
    })

    this.roomId = this.route.snapshot.params["id"];
    this.roomService.get(this.roomId).subscribe({
      next: response => {
        this.roomForm.setValue({
          name: response?.name,
          capacity: response?.capacity,
          isLab: response?.isLab,
          includeInGeneralAssignment: response?.includeInGeneralAssignment,
          isExaminationCenter: response?.isExaminationCenter
        })
      },
      error: err => {
        this.toast.showError("Unable to fetch room details", "Failed");
      }
    })
  }
}
