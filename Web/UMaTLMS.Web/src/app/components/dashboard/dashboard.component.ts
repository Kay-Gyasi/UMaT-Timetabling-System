import { Component } from '@angular/core';
import {Navigations} from "../../helpers/navigations";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  navigator = new Navigations();
}
