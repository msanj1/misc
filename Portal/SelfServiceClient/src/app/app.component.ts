import { Component, OnInit } from '@angular/core';
import { AuthService } from '../core/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent  implements OnInit {
  title = 'SelfServiceClient';
  isLoggedIn = false;
  
  constructor(private _authService:AuthService)
  {
    this._authService.loginChanged.subscribe(loggedIn => {
      this.isLoggedIn = loggedIn;
    });
  }

  ngOnInit(){
    this._authService.isLoggedIn().then(loggedin => {
      this.isLoggedIn = loggedin;
    });
  
  }

  login(){
    this._authService.login();
  }

  logout(){
    this._authService.logout();
  }

  
}
