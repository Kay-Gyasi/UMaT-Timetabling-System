import {Component, OnInit} from '@angular/core';
import {AuthService} from "./services/auth.service";


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'UMaTTS';
  public userAuthenticated = false;

  constructor(private _authService: AuthService){
    this._authService.loginChanged
      .subscribe(userAuthenticated => {
        this.userAuthenticated = userAuthenticated;
      })
  }

  ngOnInit(): void {
    this._authService.isAuthenticated()
      .then(userAuthenticated => {
        console.log(userAuthenticated);
        this.userAuthenticated = userAuthenticated;
        // if (!userAuthenticated){
        //   this._authService.login().then(_ => this.userAuthenticated = true);
        // }
      })
  }
}
