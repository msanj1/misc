import { Injectable } from '@angular/core';
import {UserManager, User, UserManagerSettings} from 'oidc-client';
import { Subject } from 'rxjs';
import { AuthContext } from '../app/model/auth-context';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Constants } from '../app/constants';



@Injectable()
export class AuthService {
  private _userManager:UserManager;
  private _user:User;
  private _loginChangedSubject = new Subject<boolean>();
  authContext:AuthContext;

  loginChanged = this._loginChangedSubject.asObservable();

  constructor(private _httpClient:HttpClient) {
    const settings:UserManagerSettings = {
      authority: environment.identityServer,
      client_id: Constants.clientId,
      redirect_uri: `${environment.clientRoot}signin-callback`,
      scope: 'openid profile roles',
      response_type: 'code',
      post_logout_redirect_uri: `${environment.clientRoot}signout-callback`,
      
      // automaticSilentRenew: true,
      // silent_redirect_uri: `${environment.clientRoot}silent-callback.html`
    };
    this._userManager = new UserManager(settings);
    this._userManager.events.addAccessTokenExpired(_ =>{
      this._loginChangedSubject.next(false);
    });

    this._userManager.events.addUserLoaded(user =>{
      if(this._user !== user)
      {
        this._user = user;
        this.loadSecurityContext();
        this._loginChangedSubject.next(!!user && !user.expired);
      }
    });
   }

   isLoggedIn(): Promise<boolean>{
     return this._userManager.getUser().then(user =>{
       const userIsCurrent = !!user && !user.expired;
      if(this._user !== user){
        this._loginChangedSubject.next(userIsCurrent);
      }
      if(userIsCurrent && !this.authContext){
        this.loadSecurityContext();
      }
      this._user = user;
      return userIsCurrent;
     });
   }

   login(){
     return this._userManager.signinRedirect();
   }

   logout(){
     this._userManager.signoutRedirect();
   }

   completeLogout(){
     this._user = null;
     this._loginChangedSubject.next(false);
     return this._userManager.signoutRedirectCallback();
   }

   getAccessToken(){
     return this._userManager.getUser().then(user =>{
       if(!!user && !user.expired){
         return user.access_token;
       }else {
         return null;
       }
     })
   }

   completeLogin(){
    return this._userManager.signinRedirectCallback().then(user => {
      this._user = user;
      this._loginChangedSubject.next(!!user && !user.expired);
      return user;
    });
   }

  private loadSecurityContext(){
    //load security configurations for the current user
  }   
}
