import {ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {LectureResponse} from "../../../models/responses/lecture-response";
import {LectureService} from "../../../services/http/lecture.service";
import {ActivatedRoute, Router} from "@angular/router";
import {NotificationService} from "../../../services/notification.service";
import {Navigations} from "../../../helpers/navigations";
import {Lookup, LookupType} from 'src/app/models/lookup';
import {LookupService} from "../../../services/http/lookup.service";
import {LectureRequest} from "../../../models/requests/lecture-request";
import {SubClassRequest} from "../../../models/requests/sub-class-request";

@Component({
  selector: 'app-edit-lecture',
  templateUrl: './edit-lecture.component.html',
  styleUrls: ['./edit-lecture.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EditLectureComponent implements OnInit{
  lecture:LectureResponse;
  lectureId:number;
  lectures:LectureRequest[] = [];
  pairings:number[] = [];
  selectedLecture:LectureRequest = new LectureRequest();
  navigator = new Navigations();
  roomsLookup:Lookup[] = [];

  constructor(private route:ActivatedRoute, private toast:NotificationService, private lectureService:LectureService,
              private cdr:ChangeDetectorRef, private router:Router, private lookupService:LookupService) {
  }

  ngOnInit() {
    this.lecture = new LectureResponse();
    this.initialize();
  }

  submitLectures(){
    for (const lecture of this.lectures) {
      lecture.isVLE = this.lecture.isVLE;
    }
    this.lectureService.createCombined(this.lectures).subscribe({
      next: async _ => {
        this.toast.showSuccess("", "Success");
        await this.router.navigateByUrl(this.navigator.lectures);
      },
      error: _ => {
        this.toast.showError("", "Operation Failed");
      }
    })
  }

  createPair(){
    if (this.pairings.length == 0) return;
    let request = new LectureRequest();
    request.lecturerId = this.selectedLecture?.lecturerId;
    request.courseId = this.selectedLecture?.courseId;
    request.isVLE = this.selectedLecture?.isVLE;
    request.isPractical = this.selectedLecture?.isPractical;
    request.preferredRoom = this.selectedLecture.preferredRoom;
    request.subClassGroups = [];

    for (const x of this.pairings ?? []) {
      let subClass = this.selectedLecture.subClassGroups.find(a => a.id == x);
      if (subClass == null || subClass == undefined) continue;
      request.subClassGroups.push(subClass);
      this.selectedLecture.subClassGroups = this.selectedLecture.subClassGroups.filter(a => a != subClass);
    }

    this.lectures.push(request);
    this.pairings = [];
    this.cdr.detectChanges();
  }

  deleteLecture(lecture:LectureRequest){
    if (lecture.id == this.lectureId) return;
    this.lectures = this.lectures.filter(x => x != lecture);

    for (const group of lecture.subClassGroups) {
      this.lectures[0].subClassGroups = [...this.lectures[0].subClassGroups, group];
    }
    this.cdr.detectChanges();
  }

  selectLecture(lecture:LectureRequest){
    if (lecture.id == this.lectureId){
      this.selectedLecture = this.lectures[0];
      return;
    }
    this.selectedLecture = lecture;
  }

  private initialize(){
    this.lectureId = this.route.snapshot.params["id"];
    this.getSelectedLecture();
    this.getRooms();
  }

  private getSelectedLecture(){
    this.lectureService.get(this.lectureId).subscribe({
      next: response => {
        this.lecture = response ?? new LectureResponse();
        let request = new LectureRequest();
        request.id = response?.id ?? 0;
        request.lecturerId = response?.lecturerId ?? 0;
        request.courseId = response?.courseId ?? 0;
        request.isVLE = response?.isVLE ?? false;
        request.isPractical = response?.isPractical ?? false;
        request.preferredRoom = response?.preferredRoom ?? "";
        request.subClassGroups = [];

        for (const x of response?.subClassGroups ?? []) {
          let subClass = new SubClassRequest();
          subClass.groupId = x.groupId;
          subClass.name = x.name;
          subClass.id = x.id;
          request.subClassGroups.push(subClass);
        }

        this.lectures.push(request);
      },
      error: err => {
        this.toast.showError("Unable to fetch lecture details", "Failed");
      }
    })
  }

  private getRooms(){
    this.lookupService.get(LookupType.Rooms).subscribe({
      next: response => {
        this.roomsLookup = response ?? [];
      },
      error: err => {
        this.toast.showError("Unable to load rooms", "Failed");
      }
    })
  }
}
