import { Injectable } from '@angular/core';
import { CoreModule } from './core.module';
import {UserManager, User} from 'oidc-client';
import { Constants } from '../constants';
import { Subject } from '../../../node_modules/rxjs';
import { HttpClient } from '../../../node_modules/@angular/common/http';
import { AuthContext } from '../model/auth-context';

@Injectable()
export class AuthService{
    private _userManager: UserManager;
    private _user: User; //signed in user
    private _loginChangedSubject = new Subject<boolean>();

    public loginChanged = this._loginChangedSubject.asObservable();
    authContext: AuthContext;

    constructor(private _httpClient:HttpClient) {
        const stsSettings = {
            authority: Constants.stsAuthority,
            client_id: Constants.clientId,
            redirect_uri: `${Constants.clientRoot}signin-callback`,
            scope: 'openid profile projects-api',
            response_type: 'code',
            post_logout_redirect_uri: `${Constants.clientRoot}signout-callback`,
            automaticSilentRenew: true,
            silent_redirect_uri: `${Constants.clientRoot}assets/silent-callback.html`
        };
        this._userManager = new UserManager(stsSettings);
        this._userManager.events.addAccessTokenExpired(_ => {
            this._loginChangedSubject.next(false);
        });
        this._userManager.events.addUserLoaded(user => {
            if (this._user !== user)
            {
                this._user = user;
                this.loadSecurityContext();
                this._loginChangedSubject.next(!!user && !user.expired);
            }
        });
    }

    login(){
        return this._userManager.signinRedirect();
    }

    isLoggedIn(): Promise<boolean>{
        return this._userManager.getUser().then(user => {
            const userCurrent = !!user && !user.expired;
            if(this._user !== user)
            {
                this._loginChangedSubject.next(userCurrent);
            }
            if(userCurrent && !this.authContext){
                this.loadSecurityContext();
            } 
            this._user = user;
            return userCurrent;
        });
    }

    completeLogin(){
        return this._userManager.signinRedirectCallback().then(user => {
            this._user = user;
            this._loginChangedSubject.next(!!user && !user.expired);
            return user;
        })
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
        return this._userManager.getUser().then(user => {
            if (!!user && !user.expired)
            {
                return user.access_token;
            }else {
                return null;
            }
        })
    }

    loadSecurityContext(){
        this._httpClient
        .get<AuthContext>(`${Constants.apiRoot}Projects/AuthContext`)
        .subscribe(
            context => {
                this.authContext = new AuthContext();
                this.authContext.claims = context.claims;
                this.authContext.userProfile = context.userProfile;
            },
            error => console.error(error)
        )
    }
}