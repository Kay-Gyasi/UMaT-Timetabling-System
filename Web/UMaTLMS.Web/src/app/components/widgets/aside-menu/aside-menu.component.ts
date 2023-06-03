import { Component } from '@angular/core';
import {Navigations} from "../../../helpers/navigations";

@Component({
  selector: 'app-aside-menu',
  templateUrl: './aside-menu.component.html',
  styleUrls: ['./aside-menu.component.css']
})
export class AsideMenuComponent {
  navigator = new Navigations();
}
