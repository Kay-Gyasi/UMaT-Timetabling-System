import { Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  UntypedFormGroup,
  Validators
} from "@angular/forms";
import {RoomService} from "../../../services/http/room-service";
import {RoomRequest} from "../../../models/requests/room-request";
import {NotificationService} from "../../../services/notification.service";

@Component({
  selector: 'app-add-room',
  templateUrl: './add-room.component.html',
  styleUrls: ['./add-room.component.css']
})
export class AddRoomComponent {
  roomForm:UntypedFormGroup;
  isLoading:boolean = false;

  constructor(private fb:FormBuilder, private roomService:RoomService,
              private toast:NotificationService) {
    this.initForm()
  }

  addRoom(){
    this.isLoading = true;
    return this.roomService.save(this.roomForm.value as RoomRequest).subscribe({
      next: _ => {
        this.roomForm.setValue({
          name: "",
          capacity: 0,
          isLab: false,
          isIncludedInGeneralAssignment: false
        });
      },
      error: _ => {
        this.toast.showError("Unable to add room", "Failed");
      },
      complete: () => {
        this.isLoading = false;
      }
    })
  }

  getControl(name: string){
      return this.roomForm.get(name) as FormControl;
  }

  private initForm(){
    this.roomForm = this.fb.group({
      name: ["", [Validators.required]],
      capacity: [0, []],
      isLab: [false, []],
      isIncludedInGeneralAssignment: [false, []]
    })
  }
}
