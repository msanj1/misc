import { NgModule } from '@angular/core';
import { AuthService } from './auth.service';

// Service	
// Service modules provide utility services such as data access and messaging. Ideally, they consist entirely of providers and have no declarations. Angular's HttpClientModule is a good example of a service module.

// The root AppModule is the only module that should import service modules.

@NgModule({
  declarations: [],
  imports: [
    
  ],
  providers:[
    AuthService
  ]
})
export class CoreModule { }
