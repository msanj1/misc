import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { SigningRedirectCallbackComponent } from './home/signin-redirect-callback.components';
import { SignoutRedirectCallbackComponent } from './home/singout-redirect-callback.components';
import { UnAuthorizedComponent } from './home/unauthorized.component';


const routes: Routes = [
  { path:'', component: HomeComponent },
  { path:'signin-callback',component: SigningRedirectCallbackComponent },
  { path:'signout-callback',component: SignoutRedirectCallbackComponent },
  { path:'unauthorized',component: UnAuthorizedComponent },
  { path:'users',loadChildren:()=>import('./user/user.module').then(m => m.UserModule)},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
