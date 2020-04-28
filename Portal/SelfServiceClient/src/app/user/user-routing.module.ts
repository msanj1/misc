import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageUsersComponent } from './manage-users/manage-users.component';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [
  { path:'manage', component: ManageUsersComponent }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),
  ]
})
export class UserRoutingModule { }
