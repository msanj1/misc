import { SimpleClaim } from './simple-claim';

export class AuthContext{
    claims:SimpleClaim[];
    //add more properties here to define all the prileges a user context will have

    get isAdmin(): boolean{
        return !!this.claims && !!(this.claims.find(c => c.type === "role" && (c.value === "Admin" || c.value === "SysAdmin")));
    }
}