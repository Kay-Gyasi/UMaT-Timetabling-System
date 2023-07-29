import {Component, OnInit} from '@angular/core';
import {LecturerResponse} from "../../../models/responses/lecturer-response";
import {PreferenceResponse} from "../../../models/responses/preference-response";
import {ActivatedRoute} from "@angular/router";
import {LecturerService} from "../../../services/http/lecturer.service";
import {NotificationService} from "../../../services/notification.service";
import {PreferenceLookups} from "../../../models/responses/preference-lookups";
import {PreferenceService} from "../../../services/http/preference.service";
import {FormBuilder, UntypedFormGroup, Validators} from "@angular/forms";
import {Lookup, LookupType} from "../../../models/lookup";
import {LookupService} from "../../../services/http/lookup.service";
import {PreferenceRequest} from "../../../models/requests/preference-request";

declare var KTMenu:any;
@Component({
  selector: 'app-lecturer-preferences',
  templateUrl: './lecturer-preferences.component.html',
  styleUrls: ['./lecturer-preferences.component.css']
})
export class LecturerPreferencesComponent implements OnInit {

  lecturer: LecturerResponse;
  preferenceLookups: PreferenceLookups = new PreferenceLookups();
  preferences:PreferenceResponse[];
  preferenceRequest:PreferenceRequest = new PreferenceRequest();
  preferenceTypeValues: Lookup[] = [];
  lecturers:Lookup[];
  preferenceForm:UntypedFormGroup;
  isLoading = false;
  daysOfWeek:Lookup[];
  dayForTimeNotAvailable:string;
  constructor(private route:ActivatedRoute, private lecturerService:LecturerService,
              private toast:NotificationService, private preferenceService:PreferenceService,
              private fb:FormBuilder, private lookupService:LookupService) {
  }

  ngOnInit(): void {
    KTMenu.init();
    KTMenu.init();

    this.preferenceForm = this.fb.group({
      type: [0, [Validators.required]],
      values: [[''], [Validators.required]],
      timetableType: [0, [Validators.required]],
      lecturers: [[], [Validators.required]]
    });

    this.getPreferences();
    this.getPreferenceLookups();
    this.getLecturersLookup();

    this.daysOfWeek = [
      {
        id:1,
        name:'Monday'
      },
      {
        id:2,
        name:'Tuesday'
      },
      {
        id:3,
        name:'Wednesday'
      },
      {
        id:4,
        name:'Thursday'
      },
      {
        id:5,
        name:'Friday'
      },
    ]
  }

  setPreference(){
    this.isLoading = true;
    this.preferenceRequest = this.preferenceForm.value;
    this.preferenceRequest.dayForTimeNotAvailable = this.dayForTimeNotAvailable;

    this.preferenceService.set(this.preferenceRequest).subscribe({
      next: data => {
        if (data === undefined){
          this.toast.showError('Unable to add preference', 'Failed');
          this.isLoading = false;
          return;
        }

        this.toast.showSuccess('Preference created', 'Success');
        this.getPreferences();
        this.isLoading = false;
      },
      error: _ => {
        this.toast.showError('Unable to add preference', 'Failed');
        this.isLoading = false;
      }
    })
  }

  getPreferenceValues(){
    this.isLoading = true;
    let selectedType = this.preferenceLookups.preferenceTypes
                    .find(x => x.id == this.preferenceForm.get('type')?.value);
    if (selectedType === undefined) return;
    this.preferenceService.getTypeValues(selectedType.id - 1).subscribe({
      next: data => {
        if (data === undefined) return;
        this.preferenceForm.get('values')?.setValue([]);
        this.preferenceForm.get('dayForTimeNotAvailable')?.setValue(['']);
        this.preferenceTypeValues = data;
        if (data.some(x => x.name == 'Monday')){
          this.daysOfWeek = data;
        }
      },
      error: _ => {
        this.toast.showError('Failed to fetch preference values', 'Failed')
      },
      complete: () => {
        this.isLoading = false;
      }
    })
  }


  deletePreference(preferenceId:number){
    this.isLoading = true;
    this.preferenceService.delete(preferenceId).subscribe({
      next: data => {
        if (data === undefined){
          this.toast.showError('Unable to delete preference', 'Failed');
          this.isLoading = false;
          return;
        }

        this.preferences = this.preferences.filter(x => x.id != preferenceId);
        this.isLoading = false;
        this.toast.showSuccess('', 'Success');
      },
      error: _ => {
        this.isLoading = false;
        this.toast.showError('Unable to delete preference', 'Failed');
      }
    })
  }

  getPreferences(){
    this.lecturerService.getPreferences().subscribe({
      next: data => {
        if (data === undefined){
          this.toast.showError('Unable to retrieve preferences', 'Failed');
          return;
        }

        this.preferences = data;
      },
      error: _ => this.toast.showError('Unable to retrieve preferences', 'Failed')
    })
  }
  isTimeNotAvailable(){
    return this.preferenceTypeValues.some(x => x.name.includes('6'));
  }
  private getLecturersLookup(){
    this.lookupService.get(LookupType.Lecturers).subscribe({
      next: data => {
        if (data === undefined) return;
        this.lecturers = data;
      },
      error: _ => console.log('Error while loading lookups')
    })
  }
  private getPreferenceLookups(){
    this.preferenceService.getLookups().subscribe({
      next: data => {
        if (data === undefined) return;
        this.preferenceLookups = data;
      },
      error: _ => console.log('Error while loading lookups')
    })
  }
}
