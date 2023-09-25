import { Injectable } from '@angular/core';
import { UserManager, User, UserManagerSettings } from 'oidc-client';
import {Constants} from "../helpers/constants";
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _userManager: UserManager;
  private _user: User;
  private _loginChangedSubject = new Subject<boolean>();

  public loginChanged = this._loginChangedSubject.asObservable();
  private get idpSettings() : UserManagerSettings {
    return {
      authority: Constants.idpAuthority,
      client_id: Constants.clientId,
      client_secret: Constants.clientSecret,
      redirect_uri: `${Constants.clientRoot}/signin-oidc`,
      scope: "openid profile email roles",
      response_type: "code",
      post_logout_redirect_uri: `https://portal.umat.edu.gh/auth/Account/Logout`
    }
  }
  constructor() {
    this._userManager = new UserManager(this.idpSettings);
  }

  public login = () => {
    return this._userManager.signinRedirect();
  }

  public isAuthenticated = (): Promise<boolean> => {
    return this._userManager.getUser()
      .then(user => {
        if (user == null) return false;
        if(this._user !== user){
          this._loginChangedSubject.next(this.checkUser(user));
        }
        this._user = user;
        return this.checkUser(user);
      })
  }

  private checkUser = (user : User): boolean => {
    return !!user && !user.expired;
  }
}
