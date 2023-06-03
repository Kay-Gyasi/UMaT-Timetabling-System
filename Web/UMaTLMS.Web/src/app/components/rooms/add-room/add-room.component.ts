import { Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  UntypedFormArray,
  UntypedFormBuilder,
  UntypedFormGroup,
  Validators
} from "@angular/forms";

@Component({
  selector: 'app-add-room',
  templateUrl: './add-room.component.html',
  styleUrls: ['./add-room.component.css']
})
export class AddRoomComponent {
  roomForm:UntypedFormGroup;

  constructor(private fb:FormBuilder) {
    this.initForm()
  }

  addRoom(){
    //
  }

  getControl(name: string){
      return this.roomForm.get(name) as FormControl;
  }

  private initForm(){
    this.roomForm = this.fb.group({
      name: ["", [Validators.required]],
      capacity: [0, []],
      isLab: [false, []]
    })
  }
}
