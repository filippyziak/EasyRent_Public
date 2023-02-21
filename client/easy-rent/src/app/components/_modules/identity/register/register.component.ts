import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/services/alertify.service';
import { IdentityService } from 'src/app/services/identity.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})


export class RegisterComponent implements OnInit {
  types = [
    {id: 0, name: "Tenant"},
    {id: 1, name: "Place Owner"}
 ];
 type =  {id: 0, name: "Tenant"}

  constructor(private identityService: IdentityService, 
    private route: Router,
     private alertify: AlertifyService) { }

  ngOnInit() {
  }

  public register(emailAddress: string, password: string, type: number){
    this.identityService.register(emailAddress, password, type)
    .subscribe((res) => {
      this.alertify.success('registered successfully');
      this.route.navigate(['login']);
    }, error => {
      this.alertify.error('Cannot register, try another credentials');
      this.route.navigate(['register'])
    });
  }
}
