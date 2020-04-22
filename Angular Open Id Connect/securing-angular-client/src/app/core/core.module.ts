import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '../../../node_modules/@angular/common/http';
import { AuthInterceptorService } from './auth-interceptor.service';
import { ProjectService } from './project.service';
import { AuthService } from './auth-service.component';
import { AccountService } from './account.service';
import { AdminRouteGuard } from './admin-route-guard';

@NgModule({
    imports: [],
    exports: [],
    declarations: [],
    providers: [
        AuthService,
        AccountService,
        ProjectService,
        AdminRouteGuard,
        {
            provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi:true
        }
       
    ],
})
export class CoreModule { 

}
