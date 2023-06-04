import { Component } from '@angular/core';
import {Navigations} from "../../../helpers/navigations";

@Component({
  selector: 'app-top-header',
  templateUrl: './top-header.component.html',
  styleUrls: ['./top-header.component.css']
})
export class TopHeaderComponent {
  navigator = new Navigations();
}
