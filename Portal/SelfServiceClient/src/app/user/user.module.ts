import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageUsersComponent } from './manage-users/manage-users.component';
import { UserRoutingModule } from './user-routing.module';

@NgModule({
  declarations: [ManageUsersComponent],
  imports: [
    CommonModule,
    UserRoutingModule
  ]
})
export class UserModule { }
